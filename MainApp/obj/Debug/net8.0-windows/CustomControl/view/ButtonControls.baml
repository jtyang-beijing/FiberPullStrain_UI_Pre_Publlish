   M S B A M L   `   `   `  ����J  FFiberPullStrain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:FiberPullStrain.CustomControl.view"FiberPullStrain.CustomControl.view  5      7    1FiberPullStrain.CustomControl.view.ButtonControls   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Blocal0clr-namespace:FiberPullStrain.CustomControl.view     5      .��5
      �   � RowDefinitions  5   
   �� 5      $	��100 �6   �� 5      �� 5      $��55 �6   .+�5   
   �� -   5         ��Name$ btStart��5      
������ $��3q�6;   
��� ���$��1q�6[   $��60(�5      ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value
 �����63    �� Triggers 5      �� 5       �� Binding 6&   �� *	IsRunning+ �� Value True6D   .a�5      ��  � ��$
 Stop��68   �� 5       � ��
 �  ��6;   5      �� 5       6&   �� *	IsRunning+	 False6D   .a�5      ��  � ��$ Start��68   �� 5       � ��
 ��� �6;   5      5      5!      �� 5"   
   -   5        $
 Exit��5"      
������ $��3��68   $��3q�6E   
��� ���$��1q�6e   
�������$
��Exit��5#   $   $��33(�63   $��45��5$      $��Bottom=�6   68   �� 5%   
   $��1��6   
��� ���$��2q�63   $��3q�6H   ._�5&      �   5'      �� 5(      $��40 �6$   �� 5)      $��50 �6$   �� 5*      $��30 �6$   �� 5+      $	��50* �6$   �� 5,      $��30 �6$   �� 5-      $��50 �6$   �� 5.      $��30 �6$   �� 5/      $	��50* �6$   �� 50      $��30 �6$   .+�52      �� $��	Set Items��6   
�������$��25(�6>   $��Center=�6L   $��40��53      $	��Top=�6    9   3FiberPullStrain.CustomControl.view.ClearableTextBox  55      -   5          Name$ inBoxDistance��55   )   $��1��6@   � 58      $��2��6   	 � ColumnDefinitions	 59      �� 5:      �� 5;      .+�5=      �� -   5        
 ��Name$

 cbmm��5=      $��0��6-   $��mm��6=   
�������$��20(�6\   $��Center=�5>      $��Center=�68    d� 	IsChecked$
 TrueY�5?      6,   �� 5@      -   5        $
 cbinch��5@      $��1��6/   $
��inch��6?   
�������$��20(�6`   $��Center=�5A      $��Center=�68   5B      5C      �� 5E      $��3��6   
�������$��1q�6>   ._�5F      (� .��5G      �� -   5         ��Name$ lbCurrentDistance��5G       
��� �����5H      �� *lb_Current_Distance+ �� UpdateSourceTrigger$ PropertyChanged=�5I      5J      �� 5L      -   5        $ btnDistanceSetOrigin��5L      $��4��67   $��
Set Origin��6D   
������ 
�������$��20(�5M   +   69     5O      -   5        $ 
inBoxForce��5O   )   $��5��6=   � 5R      $��6��6   	 5S      �� 5T      �� 5U      .+�5W      �� -	   5        $
 cbgrams��5W      $��0��60   $��grams��6@   
�������$��20(�6b   $��Center=�5X      $��Center=�68   $
 trueY�5Y      6,   �� 5Z      -
   5        $
 cbnewton��5Z      $��1��61   $��Newton��6A   
�������$��20(�6d   $��Center=�5[      $��Center=�66   6X   5\      �� 5^      $��7��6   
�������$��1q�6>   ._�5_      (� .��5`      �� -   5        $ lbCurrentForce��5`       
��� �����5a      �� *lb_Current_Force+$ PropertyChanged=�5b      5c      �� 5f      -   5        $ btnForceSetOrigin��5f      $��8��64   $��
Set Origin��6A   
������ 
�������$��20(�5g   +   69   5h      5i      5j      5k      