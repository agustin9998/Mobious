   M S B A M L   `   `   `  ���� D  @Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Interface	Interface  !    Interface.AddMovieDirectory   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:Interface       � WindowStyle$
  None=� � AllowsTransparency .  � Title$ AddMovieDirectory��$	��150��$	��450�� � 	Resources    RoundedButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name	 �� CornerRadius
 J� SnapsToDevicePixels ��Name��%�   &	        �� " ��.���  � �� �� " ��.R��� $ border��$	 4��#��z��#��z��#��z��
 . $��0,0,-2.25,-2.25q�._��� $ contentPresenter��|�.  #��z��#��z����. #
 z
 #��z��.��� .+��� -    ��Name$ MovieBackground�����	#FF1B1B1B$��3�����	#FF4F4F4F�� -    ��Name$ BtnExit��$��10��$��0,3.75,6.25,0q�$	��Top=����	#FFC13333$��Right=�$��10��#���    � Cursor$
 Hand���� -    ��Name$ TBMoviePath��$
��Left=�$��35��$��	10,49,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��430��$��18(����	#FF1D1D1D���	#FF404040���	#FFB0B0B0�� -   $��Apply��$��Center=�$	��Top=�$��75��$��	0,110,0,0q����	#FF1D1D1D���	#FF404040���	#FFB0B0B0�� -    ��Name$ LblError��$y�Hidden=�$3��-The chosen path doesn't exists or is invalid.��$��Center=�$	��Top=� J� RenderTransformOrigin$ 2.525,3.279&�$��0,79,0,0q�$
��auto��
���  ��$��16(��� $��Movies directory path:�����	#FFFF8D05$��18(�$��10,15,240,99q�