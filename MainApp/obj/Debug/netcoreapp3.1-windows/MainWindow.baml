   M S B A M L   `   `   `  ����J  FFiberPullStrain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:FiberPull	FiberPull  5   	   G CGLWpfControl, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null=.clr-namespace:OpenTK.Wpf;assembly=GLWpfControl
OpenTK.Wpf 5   	   W0clr-namespace:FiberPullStrain.CustomControl.view"FiberPullStrain.CustomControl.view  5   	       FiberPull.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:FiberPull     5   	   >wpf.clr-namespace:OpenTK.Wpf;assembly=GLWpfControl     5   	   JCustomControl0clr-namespace:FiberPullStrain.CustomControl.view     5   	   
������   � Title$*  $Fiber Pull Strain in-suit Test Stage��5
   	   $	��800��66   $
��1300��6C    � WindowStyle$ ThreeDBorderWindow=�5   	    � WindowStartupLocation$ CenterScreen=�6*   .��5      � .+�5   
   �� 
�������$��2q�6$   ._�5      � 
������  � RowDefinitions 5      �� 5      $��35 �6$   �� 5      �� 5      $��20 �6$   .+�5      2   ,FiberPullStrain.CustomControl.view.menuItems  
������ $	��Top=�5   *   � 5      $��1��6    � ColumnDefinitions 5      �� 5      $	��15* �6+   �� 5      $	��85* �6+   .+�5      7   1FiberPullStrain.CustomControl.view.ButtonControls  $��5q�63   �� 5      $��1��6   
��� ���$��2q�6C   $��5,8,5,5q�5       �� CornerRadius$ 5��6.   ._�5       � $��1��6   $��1��6,    5!      �� 5"   "   $��50 �63   �� 5#   "   .+�5&      �� $��
Pull Force��6(   
�������$��20(�5'   (   $��LeftToRight=�66   $��Center=�5(   (   $��Center=�5)   (    � LayoutTransform 5*   "   �� 5+   &    �� Angle$ 90Q�66   5-      � 5/      $��1��6#    50   "   �� 51   &   �� 52   &   $��50 �64   .+�56   "   -   'FiberPull.GLCartesianGraphControlString  -   5          Name$ 	CartGraph��57   %   $��0,5,5,5q�58   %   $��1��59   %   
�������$��1q�5;   %   �� 5>   "   $��1��6,   $��Pull Distance��69   
�������$��20(�5?      $��LeftToRight=�6&   $��Center=�5@      $��Center=�63   5A   #   5B      5C      5D      5H      �� 5J      $��2��6   
�������.+�5K      �  5L      �� 5M      �� 5N      .+�5P      �� -   5        	 ��Name$	 infobar��5P   $   
�������$��5,0,0,0q�6H   $��Information��6Y   $��Bottom=�5Q   $   
 �� TextAlignment$

 Left=�6?   5S      � 5T      -   5         �Name$ 
processbar��5T   &   $	��300��6:   $��Right=�6F   $��0,0,5,0q�5U   &   
�������$��0q�6I   5W      5X      5Y      5[      5\      5^      5`      