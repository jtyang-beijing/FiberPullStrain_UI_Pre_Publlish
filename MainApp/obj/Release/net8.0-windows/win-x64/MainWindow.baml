   M S B A M L   `   `   `  ���� J  FFiberPullStrain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:FiberPull	FiberPull  G CGLWpfControl, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null=.clr-namespace:OpenTK.Wpf;assembly=GLWpfControl
OpenTK.Wpf W0clr-namespace:FiberPullStrain.CustomControl.view"FiberPullStrain.CustomControl.view      FiberPull.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:FiberPull     >wpf.clr-namespace:OpenTK.Wpf;assembly=GLWpfControl     JCustomControl0clr-namespace:FiberPullStrain.CustomControl.view     -   
������   � Title$*  $Fiber Pull Strain in-suit Test Stage��$	��900��$
��1500�� � WindowStyle$ ThreeDBorderWindow=� � WindowStartupLocation$ CenterScreen=� � 	Resources  �� 
TargetType �� Property �� Value��%@   '�       �� " �.���  �@�Icon$ ./image/icon.ico��.��� .+��� 
�������$��2q�._�� 
������  � RowDefinitions �� $��35 ��� �� $��20 �.+�2   ,FiberPullStrain.CustomControl.view.menuItems  -     Name$ myMenuItmes��
������ $	��Top=�� $��1��	 � ColumnDefinitions	 �� $	��15* ��� $	��85* �.+�7   1FiberPullStrain.CustomControl.view.ButtonControls  -   
  Name$
 myButtonControls��$��5q��� $��1��
��� ���$��2q�$��5,8,5,5q� �� CornerRadius$ 5��._�� $��1��$��1��	 �� $��50 ��� .+��� $��
Pull Force��
�������$��20(�$��LeftToRight=�$��Center=�$��Center=� � LayoutTransform ��  �� Angle$ 90Q�� $��1�� �� �� $��50 �.+�-   'FiberPull.GLCartesianGraphControlString  -     Name$ 	CartGraph��$��0,5,5,5q�$��1��
�������$��1q� � ContextMenu �� 
�����w�$��20(�.@�m� -   $
��Data��$
��Data��$��5q�.@�m� -   $��
SaveToFile��$ ��Save Current Curve To File��m� -   $
��Open��$��
Open Curve��m� -   $��Delete��$��Delete Current Curve��m� -	   $
��View��$
��View��$��5q�.@�m� -
   $��ClearAll��$��Clear all Curves��m� -   $��AutoZoom��$��	Auto Zoom���� $��1��$��Pull Distance��
�������$��20(�$��LeftToRight=�$��Center=�$��Center=��� $��2��
�������.+��� -    ��Name$ inforBar��
�������$
��Bold"����� *	Bar_Infor+ �� UpdateSourceTrigger$ PropertyChanged=�