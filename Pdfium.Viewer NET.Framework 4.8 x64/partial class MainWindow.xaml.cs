using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Pdfium.Viewer_NET.Framework_4._8_x64
{
    public partial class MainWindow
    {
        private void Button_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Убрать документы из списка в listbox1.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }
        private void delete_listbox3_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Убрать готовые документы из списка в listbox3.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }
        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void button_viewing_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Открыть/Закрыть окно просмотра документов и настроек подписей/дат.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void button_viewing_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void button_addiction_from_window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Настройка положения дочернего окна:\n'>><<' Окно пристреливается к родительскому окну.\n'<<>>' Окно ведет себя свободно от родительского. ",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void button_addiction_from_window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void button4_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Ищет совпадения установленных в программе подписей и фамилий в активном документе по заданному пользователем ключевому слову, если совпадение имеет место, подпись добавляется в чертеж. Для настройки перейдите в дочернее окно.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void button4_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void button_visibility_date_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Настройка проставления даты в приложение к подписи. Нажатие показывает/скрывает textbox1, что активирует/отключает проставление даты для каждой подписи. Для настройки перейдите в дочернее окно.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void button_visibility_date_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void button6_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Активирует кнопку 'Подписать' для всех документов, представленных в listbox1.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void button6_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void button2_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Удалить все подписи/изображения в исходном активном документе.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void button2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void button_delete_all_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Активирует кнопку 'Удалить подписи' для всех документов, представленных в listbox1.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void button_delete_all_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void button_digital_sign1_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Поставить цифровую невидимую подпись в активный документ. Для подписи необходим сертификат и пароль к нему. Чтобы импортировать сертификат, нажмите кнопку настроек справа.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void button_digital_sign1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void button_digital_sign_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Настройка цифровой подписи. Нажатие показывает/скрывает поле для сертификата и пароля. Для начала работы DragandDropните сертификат в соответствующее поле и введите пароль к нему.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void button_digital_sign_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void back_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Откатывает все изменения и переводит документ в исходное состояние в listbox1, удаляя все проставленные подписи/даты.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void back_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void back_all_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Активирует кнопку 'Вернуть на доработку' для всех документов, представленных в listbox3.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void back_all_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void extractor_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Извлечь все подписи/изображения из активного документа в listbox1 по указанному пути. К имени файла каждой подписи будет добавлен порятковый номер.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void extractor_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void listBox1_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Список исходных документов без внесенных изменений. Подписи и даты, добавленные вручную, не меняют статус документа, как исходного. Принимает файлы формата 'pdf' и все файлы 'Компас 3D'",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void listBox1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void listBox2_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Список документов, находящихся на подписи/удалении внесенных изменений.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void listBox2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";

        }

        private void listBox3_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = new TextBlock
            {
                Text = "Список подписанных документов.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }
        private void listBox3_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            label3.Content = "";
        }
        private void textbox2_PreviewDragOver(object sender, System.Windows.DragEventArgs e)
        {
            e.Effects = System.Windows.DragDropEffects.All;
            e.Handled = true;
        }
        private void window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Sound.Play_Sound(4);
            System.Threading.Thread.Sleep(500);
            this.Close();
        }
        private void textbox2_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            e.Effects = System.Windows.DragDropEffects.Copy;
        }
        private void listBox1_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            e.Effects = System.Windows.DragDropEffects.Copy;
        }
    }
    public partial class Form1
    {
        private void iconButton1_MouseMove(object sender, MouseEventArgs e)
        {
            window.label3.Content = new TextBlock
            {
                Text = "Редактировать имя активной подписи, добавленной в базу данных.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }
        private void iconButton1_MouseLeave(object sender, EventArgs e)
        {
            window.label3.Content = "";
        }
        private void iconButton2_MouseMove(object sender, MouseEventArgs e)
        {
            window.label3.Content = new TextBlock
            {
                Text = "Удалить активную подпись из базы данных.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }

        private void iconButton2_MouseLeave(object sender, EventArgs e)
        {
            window.label3.Content = "";
        }
        private void iconButton4_MouseEnter(object sender, EventArgs e)
        {
            window.label3.Content = new TextBlock
            {
                Text = "Добавить/обновить подпись/дату в базе данных. Для добавления/обновления кнопка, устанавливающая режим подписи и даты, должна находиться в соответствующем положении, а координаты должны быть заполнены.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left
            };
        }
        private void iconButton4_MouseLeave(object sender, EventArgs e)
        {
            window.label3.Content = "";
        }
        private void iconButton3_MouseEnter(object sender, EventArgs e)
        {
            window.label3.Content = new TextBlock
            {
                Text = "Кнопка установки режима. Режим просмотра: Нажатие на поле просмотра не вносит изменения в документ. Режим подписи/даты: LeftClick по полю документа добавляет активную подпись/дату в место нажатия и координаты в соответствующие поля, которые можно сохранить в базу данных. RightClick удаляет подпись/дату добавленную последней.",
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Left,
                FontSize = 11
            };
        }
        private void iconButton3_MouseLeave(object sender, EventArgs e)
        {
            window.label3.Content = "";
        }
    }
}
