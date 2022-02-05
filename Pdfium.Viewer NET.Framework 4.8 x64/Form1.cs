using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Pdfium.Viewer_NET.Framework_4._8_x64
{
    public partial class Form1 : Form
    {
        MainWindow window;
        Graphics g;

        public List<Sign_Object> _list;
        public static Sign_Object obj;

        float mouse_x; // лайв координата Х в поле pdfViewer1
        float mouse_y; // лайв координата Y в поле pdfViewer1

        float _mouse_x; //поправка на размер подписи
        float _mouse_y;

        string global_path;
        string global_path_to_xmlfile;

        static object locker = new object();

        public Form1(MainWindow window)
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.window = window;
            this.window.ev_del += new EventDelegate(message);
            this._list = new List<Sign_Object>();

            label1.Visible = false;
            label8.Visible = false;

            pdfViewer1.BackColor = Color.White;
            pdfViewer1.Renderer.MouseMove += Renderer_MouseMove;
            pdfViewer1.Renderer.MouseLeave += Renderer_MouseLeave;
            pdfViewer1.Renderer.MouseClick += Renderer_MouseClick;
            pdfViewer1.Renderer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Renderer_Right_MouseClick);
            pdfViewer1.Renderer.ZoomChanged += Renderer_ZoomChanged;
            pdfViewer1.ZoomMode = PdfViewerZoomMode.FitBest;

            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            global_path = Path.GetDirectoryName(location);
            global_path_to_xmlfile = global_path + "/xmlfile.xml";
        }
        async private void Renderer_Right_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && iconButton3.IconChar != FontAwesome.Sharp.IconChar.Eye)
            {
                var rect = pdfViewer1.Renderer.DisplayRectangle;
                Point point = new Point(rect.X, rect.Y);
                try
                {
                    Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                    pdf.LoadFromFile(MainWindow.obj.path);
                    Spire.Pdf.PdfPageBase page = pdf.Pages[0];
                    System.Drawing.Image[] images = page.ExtractImages();
                    int images_Length = images.Length - 1;
                    if (images_Length == -1)
                    {
                        MessageBox.Show(this, "В файле нет изображений.");
                        return;
                    }
                    else
                    {
                        await Task.Factory.StartNew(() =>
                        {
                            lock (locker)
                            {
                                page.DeleteImage(images.Length - 1);
                            }
                        });
                        pdf.SaveToFile(MainWindow.obj.path);
                        pdf.Dispose();
                        for (int i = 0; i < window._list.Count; i++)
                            if (window._list[i].path == MainWindow.obj.path)
                            {
                                window._list[i].sign_count = window._list[i].sign_count - 1;
                            }
                        image_output_preview(MainWindow.obj);
                        pdfViewer1.Renderer.SetDisplayRectLocation(point);
                    }
                }
                catch
                {
                    MessageBox.Show(this, "че-то не так");
                }
            }
        }
        #region События pdfViewer1
        private void Renderer_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var rect = pdfViewer1.Renderer.DisplayRectangle;
                Point point = new Point(rect.X, rect.Y);

                if (iconButton3.IconChar == FontAwesome.Sharp.IconChar.FileSignature)
                {
                    for (int i = 0; i < _list.Count; i++)
                        if (_list[i].name == listBox1.SelectedItem.ToString())
                        {
                            _list[i].Indent_mouse_from_surname(mouse_x, mouse_y);
                            textBox3.Text = _list[i].indent_X.ToString();
                            textBox4.Text = _list[i].indent_Y.ToString();
                            break;
                        }
                    Signature _signature = new Signature(this);

                    if (textBox5.Text == "" || textBox5.Text == "0" || textBox6.Text == "" || textBox6.Text == "0")
                    {
                        Warning(517, 376, 163, 87);
                        return;
                    }
                    _mouse_x = mouse_x - float.Parse(textBox5.Text) / 2;
                    _mouse_y = mouse_y + float.Parse(textBox6.Text) / 2;
                    _signature.Save(MainWindow.obj.path, _signature.Sign(MainWindow.obj.path, obj.path, ref MainWindow.obj.sign_count, _mouse_x, _mouse_y, float.Parse(textBox5.Text), float.Parse(textBox6.Text)));

                    for (int i = 0; i < window._list.Count; i++)
                        if (window._list[i].path == MainWindow.obj.path)
                        {
                            window._list[i].sign_count = MainWindow.obj.sign_count;
                        }
                    image_output_preview(MainWindow.obj);
                    pdfViewer1.Renderer.SetDisplayRectLocation(point);
                }
                if (iconButton3.IconChar == FontAwesome.Sharp.IconChar.CalendarAlt)
                {
                    for (int i = 0; i < _list.Count; i++)
                        if (_list[i].name == listBox1.SelectedItem.ToString())
                        {
                            _list[i].Indent_mouse_from_surname(mouse_x, mouse_y);
                            textBox7.Text = _list[i].indent_X.ToString();
                            textBox8.Text = _list[i].indent_Y.ToString();
                            break;
                        }
                    Signature _signature = new Signature(window, this);
                    if (textBox9.Text == "" || textBox9.Text == "0" || textBox10.Text == "" || textBox10.Text == "0")
                    {
                        Warning(517, 472, 163, 88);
                        return;
                    }
                    _mouse_x = mouse_x - float.Parse(textBox9.Text) / 2;
                    _mouse_y = mouse_y + float.Parse(textBox10.Text) / 2;
                    _signature.Save(MainWindow.obj.path, _signature.Sign(MainWindow.obj.path, obj.path, ref MainWindow.obj.sign_count, _mouse_x, _mouse_y, float.Parse(textBox9.Text), float.Parse(textBox10.Text)));
                    for (int i = 0; i < window._list.Count; i++)
                        if (window._list[i].path == MainWindow.obj.path)
                        {
                            window._list[i].sign_count = MainWindow.obj.sign_count;
                        }
                    image_output_preview(MainWindow.obj);
                    pdfViewer1.Renderer.SetDisplayRectLocation(point);
                }
            }
        }
        private void Renderer_MouseMove(object sender, MouseEventArgs e)
        {
            ShowPdfLocation(pdfViewer1.Renderer.PointToPdf(e.Location));
        }
        private void Renderer_MouseLeave(object sender, EventArgs e)
        {
            ShowPdfLocation(PdfPoint.Empty);
        }
        private void ShowPdfLocation(PdfPoint point)
        {
            if (!point.IsValid)
            {
                label1.Visible = false;
                label1.Text = null;
                label8.Visible = false;
            }
            else
            {
                label1.Visible = true;
                label8.Visible = true;
                label1.Text = point.Location.X + "   " + point.Location.Y;
                label1.Refresh();
                mouse_x = point.Location.X;
                mouse_y = point.Location.Y;
            }
        }

        private void Renderer_ZoomChanged(object sender, EventArgs e)
        {
            float _zoom = (float)pdfViewer1.Renderer.Zoom;
            label8.Text = _zoom.ToString();
        }
        #endregion

        public void message()
        {
            //MessageBox.Show(this,"Событие работает");
        }
        public void image_output_preview(Listbox_Item obj)
        {
            if (obj != null)
            {
                this.Size = new Size(710, 612);
                pdfViewer1.Visible = true;
                label1.Visible = true;
                label8.Visible = true;
                label11.Visible = true;
                label12.Visible = true;

                MemoryStream stream;
                byte[] massiv_data = null;
                PdfiumViewer.PdfDocument doc_preview = null;

                massiv_data = System.IO.File.ReadAllBytes(obj.path);
                stream = new MemoryStream(massiv_data);
                doc_preview = PdfiumViewer.PdfDocument.Load(stream);

                if (pdfViewer1.Visible == false)
                    pdfViewer1.Visible = true;

                if (pdfViewer1.Document != null)
                    pdfViewer1.Document.Dispose();

                pdfViewer1.Document = doc_preview;
                pdfViewer1.Renderer.ZoomMax = 10;
            }
            else
            {
                this.Size = new Size(284, 612);
                pdfViewer1.Visible = false;
                label1.Visible = false;
                label8.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
            }
        }
        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files;
            string _filepath;
            string new_path;

            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(location);
            Directory.CreateDirectory(path + @"\signatures\"); //создает каталоги и подкаталоги по указанному пути, если они еще не существуют

            if (listBox1.Items.Count == 0)
            {
                files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                foreach (string filepath in files)
                {
                    _filepath = Path.GetExtension(filepath);
                    if (_filepath != ".jpg" & _filepath != ".png")
                    {
                        MessageBox.Show(this, "Попытка дропнуть файл с неверным расширением");
                        goto finish;
                    }
                    new_path = path + @"\signatures\" + Path.GetFileName(filepath);
                    listBox1.Items.Add(Path.GetFileName(filepath));
                    _list.Add(new Sign_Object(this, Path.GetFileName(filepath), new_path));
                    if (File.Exists(filepath))
                        File.Copy(filepath, new_path, true);
                }
                listBox1.SelectedIndex = 0;
            }
            else
            {
                files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach (string filepath in files)
                {
                    _filepath = Path.GetExtension(filepath);
                    if (_filepath != ".jpg" & _filepath != ".png")
                    {
                        MessageBox.Show(this, "Попытка дропнуть файл с неверным расширением");
                        goto finish;
                    }
                    for (int i = 0; i < listBox1.Items.Count; i++)
                        if (Path.GetFileName(filepath) == listBox1.Items[i].ToString())
                        {
                            MessageBox.Show(this, "Попытка дропнуть уже имеющийся файл");
                            goto finish;
                        }
                    new_path = path + @"\signatures\" + Path.GetFileName(filepath);
                    listBox1.Items.Add(Path.GetFileName(filepath));
                    _list.Add(new Sign_Object(this, Path.GetFileName(filepath), new_path));
                    if (File.Exists(filepath))
                        File.Copy(filepath, new_path, true);
                }
            }
        finish:
            return;
        }
        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                return;
            }
            if (listBox1.SelectedItem != null)
                for (int i = 0; i < _list.Count; i++)
                    if (System.IO.Path.GetFileName(_list[i].name) == listBox1.SelectedItem.ToString())
                    {
                        obj = _list[i];

                        var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var path = Path.GetDirectoryName(location);
                        var new_path = path + @"\signatures\" + listBox1.Items[i].ToString();
                        pictureBox1.ImageLocation = new_path;
                        textBox2.Text = _list[i].name;
                        break;
                    }
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(global_path_to_xmlfile);
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot.ChildNodes)
                if (xnode.ChildNodes[0].InnerText == obj.name)
                {
                    textBox3.Text = xnode.ChildNodes[2].InnerText;
                    textBox4.Text = xnode.ChildNodes[3].InnerText;
                    textBox5.Text = xnode.ChildNodes[4].InnerText;
                    textBox6.Text = xnode.ChildNodes[5].InnerText;

                    textBox7.Text = xnode.ChildNodes[6].InnerText;
                    textBox8.Text = xnode.ChildNodes[7].InnerText;
                    textBox9.Text = xnode.ChildNodes[8].InnerText;
                    textBox10.Text = xnode.ChildNodes[9].InnerText;
                    break;
                }
                else
                {
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(location);
            path = path + @"\signatures\";
            Directory.CreateDirectory(path); //создает каталоги и подкаталоги по указанному пути, если они еще не существуют

            DirectoryInfo dinfo = new DirectoryInfo(path);
            FileInfo[] Files = dinfo.GetFiles();

            for (int i = 0; i < Files.Length; i++)
            {
                _list.Add(new Sign_Object(this, Files[i].Name, Files[i].FullName));
                listBox1.Items.Add(Files[i].Name);
            }
            if (listBox1.Items.Count != 0)
                listBox1.SelectedIndex = 0;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(global_path_to_xmlfile);
            XmlElement xRoot = xDoc.DocumentElement;
            for (int i = 0; i < _list.Count; i++)
            {
                foreach (XmlNode xnode in xRoot.ChildNodes)
                {
                    if (xnode.ChildNodes[0].InnerText == _list[i].name)
                    {
                        _list[i].indent_X = float.Parse(xnode.ChildNodes[2].InnerText);
                        _list[i].indent_Y = float.Parse(xnode.ChildNodes[3].InnerText);
                        _list[i].scale_X = float.Parse(xnode.ChildNodes[4].InnerText);
                        _list[i].scale_Y = float.Parse(xnode.ChildNodes[5].InnerText);
                        _list[i].date_indent_X = float.Parse(xnode.ChildNodes[6].InnerText);
                        _list[i].date_indent_Y = float.Parse(xnode.ChildNodes[7].InnerText);
                        _list[i].date_scale_X = float.Parse(xnode.ChildNodes[8].InnerText);
                        _list[i].date_scale_Y = float.Parse(xnode.ChildNodes[9].InnerText);
                        break;
                    }
                }
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            float scale = 0;
            if (textBox9.Text != "")
                scale = float.Parse(textBox9.Text) / 2;
            textBox10.Text = Convert.ToString(scale);
        }
        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            var b = this.Location;
            if (window.button_addiction_from_window.Content.ToString() == ">><<")
            {
                window.Left = b.X - window.ActualWidth;
                window.Top = b.Y;
            }
        }
        async void Warning(int x, int y, int width, int height)
        {
            await Task.Factory.StartNew(() =>
            {
                g = CreateGraphics();
                for (int i = 0; i < 3; i++)
                {
                    g.DrawRectangle(new Pen(Color.Red, 3), new Rectangle(x, y, width, height));
                    Thread.Sleep(500);
                    g.Clear(Color.White);
                    Thread.Sleep(500);
                }
            });
        }
        async void Warning_no_images(int x, int y, int width, int height)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show(this, "Для начала работы добавьте подписи, перетащив их в соответствующее окно.");
                await Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(500);
                    g = CreateGraphics();
                    for (int i = 0; i < 3; i++)
                    {
                        g.DrawRectangle(new Pen(Color.Red, 3), new Rectangle(x, y, width, height));
                        Thread.Sleep(500);
                        g.Clear(Color.White);
                        Thread.Sleep(500);
                    }
                });
            }
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
            if (MainWindow.obj != null)
                Warning_no_images(429, 3, 260, 113);
            else Warning_no_images(3, 3, 260, 113);
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Sound.Play_Sound(4);
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            Sound.Play_Sound(1);
            if (listBox1.Items.Count > 0)
                new Action(async () =>
                {
                    iconButton1.IconColor = System.Drawing.Color.Green;
                    await Task.Delay(1000);
                    iconButton1.IconColor = System.Drawing.Color.Black;
                }).Invoke();
            for (int i = 0; i < _list.Count; i++)
                if (System.IO.Path.GetFileName(_list[i].name) == listBox1.SelectedItem.ToString() && listBox1.SelectedItem.ToString() != textBox2.Text)
                {
                    var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    var path = Path.GetDirectoryName(location);
                    var old_path = path + @"\signatures\" + listBox1.Items[i];
                    var new_path = path + @"\signatures\" + textBox2.Text;

                    _list[i].path = new_path;
                    _list[i].name = textBox2.Text;
                    listBox1.Items[i] = textBox2.Text;
                    textBox2.Text = "";
                    pictureBox1.ImageLocation = new_path; //возможно надо поставить после удаления старого файла

                    MemoryStream ms = new MemoryStream();
                    using (FileStream fs = new FileStream(old_path, FileMode.Open))
                    {
                        fs.CopyTo(ms);
                        fs.Dispose();
                        File.Delete(old_path);
                        var fileStream = new FileStream(new_path, FileMode.Create, FileAccess.Write);
                        ms.WriteTo(fileStream);
                    }
                    ms.Dispose();

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(global_path_to_xmlfile);
                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlNode xnode in xRoot.ChildNodes)
                        if (float.Parse(xnode.ChildNodes[2].InnerText) == _list[i].indent_X && float.Parse(xnode.ChildNodes[3].InnerText) == _list[i].indent_Y) //находим соответствие в xml по координатам смещения
                        {
                            xnode.ChildNodes[0].InnerText = _list[i].name;
                            xnode.ChildNodes[1].InnerText = _list[i].path;
                            xRoot.RemoveAll();
                            xDoc.Save(global_path_to_xmlfile);
                            return;
                        }
                    break;
                }
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            Sound.Play_Sound(1);
            if (listBox1.Items.Count > 0)
                new Action(async () =>
                {
                    iconButton2.IconColor = System.Drawing.Color.Green;
                    await Task.Delay(1000);
                    iconButton2.IconColor = System.Drawing.Color.Black;
                }).Invoke();
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(location);
            path = path + @"\signatures\";

            for (int i = 0; i < _list.Count; i++)
                if (System.IO.Path.GetFileName(_list[i].name) == listBox1.SelectedItem.ToString())
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(global_path_to_xmlfile);
                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlNode xnode in xRoot.ChildNodes)
                        if (xnode.ChildNodes[0].InnerText == _list[i].name)
                        {
                            xRoot.RemoveChild(xnode);
                            xDoc.Save(global_path_to_xmlfile);
                            break;
                        }
                    path += _list[i].name;
                    _list.RemoveAt(i);
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    if (_list.Count == 0)
                    {
                        pictureBox1.ImageLocation = null;
                        textBox2.Text = "";
                    }
                    else listBox1.SelectedIndex = 0;
                    break;
                }
            lock (locker)
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
        }
        private void iconButton3_Click(object sender, EventArgs e)
        {
            Sound.Play_Sound(1);
            if (iconButton3.IconChar == FontAwesome.Sharp.IconChar.Eye)
            {
                iconButton3.IconChar = FontAwesome.Sharp.IconChar.FileSignature;
                if (this.pdfViewer1.Visible == true)
                    iconButton3.Location = new Point(463, 406);
                else iconButton3.Location = new Point(37, 406);
                new Action(async () =>
                {
                    iconButton3.IconColor = System.Drawing.Color.Green;
                    await Task.Delay(1000);
                    iconButton3.IconColor = System.Drawing.Color.Black;
                }).Invoke();
                return;
            }
            if (iconButton3.IconChar == FontAwesome.Sharp.IconChar.FileSignature)
            {
                iconButton3.IconChar = FontAwesome.Sharp.IconChar.CalendarAlt;
                if (this.pdfViewer1.Visible == true)
                    iconButton3.Location = new Point(458, 504);
                else iconButton3.Location = new Point(32, 504);
                new Action(async () =>
                {
                    iconButton3.IconColor = System.Drawing.Color.Green;
                    await Task.Delay(1000);
                    iconButton3.IconColor = System.Drawing.Color.Black;
                }).Invoke();
                return;
            }
            if (iconButton3.IconChar == FontAwesome.Sharp.IconChar.CalendarAlt)
            {
                iconButton3.IconChar = FontAwesome.Sharp.IconChar.Eye;
                if (this.pdfViewer1.Visible == true)
                    iconButton3.Location = new Point(458, 337);
                else iconButton3.Location = new Point(32, 337);
                new Action(async () =>
                {
                    iconButton3.IconColor = System.Drawing.Color.Green;
                    await Task.Delay(1000);
                    iconButton3.IconColor = System.Drawing.Color.Black;
                }).Invoke();
                return;
            }
        }
        private void iconButton4_Click(object sender, EventArgs e)
        {
            Sound.Play_Sound(1);
            if (iconButton3.IconChar != FontAwesome.Sharp.IconChar.Eye)
            {
                int i;
                TextBox[] textboxes = new TextBox[8]
                        { textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10 };
                for (i = 0; i < textboxes.Length; i++)
                {
                    if (textboxes[i].Text == "")
                        textboxes[i].Text = "0";
                }
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(window.global_path_to_xmlfile);
                XmlElement xRoot = xDoc.DocumentElement;

                foreach (XmlNode xnode in xRoot.ChildNodes)
                    if (xnode.ChildNodes[0].InnerText == obj.name)
                    {
                        xnode.ChildNodes[1].InnerText = obj.path;

                        xnode.ChildNodes[2].InnerText = textBox3.Text;
                        xnode.ChildNodes[3].InnerText = textBox4.Text;
                        xnode.ChildNodes[4].InnerText = textBox5.Text;
                        xnode.ChildNodes[5].InnerText = textBox6.Text;

                        xnode.ChildNodes[6].InnerText = textBox7.Text;
                        xnode.ChildNodes[7].InnerText = textBox8.Text;
                        xnode.ChildNodes[8].InnerText = textBox9.Text;
                        xnode.ChildNodes[9].InnerText = textBox10.Text;
                        xDoc.Save(global_path_to_xmlfile);
                        return;
                    }
                if (iconButton3.IconChar == FontAwesome.Sharp.IconChar.FileSignature)
                {
                    for (i = 0; i < 4; i++)
                    {
                        if (textboxes[i].Text == "0")
                        {
                            Warning(537, 376, 143, 87);
                            return;
                        }
                    }
                }
                if (iconButton3.IconChar == FontAwesome.Sharp.IconChar.CalendarAlt)
                {
                    for (i = 3; i < 8; i++)
                    {
                        if (textboxes[i].Text == "0")
                        {
                            Warning(537, 474, 143, 87);
                            return;
                        }
                    }
                }
                XmlElement list = xDoc.CreateElement("list");

                xRoot.AppendChild(list);

                XmlElement elem1 = xDoc.CreateElement("name");
                XmlElement elem2 = xDoc.CreateElement("path");

                XmlText elem1_text = null;
                XmlText elem2_text = null;

                foreach (var item in _list)
                {
                    if (item.name == listBox1.SelectedItem.ToString())
                    {
                        elem1_text = xDoc.CreateTextNode(item.name);
                        elem2_text = xDoc.CreateTextNode(item.path);
                        break;
                    }
                }
                list.AppendChild(elem1);
                list.AppendChild(elem2);

                elem1.AppendChild(elem1_text);
                elem2.AppendChild(elem2_text);

                XmlElement elem3 = xDoc.CreateElement("indent_X");
                XmlElement elem4 = xDoc.CreateElement("indent_Y");
                XmlElement elem5 = xDoc.CreateElement("scale_X");
                XmlElement elem6 = xDoc.CreateElement("scale_Y");

                XmlElement elem7 = xDoc.CreateElement("indent_date_X");
                XmlElement elem8 = xDoc.CreateElement("indent_date_Y");
                XmlElement elem9 = xDoc.CreateElement("scale_date_X");
                XmlElement elem10 = xDoc.CreateElement("scale_date_Y");

                XmlText elem3_text = xDoc.CreateTextNode(textBox3.Text);
                XmlText elem4_text = xDoc.CreateTextNode(textBox4.Text);
                XmlText elem5_text = xDoc.CreateTextNode(textBox5.Text);
                XmlText elem6_text = xDoc.CreateTextNode(textBox6.Text);

                XmlText elem7_text = xDoc.CreateTextNode(textBox7.Text);
                XmlText elem8_text = xDoc.CreateTextNode(textBox8.Text);
                XmlText elem9_text = xDoc.CreateTextNode(textBox9.Text);
                XmlText elem10_text = xDoc.CreateTextNode(textBox10.Text);

                elem3.AppendChild(elem3_text);
                elem4.AppendChild(elem4_text);
                elem5.AppendChild(elem5_text);
                elem6.AppendChild(elem6_text);

                elem7.AppendChild(elem7_text);
                elem8.AppendChild(elem8_text);
                elem9.AppendChild(elem9_text);
                elem10.AppendChild(elem10_text);

                list.AppendChild(elem3);
                list.AppendChild(elem4);
                list.AppendChild(elem5);
                list.AppendChild(elem6);

                list.AppendChild(elem7);
                list.AppendChild(elem8);
                list.AppendChild(elem9);
                list.AppendChild(elem10);

                xDoc.Save(global_path_to_xmlfile);

                new Action(async () =>
                {
                    iconButton4.IconColor = System.Drawing.Color.Green;
                    await Task.Delay(1000);
                    iconButton4.IconColor = System.Drawing.Color.Black;
                }).Invoke();
            }
            else
            {
                MessageBox.Show(this, "Чтобы добавить/обновить данные, необходимо перевести кнопку 'Режим просмотра' в интересующий Вас режим.");
                new Action(async () =>
                {
                    iconButton4.IconColor = System.Drawing.Color.Red;
                    await Task.Delay(1000);
                    iconButton4.IconColor = System.Drawing.Color.Black;
                }).Invoke();
                if (obj != null)
                {
                    Warning(455, 334, 44, 44);
                }
                else Warning(29, 334, 44, 44);
            }
        }
    }

}
