   M S B A M L   `   `   `  ����J  FFiberPullStrain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:FiberPullStrain.CustomControl.view"FiberPullStrain.CustomControl.view  5      2    ,FiberPullStrain.CustomControl.view.menuItems   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Blocal0clr-namespace:FiberPullStrain.CustomControl.view     5      $��30��5        � 	Resources  5	       �� 
TargetType   MenuItemStyle �� BasedOn��%0   'm�       7   �� 5
   
   " m�6   8   6&   5      .��5      � 
������  � RowDefinitions 5   
   �� 5      $��50 �6   .+�5   
   o� 
������ .@�5      m� $��_File��6   
�������.@�5      m� -   5         m�Name$ mnNewJog��5      $��New Jog��6-   6>   m� 5      -   5        $ mnOpen��5      $
��Open��6+   69   m� 5      -   5        $ mnClose��5      $��Close��6,   m� 5      -   5        $ mnSave��5      $
��Save��6+   69   m� 5      -   5        $ mnSaveAs��5      $��Save as��6-   6>   m� 5      -   5        $ mnExit��5      $
��Exit��6+   69   5      m� 5      -   5        $��mnView��5   9   $��_View��6   
�������.@�5      m� -   5        $ mnClearView��5      $��
Clear View��60   5      m� 5      -	   5        $ mnDeleteSelectedCurve��5      $��Delete Selected Curve��6:   5      m� 5      -
   5        $ mnSetCurveStyle��5      $��Set Curve Style��64   .@�5      �� -   5         ��Name$ checkboxlineCuvre��5      $��5q�6:   $��
Line Curve��6E   $��Center=�6Z    d� 	IsChecked$
 TrueY�5       60   �� 5!      -   5        $ checkboxdotsCuvre��5!      $��5q�6:   $��
Dots Curve��6E   $��Center=�6Z   $ FalseY�5"      61   5#      5$      m� 5%      $��_Setup��6   
�������.@�5&      m� -   5        $ mnSearch��5&      $��Search Stage��6-   5'      m� 5(      -   5        $ mnSystemSetup��5(      $��System Setup��62   5)      5*      m� 5+      $��_About��6   
�������.@�5,      m� -   5        $ mnAboutProduct��5,      $��Product��63   5-      m� 5.      -   5        $ mnAboutSupport��5.      $��Support��63   5/      50      51      53      54      