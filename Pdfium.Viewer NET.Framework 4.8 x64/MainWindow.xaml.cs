using Kompas6API5;
using KompasAPI7;
using Spire.Pdf;
using Spire.Pdf.Security;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Pdfium.Viewer_NET.Framework_4._8_x64
{
    public delegate void EventDelegate();
    public partial class MainWindow : Window
    {
        public event EventDelegate ev_del;
        internal protected static object locker = new object();
        Form1 form1;
        public string[] path;
        string name;
        public string password;
        string global_path;
        public string global_path_to_xmlfile;
        public List<Listbox_Item> _list;
        public static Listbox_Item obj;
        private KompasObject kompas;
        private IKompasAPIObject _IKompasAPIObject;
        private IApplication appl;
        private IDocuments _IDocuments;
        private IKompasDocument docum;

        public MainWindow()
        {
            InitializeComponent();

            textbox2.Password = Properties.Settings.Default.path;
            textbox3.Password = Properties.Settings.Default.password;

            textbox2.Visibility = Visibility.Hidden;
            textbox3.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            back.Margin = new Thickness(7, 380, 0, 0);
            back_all.Margin = new Thickness(7, 410, 0, 0);

            if (Properties.Settings.Default.path == "")
            {
                Properties.Settings.Default.path = "";
                textbox2.Password = Properties.Settings.Default.path;
            }
            if (Properties.Settings.Default.password == "")
            {
                Properties.Settings.Default.password = "";
                textbox3.Password = Properties.Settings.Default.password;
            }
            this._list = new List<Listbox_Item>();
            this.ev_del += new EventDelegate(lets_go);

            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            global_path = Path.GetDirectoryName(location);
            global_path_to_xmlfile = global_path + "/xmlfile.xml";

            FileInfo MyFile = new FileInfo(global_path_to_xmlfile);
            if (MyFile.Exists == false)
            {
                FileStream fs = MyFile.Create();
                fs.Close();
                XDocument xdoc = new XDocument();
                XElement formats = new XElement("xmlfile");

                xdoc.Add(formats);
                xdoc.Save(global_path_to_xmlfile);
            }
            textbox1.Visibility = Visibility.Visible;
            textbox1.Text = current_data_time();
        }
        public void lets_go()
        {
            if (this.form1 != null)
                form1.image_output_preview(obj);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sound.Play_Sound(4);
            if (this.form1 == null || this.form1.IsDisposed == true)
            {
                var a = new System.Windows.Point(Left, Top);
                var b = this.ActualWidth;
                form1 = new Form1(this);
                form1.Show();
                form1.Location = new System.Drawing.Point((int)a.X + (int)b, (int)a.Y);
                form1.image_output_preview(obj);
            }
            else { form1.Dispose(); }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Sound.Play_Sound(1);
            if (obj != null)
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = obj.name.Remove(obj.name.Length - 4);
                dlg.Filter = "PngFiles(.png)|*.png";

                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    string imageFileName = dlg.FileName;

                    Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                    pdf.LoadFromFile(obj.path);
                    Spire.Pdf.PdfPageBase page = pdf.Pages[0];
                    System.Drawing.Image[] images = page.ExtractImages();
                    if (images.Length == 0)
                    {
                        new Action(async () =>
                        {
                            extractor.Background = System.Windows.Media.Brushes.Red;
                            await Task.Delay(1000);
                            extractor.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
                        }).Invoke();
                        return;
                    }
                    int index = 0;
                    foreach (System.Drawing.Image image in images)
                    {
                        if (index != 0)
                            imageFileName = imageFileName.Remove(imageFileName.Length - 5);
                        else imageFileName = imageFileName.Remove(imageFileName.Length - 4);
                        imageFileName = String.Format(imageFileName + "{0}.png", index++);
                        image.Save(imageFileName, ImageFormat.Png);
                    }
                    Sound.Play_Sound(2);
                    new Action(async () =>
                    {
                        extractor.Background = System.Windows.Media.Brushes.Green;
                        await Task.Delay(1000);
                        extractor.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
                    }).Invoke();
                }
            }
            else System.Windows.MessageBox.Show(this, "Нет активного документа.");
        }
        async private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Sound.Play_Sound(1);
            int i;
            string filepath = null;
            Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
            if (listBox1.Items.Count > 0)
            {
                for (i = 0; i < _list.Count; i++)
                    if (_list[i].name == listBox1.SelectedItem.ToString())
                    {
                        filepath = _list[i].path;
                        listBox2.Items.Add(System.IO.Path.GetFileName(filepath));
                        int a = listBox1.SelectedIndex;
                        listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                        _list.RemoveAt(i);
                        if (a > -1)
                            if (a != listBox1.Items.Count)
                            {
                                listBox1.SelectedIndex = a;
                                listBox1.SelectedItem = a;
                            }
                            else
                            {
                                listBox1.SelectedIndex = a - 1;
                                listBox1.SelectedItem = a - 1;
                            }
                        else
                        {
                            obj = null;
                            if (form1 != null)
                            {
                                form1.pdfViewer1.Document.Dispose();
                                form1.pdfViewer1.Visible = false;
                            }
                        }
                        await Task.Run(() =>
                        {
                            array_img_origin(pdf, filepath);
                        });
                        break;
                    }
            }
        }
        public async void array_img_origin(Spire.Pdf.PdfDocument pdf, string filepath)
        {
            int i;
            int count = 0;
            pdf.LoadFromFile(filepath);
            Spire.Pdf.PdfPageBase page = pdf.Pages[0];
            System.Drawing.Image[] images = page.ExtractImages();
            int images_Length = images.Length - 1;
            if (images_Length == -1)
            {
                Dispatcher.Invoke(() =>
                {
                    for (i = 0; i < listBox2.Items.Count; i++)
                        if (listBox2.Items[i].ToString() == System.IO.Path.GetFileName(filepath))
                        {
                            listBox2.Items.RemoveAt(i);
                            _list.Add(new Listbox_Item(filepath));
                            listBox1.Items.Add(System.IO.Path.GetFileName(filepath));
                            listBox1.SelectedIndex = listBox1.Items.Count;

                            new Action(async () =>
                            {
                                button2.Background = System.Windows.Media.Brushes.Red;
                                await Task.Delay(3000);
                                button2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
                            }).Invoke();
                            break;
                        }
                });
                return;
            }
            else
            {
                count = -1;
                for (i = images.Length - 1; i > count; i--)
                {
                    await Task.Factory.StartNew(() =>
                    {
                        lock (locker)
                        {
                            page.DeleteImage(i);
                        }
                    });
                }
                Dispatcher.Invoke(() =>
                {
                    try
                    {
                        pdf.SaveToFile(filepath);
                        pdf.Dispose();
                    }
#pragma warning disable CS0168 // Переменная "ex" объявлена, но ни разу не использована.
                    catch (Exception ex)
#pragma warning restore CS0168 // Переменная "ex" объявлена, но ни разу не использована.
                    {
                        System.Windows.MessageBox.Show(this, "Действие не может быть выполнено, так как файл открыт в другой программе.");

                        for (i = 0; i < listBox2.Items.Count; i++)
                            if (listBox2.Items[i].ToString() == System.IO.Path.GetFileName(filepath))
                            {
                                listBox2.Items.RemoveAt(i);
                                _list.Add(new Listbox_Item(filepath));
                                listBox1.Items.Add(System.IO.Path.GetFileName(filepath));
                                listBox1.SelectedIndex = listBox1.Items.Count;
                                break;
                            }
                    }
                    finally
                    {
                        for (i = 0; i < listBox2.Items.Count; i++)
                            if (listBox2.Items[i].ToString() == System.IO.Path.GetFileName(filepath))
                            {
                                listBox2.Items.RemoveAt(i);
                                _list.Add(new Listbox_Item(filepath));
                                var item = listBox1.Items.Add(System.IO.Path.GetFileName(filepath));
                                listBox1.SelectedIndex = item;
                                listBox1.SelectedItem = item;
                                new Action(async () =>
                                {
                                    button2.Background = System.Windows.Media.Brushes.Green;
                                    await Task.Delay(1000);
                                    button2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
                                }).Invoke();
                                break;
                            }
                        Sound.Play_Sound(2);
                    }
                });
            }
        }
        public async void array_img(Spire.Pdf.PdfDocument pdf, string filepath, int sign_count)
        {
            int i;
            int count;
            try
            {
                pdf.LoadFromFile(filepath);
                Spire.Pdf.PdfPageBase page = pdf.Pages[0];
                System.Drawing.Image[] images = page.ExtractImages();
                int images_Length = images.Length - 1;

                count = images.Length - 1 - sign_count;
                if (count == 0) count = -1;
                for (i = images.Length - 1; i > count; i--)
                {
                    await Task.Factory.StartNew(() =>
                    {
                        lock (locker)
                        {
                            page.DeleteImage(i);
                        }
                    });
                }
                pdf.SaveToFile(filepath);
                pdf.Dispose();

                Dispatcher.Invoke(() =>
                {
                    for (i = 0; i < listBox2.Items.Count; i++)
                        if (listBox2.Items[i].ToString() == System.IO.Path.GetFileName(filepath))
                        {
                            listBox2.Items.RemoveAt(i);
                            var item = listBox1.Items.Add(System.IO.Path.GetFileName(filepath));
                            for (i = 0; i < _list.Count; i++)
                            {
                                if (_list[i].name == System.IO.Path.GetFileName(filepath))
                                {
                                    _list[i].sign_count = _list[i].sign_count - sign_count;
                                }
                            }
                            listBox1.SelectedIndex = item;
                            listBox1.SelectedItem = item;
                            new Action(async () =>
                            {
                                back.Background = System.Windows.Media.Brushes.Green;
                                back.Content = new TextBlock
                                {
                                    Text = "Удаление изменений",
                                    TextWrapping = TextWrapping.Wrap,
                                    TextAlignment = TextAlignment.Center
                                };
                                await Task.Delay(1000);
                                back.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
                                back.Content = new TextBlock
                                {
                                    Text = "Вернуть на доработку",
                                    TextWrapping = TextWrapping.Wrap
                                };
                            }).Invoke();
                            break;
                        }
                    Sound.Play_Sound(2);
                });
            }
            catch
            {
                Dispatcher.Invoke(() =>
                {
                    System.Windows.MessageBox.Show(this, "Действие не может быть выполнено, так как файл открыт в другой программе.");

                    for (i = 0; i < listBox2.Items.Count; i++)
                        if (listBox2.Items[i].ToString() == System.IO.Path.GetFileName(filepath))
                        {
                            listBox2.Items.RemoveAt(i);
                            listBox3.Items.Add(System.IO.Path.GetFileName(filepath));
                            listBox3.SelectedIndex = listBox3.Items.Count - 1;
                            listBox3.SelectedItem = listBox3.Items.Count - 1;
                            break;
                        }
                });
            }
        }
        async private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Sound.Play_Sound(1);
            List<Sign_Object> local_list = new List<Sign_Object>();
            if (File.Exists(global_path_to_xmlfile))
            {
                try
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(global_path_to_xmlfile);
                    XmlElement xRoot = xDoc.DocumentElement;
                    if (xRoot.ChildNodes.Count != 0)
                        foreach (XmlNode xnode in xRoot.ChildNodes)
                        {
                            if (xnode.ChildNodes.Count != 0)
                            {
                                local_list.Add(new Sign_Object(
                                      xnode.ChildNodes[0].InnerText,
                                      xnode.ChildNodes[1].InnerText,

                                      float.Parse(xnode.ChildNodes[2].InnerText),
                                      float.Parse(xnode.ChildNodes[3].InnerText),
                                      float.Parse(xnode.ChildNodes[4].InnerText),
                                      float.Parse(xnode.ChildNodes[5].InnerText),

                                      float.Parse(xnode.ChildNodes[6].InnerText),
                                      float.Parse(xnode.ChildNodes[7].InnerText),
                                      float.Parse(xnode.ChildNodes[8].InnerText),
                                      float.Parse(xnode.ChildNodes[9].InnerText))
                                      );
                            }
                        }
                    else
                    {
                        new Action(async () =>
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                button_viewing.Background = System.Windows.Media.Brushes.Green;
                                await Task.Delay(500);
                                button_viewing.Background = System.Windows.Media.Brushes.Red;
                                await Task.Delay(500);
                            }
                            button_viewing.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
                        }).Invoke();
                        System.Windows.MessageBox.Show(this, "В базе данных еще нет подписей или дат.\nПоставьте подписи вручную и сохраните в базу данных.");
                        return;
                    }
                }
                catch { System.Windows.MessageBox.Show(this, "Добавьте подписи в базу данных."); return; }
            }
            string filename;
            string filepath;
            int i;
            if (listBox1.Items.Count != 0)
            {
                for (i = 0; i < _list.Count; i++)
                    if (_list[i].name == listBox1.SelectedItem.ToString())
                    {
                        filename = _list[i].name;
                        filepath = _list[i].path;
                        listBox2.Items.Add(_list[i].name);
                        int a = listBox1.SelectedIndex;
                        listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                        //_list.RemoveAt(i);

                        if (a > -1)
                            if (a != listBox1.Items.Count)
                            {
                                listBox1.SelectedIndex = a;
                                listBox1.SelectedItem = a;
                            }
                            else
                            {
                                listBox1.SelectedIndex = a - 1;
                                listBox1.SelectedItem = a - 1;
                            }
                        else
                        {
                            obj = null;
                            if (form1 != null)
                            {
                                form1.pdfViewer1.Document.Dispose();
                                form1.pdfViewer1.Visible = false;
                                for (i = 0; i < _list.Count; i++)
                                    if (_list[i].name == listBox1.SelectedItem.ToString())
                                    {
                                        obj = _list[i];
                                        ev_del.Invoke();
                                        break;
                                    }
                            }
                        }
                        Signature _signature = new Signature(this);
                        await Task.Factory.StartNew(() =>
                        {
                            lock (locker)
                            {
                                try
                                {
                                    _signature.Save(_list[i].path, _signature.Sign(_list[i], local_list, _list[i].path, ref _list[i].sign_count));
                                    Dispatcher.Invoke(() =>
                                    {
                                        for (i = 0; i < listBox2.Items.Count; i++)
                                            if (listBox2.Items[i].ToString() == filename)
                                            {
                                                listBox2.Items.RemoveAt(i);
                                                var item = listBox3.Items.Add(filename);
                                                listBox3.SelectedIndex = item;
                                                listBox3.SelectedItem = item;
                                                break;
                                            }
                                        new Action(async () =>
                                        {
                                            Sound.Play_Sound(2);
                                            button4.Background = System.Windows.Media.Brushes.Green;
                                            await Task.Delay(1000);
                                            button4.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
                                        }).Invoke();
                                    });
                                }
                                catch
                                {
                                    Dispatcher.Invoke(() =>
                                    {
                                        System.Windows.MessageBox.Show(this, "Действие не может быть выполнено, так как файл открыт в другой программе.");
                                        for (i = 0; i < listBox2.Items.Count; i++)
                                            if (listBox2.Items[i].ToString() == filename)
                                            {
                                                listBox2.Items.RemoveAt(i);
                                                var item = listBox1.Items.Add(filename);
                                                listBox1.SelectedIndex = item;
                                                listBox1.SelectedItem = item;
                                                break;
                                            }
                                    });
                                }
                            }
                        });
                        break;
                    }
            }
        }
        string current_data_time()
        {
            string _date = Convert.ToString(DateTime.Today);
            char[] ar = _date.ToCharArray();
            int i;
            string new_date = null;
            for (i = 0; i < _date.Length - 8; i++)
                if (i != _date.Length - 12 && i != _date.Length - 11)
                    new_date = new_date + Convert.ToString(ar[i]);
            return new_date;
        }
        async private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Sound.Play_Sound(1);
            int i;
            string filepath = null;
            int sign_count = 0;
            if (listBox3.Items.Count > 0)
            {
                Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                for (i = 0; i < _list.Count; i++)
                    if (_list[i].name == listBox3.SelectedItem.ToString())
                    {
                        filepath = _list[i].path;
                        sign_count = _list[i].sign_count;

                        listBox2.Items.Add(System.IO.Path.GetFileName(filepath));
                        int a = listBox3.SelectedIndex;
                        listBox3.Items.RemoveAt(listBox3.SelectedIndex);
                        //_list.RemoveAt(i);

                        if (a > -1)
                            if (a != listBox3.Items.Count)
                            {
                                listBox3.SelectedIndex = a;
                                listBox3.SelectedItem = a;
                            }
                            else
                            {
                                listBox3.SelectedIndex = a - 1;
                                listBox3.SelectedItem = a - 1;
                            }
                        else
                        {
                            obj = null;
                            if (form1 != null)
                            {
                                form1.pdfViewer1.Document.Dispose();
                                form1.pdfViewer1.Visible = false;
                            }
                        }
                        await Task.Run(() =>
                        {
                            array_img(pdf, filepath, sign_count);
                        });
                        break;
                    }
            }
        }
        private void listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                for (int i = 0; i < _list.Count; i++)
                    if (System.IO.Path.GetFileName(_list[i].path) == listBox1.SelectedItem.ToString())
                    {
                        obj = _list[i];
                        ev_del.Invoke();
                        break;
                    }
            }
            else if (listBox1.Items.Count == 0) { obj = null; ev_del.Invoke(); }
        }
        private void listBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                for (int i = 0; i < _list.Count; i++)
                    if (_list[i].name == listBox3.SelectedItem.ToString())
                    {
                        obj = _list[i];
                        ev_del.Invoke();
                        break;
                    }
            }
            else if (listBox1.Items.Count == 0) { obj = null; ev_del.Invoke(); }
        }
        private void listBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                for (int i = 0; i < _list.Count; i++)
                    if (_list[i].name == listBox1.SelectedItem.ToString())
                    {
                        obj = _list[i];
                        ev_del.Invoke();
                        break;
                    }
            }
            else if (listBox1.Items.Count == 0) { obj = null; ev_del.Invoke(); }
        }
        private void listBox3_GotFocus(object sender, RoutedEventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                if (listBox3.SelectedItem != null)
                    for (int i = 0; i < _list.Count; i++)
                        if (_list[i].name == listBox3.SelectedItem.ToString())
                        {
                            obj = _list[i];
                            ev_del.Invoke();
                            break;
                        }
            }
            else if (listBox1.Items.Count == 0) { obj = null; ev_del.Invoke(); }
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Sound.Play_Sound(1);
            if (_list.Count > 0)
                foreach (Listbox_Item item in _list)
                {
                    if (listBox1.Items.Count > 0)
                    {
                        if (item.name == listBox1.SelectedItem.ToString())
                        {
                            int i = listBox1.SelectedIndex;


                            _list.Remove(item);
                            listBox1.Items.Remove(listBox1.SelectedItem);
                            if (i - 1 > -1)
                            {
                                listBox1.SelectedIndex = i - 1;
                                listBox1.SelectedItem = i - 1;
                            }
                            else
                            {
                                listBox1.SelectedIndex = i;
                                listBox1.SelectedItem = i;
                                obj = null;
                                if (form1 != null)
                                {
                                    form1.pdfViewer1.Document.Dispose();
                                    form1.pdfViewer1.Visible = false;
                                }
                            }
                            break;
                        }
                    }
                }
        }
        async private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                await Task.Factory.StartNew(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        int count = listBox1.Items.Count;
                        for (int i = 0; i < count; i++)
                        {
                            Button_Click_3(button6, e);
                        }
                    });
                });
                button6.Background = System.Windows.Media.Brushes.Green;
                new Action(async () =>
                {
                    await Task.Delay(1000);
                    button6.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
                }).Invoke();
                obj = null;
                if (form1 != null)
                {
                    form1.pdfViewer1.Document.Dispose();
                    form1.pdfViewer1.Visible = false;
                    if (listBox1.Items.Count != 0)
                        for (int i = 0; i < _list.Count; i++)
                            if (_list[i].name == listBox1.SelectedItem.ToString())
                            {
                                obj = _list[i];
                                ev_del.Invoke();
                                break;
                            }
                }
            }
        }
        private void button_visibility_date_Click(object sender, RoutedEventArgs e)
        {
            Sound.Play_Sound(4);
            if (textbox1.Visibility == Visibility.Visible)
            {
                textbox1.Visibility = Visibility.Hidden;
                textbox1.Text = "";
                button6.Margin = new Thickness(7, 39, 0, 0);
                button_delete_all.Margin = new Thickness(7, 99, 0, 0);
                button2.Margin = new Thickness(7, 69, 0, 0);
                extractor.Margin = new Thickness(7, 129, 0, 0);
            }
            else
            {
                textbox1.Visibility = Visibility.Visible;
                textbox1.Text = current_data_time();
                button6.Margin = new Thickness(7, 61, 0, 0);
                button_delete_all.Margin = new Thickness(7, 121, 0, 0);
                button2.Margin = new Thickness(7, 91, 0, 0);
                extractor.Margin = new Thickness(7, 151, 0, 0);
            }
        }
        async private void listBox1_Drop(object sender, System.Windows.DragEventArgs e)
        {
            int i;
            string path;
            string[] files;
            List<string> a = null;
            bool kompas_pdf = false;

            await Task.Factory.StartNew(() =>
            {
                files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop, false);
                for (int i = 0; i < files.Length; i++)
                    if (System.IO.Path.GetExtension(files[i]) != ".cdw" & System.IO.Path.GetExtension(files[i]) != ".spw" & System.IO.Path.GetExtension(files[i]) != ".frw" & System.IO.Path.GetExtension(files[i]) != ".dwg" & System.IO.Path.GetExtension(files[i]) != ".kdw")
                    { return; }
                a = Open_in_Kompas(files);
                kompas_pdf = true;
            });
            if (a != null)
            {
                foreach (string filepath in a)
                {
                    for (i = 0; i < _list.Count; i++)
                    {
                        if (_list[i].path == filepath)
                        {
                            System.Windows.MessageBox.Show(this, "Такой файл уже был импортирован из Компаса, он будет обновлен без добавления в список.");
                            return;
                        }
                    }
                    this._list.Add(new Listbox_Item(filepath));
                    listBox1.Items.Add(System.IO.Path.GetFileName(filepath));
                }
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
            if (_list.Count == 0 && kompas_pdf == false)
            {
                files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop, false);
                foreach (string filepath in files)
                {
                    path = System.IO.Path.GetExtension(filepath);
                    if (path != ".pdf" & path != ".PDF")
                    {
                        System.Windows.MessageBox.Show(this, "Неверный формат файла.");
                        goto finish;
                    }
                }
                foreach (string filepath in files)
                    this._list.Add(new Listbox_Item(filepath));
                for (i = 0; i < _list.Count; i++)
                    listBox1.Items.Add(System.IO.Path.GetFileName(_list[i].path));

                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
            else
             if (kompas_pdf == false)
            {
                files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop, false);

                foreach (string filepath in files)
                {
                    path = System.IO.Path.GetExtension(filepath);
                    if (path != ".pdf" & path != ".PDF")
                    {
                        System.Windows.MessageBox.Show(this, "Неверный формат файла.");
                        goto finish;
                    }
                    for (i = 0; i < _list.Count; i++)
                        if (_list[i].name == System.IO.Path.GetFileName(filepath))
                        {
                            System.Windows.MessageBox.Show(this, "Файл уже содержится в списке.");
                            goto finish;
                        }
                }
                foreach (string filepath in files)
                {
                    this._list.Add(new Listbox_Item(filepath));
                    listBox1.Items.Add(System.IO.Path.GetFileName(_list[_list.Count - 1].path));
                }
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        finish:
            return;
        }
        public List<string> Open_in_Kompas(string[] files)
        {
            string[] _files = files;
            var tmp = new List<string>(files);
            var files_to_pdf = new List<string>();
            try
            {
                _IKompasAPIObject = (IKompasAPIObject)Marshal.GetActiveObject("KOMPAS.Application.7");
                appl = (IApplication)_IKompasAPIObject.Application;
                appl.HideMessage = Kompas6Constants.ksHideMessageEnum.ksHideMessageYes;
                appl.Visible = true;
                _IDocuments = (IDocuments)appl.Documents;
                IDocuments a = (IDocuments)_IDocuments[_IDocuments.Count];
                foreach (string filepath in files)
                {
                    for (int i = 0; i < appl.Documents.Count; i++)
                    {
                        if (appl.Documents[i].PathName == filepath)
                        {
                            _files = _files.Where(val => val != filepath).ToArray();

                            //_IDocuments.Open(filepath, true, false);
                            appl.ActiveDocument = appl.Documents[i];
                            docum = (IKompasDocument)appl.ActiveDocument;
                            docum.SaveAs(docum.PathName.Remove(docum.PathName.Length - 4) + ".pdf");
                            var path = docum.PathName.Remove(docum.PathName.Length - 4) + ".pdf";
                            files_to_pdf.Add(path);
                            //docum.Close(0);
                            break;
                        }
                    }
                }
                foreach (string _filepath in _files)
                {
                    _IDocuments.Open(_filepath, true, false);
                    docum = (IKompasDocument)appl.ActiveDocument;
                    docum.SaveAs(docum.PathName.Remove(docum.PathName.Length - 4) + ".pdf");
                    var path = docum.PathName.Remove(docum.PathName.Length - 4) + ".pdf";
                    files_to_pdf.Add(path);
                    docum.Close(0);
                }
            }
            catch
            {
                //Dispatcher.Invoke(() => this.Hide());
                kompas = null;
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                kompas = (KompasObject)Activator.CreateInstance(t);
                //kompas.Visible = true;
                kompas.ActivateControllerAPI();
                System.Threading.Thread.Sleep(100);
                _IKompasAPIObject = (IKompasAPIObject)Marshal.GetActiveObject("KOMPAS.Application.7");
                appl = (IApplication)_IKompasAPIObject.Application;
                appl.HideMessage = Kompas6Constants.ksHideMessageEnum.ksHideMessageYes;
                appl.Visible = false;
                _IDocuments = (IDocuments)appl.Documents;
                foreach (string filepath in files)
                {
                    _IDocuments.Open(filepath, true, false);
                    docum = (IKompasDocument)appl.ActiveDocument;
                    docum.SaveAs(docum.PathName.Remove(docum.PathName.Length - 4) + ".pdf");
                    var path = docum.PathName.Remove(docum.PathName.Length - 4) + ".pdf";
                    files_to_pdf.Add(path);
                    docum.Close(0);
                }
                kompas.Quit();
            }
            return files_to_pdf;
        }
        private void window_LocationChanged(object sender, EventArgs e)
        {
            var a = new System.Windows.Point(Left, Top);
            var b = this.ActualWidth;
            if (form1 != null & button_addiction_from_window.Content.ToString() == ">><<")
                form1.Location = new System.Drawing.Point((int)a.X + (int)b + 0, (int)a.Y);
        }
        private void button_addiction_from_window_Click(object sender, RoutedEventArgs e)
        {
            Sound.Play_Sound(1);
            if (button_addiction_from_window.Content.ToString() == ">><<")
                button_addiction_from_window.Content = "<<>>";
            else
            {
                button_addiction_from_window.Content = ">><<";
                var a = new System.Windows.Point(Left, Top);
                var b = this.ActualWidth;
                if (form1 != null)
                    form1.Location = new System.Drawing.Point((int)a.X + (int)b, (int)a.Y);
            }
        }
        async private void button_delete_all_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                await Task.Factory.StartNew(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        int count = listBox1.Items.Count;
                        for (int i = 0; i < count; i++)
                        {
                            Button_Click_2(button_delete_all, e);
                        }
                    });
                });
                button_delete_all.Background = System.Windows.Media.Brushes.Green;
                new Action(async () =>
                {
                    await Task.Delay(1000);
                    button_delete_all.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
                }).Invoke();
                obj = null;
                if (form1 != null)
                {
                    form1.pdfViewer1.Document.Dispose();
                    form1.pdfViewer1.Visible = false;
                    for (int i = 0; i < _list.Count; i++)
                        if (_list[i].name == listBox1.SelectedItem.ToString())
                        {
                            obj = _list[i];
                            ev_del.Invoke();
                            break;
                        }
                }
            }
        }
        async private void back_allClick_7(object sender, RoutedEventArgs e)
        {
            Sound.Play_Sound(1);
            if (listBox3.Items.Count != 0)
            {
                await Task.Factory.StartNew(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        int count = listBox3.Items.Count;
                        for (int i = 0; i < count; i++)
                        {
                            Button_Click_4(back_all, e);
                        }
                    });
                });
                back_all.Background = System.Windows.Media.Brushes.Green;
                new Action(async () =>
                {
                    await Task.Delay(1000);
                    back_all.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
                }).Invoke();
                obj = null;
                if (form1 != null)
                {
                    form1.pdfViewer1.Document.Dispose();
                    form1.pdfViewer1.Visible = false;
                    if (listBox3.Items.Count != 0)
                        for (int i = 0; i < _list.Count; i++)
                            if (_list[i].name == listBox3.SelectedItem.ToString())
                            {
                                obj = _list[i];
                                ev_del.Invoke();
                                break;
                            }
                }
            }
        }
        private void button_digital_sign_Click(object sender, RoutedEventArgs e)
        {
            Sound.Play_Sound(4);
            if (textbox2.Visibility == Visibility.Visible)
            {
                textbox2.Visibility = Visibility.Hidden;
                textbox3.Visibility = Visibility.Hidden;
                label1.Visibility = Visibility.Hidden;
                label2.Visibility = Visibility.Hidden;
                back.Margin = new Thickness(7, 380, 0, 0);
                back_all.Margin = new Thickness(7, 410, 0, 0);
            }
            else
            {
                textbox2.Visibility = Visibility.Visible;
                textbox3.Visibility = Visibility.Visible;
                label1.Visibility = Visibility.Visible;
                label2.Visibility = Visibility.Visible;
                back.Margin = new Thickness(7, 477, 0, 0);
                back_all.Margin = new Thickness(7, 507, 0, 0);
            }
        }
        private void button_digital_sign1_Click_7(object sender, RoutedEventArgs e)
        {
            Sound.Play_Sound(1);
            if (MainWindow.obj != null)
            {
                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(MainWindow.obj.path);
                PdfCertificate cert = new PdfCertificate(Properties.Settings.Default.path, Properties.Settings.Default.password);
                PdfSignature signature = new PdfSignature(doc, doc.Pages[doc.Pages.Count - 1], cert, "gennady");
                signature.DocumentPermissions = PdfCertificationFlags.ForbidChanges | PdfCertificationFlags.AllowFormFill;
                doc.SaveToFile(MainWindow.obj.path);
                doc.Close();
                Sound.Play_Sound(2);
                new Action(async () =>
                {
                    Sound.Play_Sound(2);
                    button_digital_sign1.Background = System.Windows.Media.Brushes.Green;
                    await Task.Delay(1000);
                    button_digital_sign1.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
                }).Invoke();
            }
        }
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            textbox2.Password = Properties.Settings.Default.path;
            textbox3.Password = Properties.Settings.Default.password;
            if (Properties.Settings.Default.path == "")
            {
                Properties.Settings.Default.path = "";
                textbox2.Password = Properties.Settings.Default.path;
            }
            if (Properties.Settings.Default.password == "")
            {
                Properties.Settings.Default.password = "";
                textbox3.Password = Properties.Settings.Default.password;
            }
        }
        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Sound.Play_Sound(4);
            Properties.Settings.Default.password = textbox3.Password;
            Properties.Settings.Default.Save();
        }
        private void textbox2_PreviewDrop(object sender, DragEventArgs e)
        {
            Sound.Play_Sound(4);
            e.Handled = true;
            path = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string extension = System.IO.Path.GetExtension(path[0]);
            if (extension == ".pfx")
            {
                name = System.IO.Path.GetFileName(path[0]);
                textbox2.Password = name;
                Properties.Settings.Default.path = textbox2.Password;
                Properties.Settings.Default.Save();
            }
            else
            {
                new Action(async () =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        textbox2.Background = System.Windows.Media.Brushes.Red;
                        await Task.Delay(500);
                        textbox2.Background = System.Windows.Media.Brushes.White;
                        await Task.Delay(500);
                    }
                }).Invoke();
            }
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Sound.Play_Sound(1);
            if (_list.Count > 0)
                foreach (Listbox_Item item in _list)
                {
                    if (listBox3.Items.Count > 0)
                    {
                        if (item.name == listBox3.SelectedItem.ToString())
                        {
                            int i = listBox3.SelectedIndex;

                            _list.Remove(item);
                            listBox3.Items.Remove(listBox3.SelectedItem);
                            if (i - 1 > -1)
                            {
                                listBox3.SelectedIndex = i - 1;
                                listBox3.SelectedItem = i - 1;
                            }
                            else
                            {
                                listBox3.SelectedIndex = i;
                                listBox3.SelectedItem = i;
                                obj = null;
                                if (form1 != null)
                                {
                                    form1.pdfViewer1.Document.Dispose();
                                    form1.pdfViewer1.Visible = false;
                                }
                            }
                            break;
                        }
                    }
                }
        }
    }
}
