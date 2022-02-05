using Spire.Pdf.General.Find;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Media;

namespace Pdfium.Viewer_NET.Framework_4._8_x64
{
    public class Listbox_Item //объектное представление документа в формате pdf
    {
        public Listbox_Item(string path)
        {
            this.path = path;
            this.name = System.IO.Path.GetFileName(path);
            sign_count = 0;
        }
        public Listbox_Item(string path, int sign_count)
        {
            this.path = path;
            this.name = System.IO.Path.GetFileName(path);
            this.sign_count = sign_count;
        }
        public string path;
        public string name;
        public int sign_count;
    }

    public class Sign_Object //объектное представление сущности подпись в формате pdf
    {
        public Sign_Object(Form1 form1, string name, string path)
        {
            this.form1 = form1;
            this.name = name;
            this.path = path;
        }
        public Sign_Object(string name, string path, float indent_X, float indent_Y, float scale_X, float scale_Y, float date_indent_X, float date_indent_Y, float date_scale_X, float date_scale_Y)
        {
            this.name = name;
            this.path = path;
            this.indent_X = indent_X;
            this.indent_Y = indent_Y;
            this.scale_X = scale_X;
            this.scale_Y = scale_Y;
            this.date_indent_X = date_indent_X;
            this.date_indent_Y = date_indent_Y;
            this.date_scale_X = date_scale_X;
            this.date_scale_Y = date_scale_Y;
        }

        Form1 form1;
        public string name;
        public string path;
        public float indent_X;
        public float indent_Y;
        public float scale_X;
        public float scale_Y;
        public float date_indent_X;
        public float date_indent_Y;
        public float date_scale_X;
        public float date_scale_Y;

        public void Indent_mouse_from_surname(float mouse_x, float mouse_y)
        {
            if (MainWindow.obj != null)
            {
                Spire.Pdf.PdfDocument pdfDocument = new Spire.Pdf.PdfDocument();
                pdfDocument.LoadFromFile(MainWindow.obj.path);
                Spire.Pdf.PdfPageBase page = pdfDocument.Pages[0];

                float Y_page = page.ActualSize.Height;

                string _name = name.Remove(name.Length - 4);
                PdfTextFind[] text = pdfDocument.Pages[0].FindText(_name).Finds;
                try
                {
                    PointF p = text[0].Position;
                    indent_X = mouse_x - p.X; //относительная координата Х подписи относительно фамилии
                    indent_Y = mouse_y - (Y_page - p.Y); //относительная координата Y подписи относительно фамилии
                }
                catch
                {
                    if (form1.iconButton3.IconChar == FontAwesome.Sharp.IconChar.FileSignature)
                        System.Windows.Forms.MessageBox.Show(form1, "В тексте документа нет совпадений с именем выбранной подписи.\nИзображение будет добавлено без какой-либо привязки.");
                    if (form1.iconButton3.IconChar == FontAwesome.Sharp.IconChar.CalendarAlt)
                        System.Windows.Forms.MessageBox.Show(form1, "В тексте документа нет совпадений с именем выбранной подписи.\nПроизвольный текст из textbox1 будет добавлен без какой-либо привязки.");
                    indent_X = mouse_x;
                    indent_Y = mouse_y - Y_page;
                }
            }
        }
        public List<Surname> Sign_Plase(Listbox_Item obj)
        {
            Spire.Pdf.PdfDocument pdfDocument = new Spire.Pdf.PdfDocument();
            pdfDocument.LoadFromFile(obj.path);
            Spire.Pdf.PdfPageBase page = pdfDocument.Pages[0];

            float Y_page = page.ActualSize.Height;

            string _name = name.Remove(name.Length - 4);
            PdfTextFind[] text = pdfDocument.Pages[0].FindText(_name).Finds;

            List<Surname> list = new List<Surname>();

            for (int i = 0; i < text.Length; i++)
            {
                PointF p = text[i].Position;
                list.Add(new Surname(p.X + indent_X, Y_page - p.Y + indent_Y, p.X + date_indent_X, Y_page - p.Y + date_indent_Y));
            }
            return list;
        }
    }
    public class Surname
    {
        public Surname(float surname_X, float surname_Y, float date_surname_X, float date_surname_Y)
        {
            this.surname_X = surname_X;
            this.surname_Y = surname_Y;
            this.date_surname_X = date_surname_X;
            this.date_surname_Y = date_surname_Y;
        }
        public float surname_X;
        public float surname_Y;
        public float date_surname_X;
        public float date_surname_Y;
    }
    public static class Sound
    {
        public static void Play_Sound(int i)
        {
            switch (i)
            {
                case 1:
                    using (MemoryStream fileout = new MemoryStream(Properties.Resources.Button))
                    using (GZipStream gz = new GZipStream(fileout, CompressionMode.Decompress))
                        new SoundPlayer(gz).Play();
                    break;
                case 2:
                    using (MemoryStream fileout = new MemoryStream(Properties.Resources.Event))
                    using (GZipStream gz = new GZipStream(fileout, CompressionMode.Decompress))
                        new SoundPlayer(gz).Play();
                    break;
                case 3:
                    using (MemoryStream fileout = new MemoryStream(Properties.Resources.Form_close))
                    using (GZipStream gz = new GZipStream(fileout, CompressionMode.Decompress))
                        new SoundPlayer(gz).Play();
                    break;
                case 4:
                    using (MemoryStream fileout = new MemoryStream(Properties.Resources.Knopka))
                    using (GZipStream gz = new GZipStream(fileout, CompressionMode.Decompress))
                        new SoundPlayer(gz).Play();
                    break;
            }
        }
    }
}
