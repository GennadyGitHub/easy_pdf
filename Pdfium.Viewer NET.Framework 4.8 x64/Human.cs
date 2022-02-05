using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Pdfium.Viewer_NET.Framework_4._8_x64
{
    public class Signature
    {
        internal protected static object locker = new object();
        //public string Surname
        //{
        //    get
        //    {
        //        return surname;
        //    }
        //    set
        //    {
        //        if (value != null && value.Contains(' ') == false && Regex.IsMatch(value, @"^[a-zA-Z]+$"))
        //            surname = value;
        //    }
        //}
        public Signature(MainWindow window, Form1 form1)
        {
            this.window = window;
            this.form1 = form1;
        }
        public Signature(MainWindow window)
        {
            this.window = window;
        }
        public Signature(Form1 form1)
        {
            this.form1 = form1;
        }
        MainWindow window;
        Form1 form1;

        public PdfSharp.Pdf.PdfDocument Sign(string filename, string sign_path, ref int sign_count, double x, double y, double width, double height)
        {
            PdfSharp.Pdf.PdfDocument pdf = PdfSharp.Pdf.IO.PdfReader.Open(filename, PdfDocumentOpenMode.Modify);
            PdfSharp.Pdf.PdfPage page = pdf.Pages[0];

            double y_page = Convert.ToInt32(page.Height);
            y = y_page - y;

            XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);
            form1.Invoke((MethodInvoker)(() =>
            {
                if (form1.iconButton3.IconChar == FontAwesome.Sharp.IconChar.FileSignature)
                {
                    Bitmap bmp = new Bitmap(sign_path);
                    bmp.MakeTransparent(System.Drawing.Color.White);
                    PdfSharp.Drawing.XImage img = PdfSharp.Drawing.XImage.FromGdiPlusImage(bmp);
                    gfx.DrawImage(img, x, y, width, height);
                }
                else if (form1.iconButton3.IconChar == FontAwesome.Sharp.IconChar.CalendarAlt)
                {
                    System.Drawing.RectangleF rect = new System.Drawing.RectangleF(0, 0, (float)((float)width * 5.77), (float)((float)height * 2.62));
                    Bitmap bmp1 = new Bitmap(Convert.ToInt32(width * 4.23), Convert.ToInt32(height * 2.62), PixelFormat.Format24bppRgb);
                    System.Drawing.Font font = new System.Drawing.Font("GOST type A", (float)width); //шрифт для даты
                    Graphics g = Graphics.FromImage(bmp1);
                    g.FillRectangle(Brushes.White, rect);

                    StringFormat _format = new StringFormat(StringFormatFlags.NoClip);
                    _format.LineAlignment = StringAlignment.Near;
                    _format.Alignment = StringAlignment.Near;
                    g.DrawString(window.textbox1.Text, font, Brushes.Black, rect, _format);
                    bmp1.MakeTransparent(System.Drawing.Color.White);
                    PdfSharp.Drawing.XImage img1 = PdfSharp.Drawing.XImage.FromGdiPlusImage(bmp1);
                    gfx.DrawImage(img1, x, y, width, height);
                }
            }));

            page.Close();
            pdf.Close();

            sign_count++;

            return pdf;
        }
        public PdfSharp.Pdf.PdfDocument Sign(Listbox_Item obj, List<Sign_Object> obj1, string filename, ref int sign_count)
        {

            PdfSharp.Pdf.PdfDocument pdf = PdfSharp.Pdf.IO.PdfReader.Open(filename, PdfDocumentOpenMode.Modify);
            PdfSharp.Pdf.PdfPage page = pdf.Pages[0];
            float y = Convert.ToInt32(page.Height);

            XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);

            for (int i = 0; i < obj1.Count; i++)
            {
                if (obj1[i].indent_X != 0 && obj1[i].indent_Y != 0 && obj1[i].scale_X != 0 && obj1[i].scale_Y != 0)
                {
                    Bitmap bmp = new Bitmap(obj1[i].path);
                    bmp.MakeTransparent(System.Drawing.Color.White);
                    PdfSharp.Drawing.XImage img = PdfSharp.Drawing.XImage.FromGdiPlusImage(bmp);

                    var surnames_list = obj1[i].Sign_Plase(obj);
                    for (int j = 0; j < surnames_list.Count; j++)
                    {
                        float _x = surnames_list[j].surname_X - obj1[i].scale_X / 2;
                        float _y = y - surnames_list[j].surname_Y - obj1[i].scale_Y / 2;
                        gfx.DrawImage(img, _x, _y, obj1[i].scale_X, obj1[i].scale_Y);
                        sign_count++;
                    }
                }
                if (obj1[i].date_indent_X != 0 && obj1[i].date_indent_Y != 0 && obj1[i].date_scale_X != 0 && obj1[i].date_scale_Y != 0)
                {
                    int date_count = 0;
                    var surnames_list = obj1[i].Sign_Plase(obj);
                    for (int j = 0; j < surnames_list.Count; j++)
                    {
                        float _x = surnames_list[j].date_surname_X - obj1[i].date_scale_X / 2;
                        float _y = y - surnames_list[j].date_surname_Y - obj1[i].date_scale_Y / 2;
                        System.Drawing.RectangleF rect = new System.Drawing.RectangleF(0, 0, (float)((float)obj1[i].date_scale_X * 5.77), (float)((float)obj1[i].date_scale_Y * 2.62));
                        Bitmap bmp1 = new Bitmap(Convert.ToInt32(obj1[i].date_scale_X * 4.23), Convert.ToInt32(obj1[i].date_scale_Y * 2.62), PixelFormat.Format24bppRgb);
                        System.Drawing.Font font = new System.Drawing.Font("GOST type A", obj1[i].date_scale_X); //шрифт для даты
                        Graphics g = Graphics.FromImage(bmp1);
                        g.FillRectangle(Brushes.White, rect);
                        window.Dispatcher.Invoke(() =>
                        {
                            if (window.textbox1.Text != "")
                            {
                                g.DrawString(window.textbox1.Text, font, Brushes.Black, rect, StringFormat.GenericTypographic);
                                date_count = 1;
                            }
                        });
                        bmp1.MakeTransparent(System.Drawing.Color.White);
                        PdfSharp.Drawing.XImage img1 = PdfSharp.Drawing.XImage.FromGdiPlusImage(bmp1);
                        gfx.DrawImage(img1, _x, _y, obj1[i].date_scale_X, obj1[i].date_scale_Y);
                        if (date_count == 1)
                            sign_count++;
                    }
                }
            }
            page.Close();
            pdf.Close();

            return pdf;
        }
        public void Save(string filename, PdfSharp.Pdf.PdfDocument pdf)
        {
            lock (locker)
                pdf.Save(filename);
        }
    }

}
