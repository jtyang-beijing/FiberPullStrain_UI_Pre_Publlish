   M S B A M L   `   `   `  ����J  FFiberPullStrain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:FiberPull	FiberPull  5   	   G CGLWpfControl, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null=.clr-namespace:OpenTK.Wpf;assembly=GLWpfControl
OpenTK.Wpf 5   	   W0clr-namespace:FiberPullStrain.CustomControl.view"FiberPullStrain.CustomControl.view  5   	       FiberPull.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:FiberPull     5   	   >wpf.clr-namespace:OpenTK.Wpf;assembly=GLWpfControl     5   	   JCustomControl0clr-namespace:FiberPullStrain.CustomControl.view     5   	   -   5        
������   � Title$*  $Fiber Pull Strain in-suit Test Stage��5
   	   $	��900��66   $
��1500��6C    � WindowStyle$ ThreeDBorderWindow=�5   	    � WindowStartupLocation$ CenterScreen=�6*    � Icon$ /blizzard_icon_249823.ico��6O    � 	Resources 5       �� 
TargetType �� Property �� Value��%�   '�       �� 5   
   " �6   .�5      ��  �@�Icon$U Opack://application:,,,/FiberPullStrain;component/image/blizzard_icon_249823.ico��6%   5      5      .��5      � .+�5   
   �� 
�������$��2q�6$   ._�5      � 
������  � RowDefinitions 5      �� 5      $��35 �6$   �� 5      �� 5      $��20 �6$   .+�5      2   ,FiberPullStrain.CustomControl.view.menuItems  -   5        	  Name$	 myMenuItmes��5   *   
������ $	��Top=�5   *   � 5      $��1��6   
 � ColumnDefinitions
 5      �� 5      $	��15* �6+   �� 5       $	��85* �6+   .+�5"      7   1FiberPullStrain.CustomControl.view.ButtonControls  -   5          Name$ myButtonControls��5"   3   $��5q�6M   �� 5#      $��1��6   
��� ���$��2q�6C   $��5,8,5,5q�5$       �� CornerRadius$ 5��6.   ._�5%      � $��1��6   $��1��6,   
 5&      �� 5'   "   $��50 �63   �� 5(   "   .+�5+      �� $��
Pull Force��6(   
�������$��20(�5,   (   $��LeftToRight=�66   $��Center=�5-   (   $��Center=�5.   (    � LayoutTransform 5/   "   �� 50   &    �� Angle$ 90Q�66   52      � 54      $��1��6#    55   "   �� 56   &   �� 57   &   $��50 �64   .+�5;   "   -   'FiberPull.GLCartesianGraphControlString  -   5          Name$ 	CartGraph��5<   %   $��0,5,5,5q�5=   %   $��1��5>   %   
�������$��1q�5@   %    � ContextMenu 5A   &   �� 5B   *   
�����w�$��20(�6S   .@�5C   .   m� -   5        $
��Data��5C   E   $
��Data��67   $��5q�6Q   .@�5D   2   m� -   5        $��NewJog��5D   R   $��New Jog Curve��6;   $��5q�5E   ;   6F   m� 5F   2   -   5        $��
SaveToFile��5F   _   $ ��Save Current Curve To File��6;   $��5q�5G   ;   5H   ;   m� 5I   2   -   5        $
��Open��5I   O   $��
Open Curve��6;   $��5q�6[   5J   ;   m� 5K   2   -	   5        $��Delete_Selected_Curve��5K   Z   $��Delete Selected Curve��6;   $��5q�5L   ;   5M   ;   5N   /   m� 5O   .   -
   5        $
��View��5O   E   $
��View��67   $��5q�6Q   .@�5P   2   m� -   5        $��ClearAll��5P   U   $��Clear all Curves��6;   $��5q�5Q   ;   5R   ;   m� 5S   2   -   5        $��AutoZoom��5S   N   $��	Auto Zoom��6;   $��5q�5T   ;   5U   ;   m� 5V   2   -   5        $��SetCurveStyle��5V   _   $��Set Curve Style��6;   $��5q�6T   5W   ;   5X   /   m� 5Y   .   -   5        $��systemSetup��5Y   M   $��System Setup��67   $��5q�6`   5Z   7   5[   +   5]   #   �� 5_   "   $��1��6)   ._�5`   &   � 
 5a   *   �� 5b   .   $	��50* �6?   �� 5c   .   $	��130 �6?   �� 5d   .   $	��50* �6?   .+�5f   *   �� -   5         ��Name$ jogleft��5f   1   $��0��6B   $	��130��6R   $��Right=�6^   
������ $	��<<<��5g   K   
��� E��$��20(�5h   1   $��0q�6?    �� 	ClickMode$ Press=�6S   5k   1   �� 5l   *   $��1��64   $��1��6A   $��Pull Distance��6Q   
�������$��20(�5m   5   $��LeftToRight=�6C   $	��155��6_   $��Center=�5n   5   $��Center=�6P    �� TextAlignment$ Center=�5o   4   5p   +   �� 5q   *   -   5        $ jogright��5q   1   $��2��6C   $	��130��6S   $
��Left=�6_   
������ $	��>>>��5r   J   
��� E��$��20(�5s   1   $��0q�6?   $ Press=�6S   5v   1   5w   '   5x   #   5y      5z      5{      5      �� 5�      $��2��6   
�������.+�5�      �� -   5         ��Name$ inforBar��5�      
�������$
��Bold"�5�   /   ��5�   .   �� *	Bar_Infor+ �� UpdateSourceTrigger$ PropertyChanged=�5�   /   5�      5�      5�      5�      5�      