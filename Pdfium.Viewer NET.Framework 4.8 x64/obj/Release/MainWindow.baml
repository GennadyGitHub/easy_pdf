   M S B A M L   `   `   `  ���� ^  ZPdfium.Viewer NET.Framework 4.8 x64, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:Pdfium.Viewer_NET.Framework_4._8_x64$Pdfium.Viewer_NET.Framework_4._8_x64  5    /Pdfium.Viewer_NET.Framework_4._8_x64.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Dlocal2clr-namespace:Pdfium.Viewer_NET.Framework_4._8_x64     -     �Name$  window�� � Title$ 
MainWindow��$	��607��$	��574�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�
������  � AllowsTransparency . $��0,0,0,0q�$��Center=�$��Center=�.����  �� CornerRadius$ 5��$��7q����	#FF3D3A86$��0,0,0,0q�$	��607��$	��574��$
��Left=�$	��Top=�._�� .+��  � RowDefinitions �� $��30 ��� .+�� ���	#FF20205F$	��560��$
��Left=�$	��Top=�$��30��.+��� $*��$Pdfium Viewer x64 .NET Framework 4.8��$
��Left=�$��16,0,0,0q�$��Center=�$	��343��$��25��$��Dosis ExtraLight)�$��Italic$�$��0,0,0,0q�$��Center=�$��20(����	#FF49F71A� ���	#FF3D3A86$��1��$	��563��$	��560��$
��Left=�$	��Top=�$��0,0,0,0q� � ColumnDefinitions �� $��3* ��� $
��572* �.+��� -   	 ��Name$	 label1��$��1��$��Сертификат:��$
��Left=�$��
23,378,0,0q�$	��Top=�$��20��$��70��$��0,0,0,0q�$��Dosis ExtraLight)�$��Center=�
�������G� -   
 G�Name$
 textbox2��   PasswordBox#��[  $��1��$
��Left=�$��
17,398,0,0q�$	��Top=�$	��120����.  $��17��G� -   $
 textbox3��#��[  $��1��$
��Left=�$��
17,438,0,0q�$	��Top=�$	��120��|�.  ��.  $��17���� -   $	 label3��$�� ��$
��Left=�$��	0,186,0,0q�$	��Top=�
�������$��2��$	��158��$	��199��$��Dosis ExtraLight)��� -    ��Name$ listBox2��  RoundButton#��[ $��205,230,0,0q�|�.   �� SelectedIndex$ 0��$	��300��$	��110��$ �� IsSynchronizedWithCurrentItem$
 TrueY� �� IsTextSearchEnabled .  $��2��$
��Left=�$	��Top=�$��Dosis ExtraLight)�$��1���� -    ��Name$ 	extractor��#��[ $
��Left=�$��	7,151,0,0q�$	��Top=�$��30��$	��160��|�.  $��Dosis ExtraLight)�$��1����.  .��� $��Center=�$��Center=�$��0,0,0,0q�$��25�� �� $��25 ���  �� $��25 ��� $	��125 �.+��� $��1��$��25��$��25��$��Images/extract.png��$
��Fill=�$��Center=�$��Center=�$��	0,-25,0,0q��� $
��Wrap=�$��30�� �� TextAlignment$ Center=�$;��5Извлечь подписи из документа��$	��125��$��Center=�$��Center=� �� Padding$ 0,0,0,0q�$��1��$��-1,0,0,0q���Z� �� -   $ button2��#��[ $
��Left=�$	��Top=�$��25��$	��160��|�.  $��7,91,0,0q� J� RenderTransformOrigin$ 	0.5,0.551&�$��Dosis ExtraLight)�$��1����.  .��� $��Center=�$��Center=�$��0,0,0,0q�$��25�� �� $��25 ���  �� $��25 ��� $	��125 �.+��� $��1��$��21��$��21��$��Images/delete.png��$
��Fill=�$��Center=�$��Center=�$��	0,-25,0,0q��� $
��Wrap=�$��15��$ Center=�$#��Удалить подписи��$	��125��$��Center=�$��Center=�$ 0,0,0,0q�$��1����Z� �� -	   $ button4��#��[ $
��Left=�$��7,9,0,0q�$	��Top=�$��25��$	��160��$��1,1,1,1q�$��Dosis ExtraLight)�$��1����.  $��1��$��2��.��� $��Center=�$��Center=�$��0,0,0,0q�$��25�� �� $��25 ���  �� $��25 ��� $	��125 �.+��� $��1��$��25��$��25��$��Images/signature.png��$
��Fill=�$��Center=�$��Center=�$��	0,-25,0,0q��� $
��Wrap=�$��15��$ Center=�$��Подпись��$	��126��$��Right=�$��Center=�$ 0,0,0,0q�$��2����Z� �� -
    ��Name$ textbox1��#��[ $
��Left=�$��	27,39,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��120��$��Dosis ExtraLight)�$��1����.  $��17��$��Center=��� -   $ listBox3��#��[ $��205,350,0,0q�$ 0��$	��210��$	��Top=�$
��Left=�$	��300��$
 TrueY�|�.  $��3�� .  $��Dosis ExtraLight)�$��1���� -   $
 back��#��[ $
��Left=�$��	7,477,0,0q�$	��Top=�$��25��$	��160��$��Dosis ExtraLight)�$��1����.  .��� $��Center=�$��Center=�$��0,0,0,0q�$��25�� �� $��25 ���  �� $��25 ��� $	��125 �.+��� $��1��$��25��$��25��$��Images/back.png��$
��Fill=�$��Center=�$��Center=�$��	0,-25,0,0q��� $
��Wrap=�$��15��$ Center=�$,��&Вернуть на доработку��$	��125��$��Center=�$��Center=�$ 0,0,0,0q�$��1��$��-1,0,0,0q���Z� �� -   #��[ $
��Left=�$��	518,9,0,0q�$	��Top=�$��35��$��35��$��Dosis ExtraLight)�$��Normal$�$��Normal"�$	 0,0&���.  $��0,0,0,0q�$��1��.���� $��33��$��33��$��Images/transfer.png��$
��Fill=��� -   $ button6��#��[ $
��Left=�$��7,61,0,0q�$	��Top=�$��25��$	��160��$��1����.  $��Dosis ExtraLight)�.��� $��Center=�$��Center=�$��0,0,0,0q�$��25�� �� $��25 ���  �� $��25 ��� $	��125 �.+��� $��1��$��21��$��21��$��Images/signatures.png��$
��Fill=�$��Center=�$��Center=�$��	0,-25,0,0q��� $
��Wrap=�$��15��$ Center=�$%��Пакетная подпись��$	��125��$��Center=�$��Center=�$ 0,0,0,0q�$��1����Z� �� -   $ button_visibility_date��#��[ $�� ��$
��Left=�$��	172,9,0,0q�$	��Top=�$��25��$��20��$��1����.  $��0���� -   $ listBox1��#��[ $��	205,9,0,0q�$	��210��$
��Left=�$	��Top=�$	��300�� J� 	AllowDrop . |�.   .  $��1��$
 TrueY�$ 0��$��Dosis ExtraLight)�$��1���� -   $ button_viewing��#��[ $�� ��$
��Left=�$��
518,56,0,0q�$	��Top=�$	��116��$��35��$��Dosis ExtraLight)�$��1����.  �� -   $" button_addiction_from_window��#��[ $
��>><<��$
��Left=�$��518,184,0,0q�$	��Top=�$��35��$��35��$��Dosis ExtraLight)�$��1����.  $��
-0.4,0,0,0q�$
��Bold"�$��Normal&��� -   $ delete_listbox3��#��[ $��1��$
��Left=�$��518,350,0,0q�$	��Top=�$��35��$��35��$��1��$��0,0,0,0q�.���� $��33��$��33��$��Images/transfer.png��$
��Fill=��� -   $ button_delete_all��#��[ $
��Left=�$��	7,121,0,0q�$	��Top=�$	��160��$��25��$��Dosis ExtraLight)�$��1����.  .��� $��Center=�$��Center=�$��0,0,0,0q�$��25�� �� $��25 ���  �� $��25 ��� $	��125 �.+��� $��1��$��21��$��21��$��Images/deletes.png��$
��Fill=�$��Center=�$��Center=�$��	0,-25,0,0q��� $
��Wrap=�$��15��$ Center=�$'��!Пакетное удаление��$	��125��$��Center=�$��Center=�$ 0,0,0,0q�$��1��$��-1,0,0,0q���Z� �� -   $ back_all��#��[ $
��Left=�$��	7,507,0,0q�$	��Top=�$	��160��$��25��$��Dosis ExtraLight)�$��1����.  .��� $��Center=�$��Center=�$��0,0,0,0q�$��25�� �� $��25 ���  �� $��25 ��� $	��125 �.+��� $��1��$��25��$��25��$��Images/back_all.png��$
��Fill=�$��Center=�$��Center=�$��	0,-25,0,0q��� $
��Wrap=�$��15��$ Center=�$#��Вернуть пакетом��$	��125��$��Center=�$��Center=�$ 0,0,0,0q�$��1��$��-1,0,0,0q���Z� �� -   $ button_digital_sign1��#��[ $
��Left=�$��	7,350,0,0q�$	��Top=�$��25��$	��160��$��Dosis ExtraLight)�$��1����.  .��� $��Center=�$��Center=�$��0,0,0,0q�$��25�� �� $��25 ���  �� $��25 ��� $	��125 �.+��� $��1��$��21��$��21��$��Images/digital_sign.png��$
��Fill=�$��Center=�$��Center=�$��	0,-25,0,0q��� $
��Wrap=�$��15��$ Center=�$%��Цифровая подпись��$	��125��$��Center=�$��Center=�$ 0,0,0,0q�$��1��$��-1,0,0,0q���Z� �� -   $ button_digital_sign��#��[ $
��Left=�$��172,350,0,0q�$	��Top=�$��25��$��20��$��Dosis ExtraLight)�$��1����.  �� -   $
 exit��  CloseButton#���    Button_Close#���  $��0q�
������ $
��Left=�$��Center=�$��26��$��24��$��	524,0,0,0q�����  �� ImageSource$ Images/default.png�� j� Stretch$
 Fill=��� -   $	 label2��$��Пароль:��$
��Left=�$��
26,448,0,0q�$	��Top=�$��20��$��70��$��0,0,0,0q�$��Dosis ExtraLight)�$��Center=�
������� � 	Resources  
 Images  Button.MouseOver.Background �� Color  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border 	 Button.Disabled.Background 
 Button.Disabled.Border  Button.Disabled.Foreground �� 
TargetType ��Name J� SnapsToDevicePixels ��Name �� Triggers P� Property  P� Value! �� 
TargetName" �� Value# �� Property��%0  &	       &	 '     &	 <     &	 Q     &	 f     &		 {     &	
 �     &	 �     &	 �     7  7  7  7  7 	 7 
 7  �� $ Images/close.png��$
 Fill=��� $ 	#FFBEE6FD���� $ 	#FF3C7FB1���� $ 	#FFC4E5F6���� $ 	#FF2C628B���� $ 	#FFF4F4F4���� $ 	#FFADB2B5���� $ 	#FF838383���� " ��.R��� $ border��#��z��#��z��#��z�� . ._��� $ contentPresenter��|�.  #��z��#��z����. # z #��z�� P�  �@��IsDefaulted  . . ��� $! border��#" � ��# � ��P�  �@J�IsMouseOver  . . ��� $! border��8"   # � ���� $! border��8"  # � ��P�  � ��  . . ��� $! border��8"  # � ���� $! border��8"  # � ��P�  � {�  .  . ��� $! border��8"  # � ���� $! border��8"  # � ���� $! contentPresenter��8"  # � ��