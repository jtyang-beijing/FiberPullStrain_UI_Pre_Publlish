   M S B A M L   `   `   `  ����J  FFiberPullStrain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:FiberPullStrain.CustomControl.view"FiberPullStrain.CustomControl.view  5      W0clr-namespace:FiberPullStrain.CustomControl.view"FiberPullStrain.CustomControl.view  5      7    1FiberPullStrain.CustomControl.view.ButtonControls   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Blocal0clr-namespace:FiberPullStrain.CustomControl.view     5      MClearableTextBox0clr-namespace:FiberPullStrain.CustomControl.view     5      .��5
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
��� ���$��2q�63   $��3q�6H   ._�5&      �   5'      �� 5(      $��40 �6$   �� 5)      $	��50* �6$   �� 5*      $��30 �6$   �� 5+      $��30 �6$   �� 5,      $��30 �6$   �� 5-      $	��50* �6$   �� 5.      $��30 �6$   �� 5/      $��30 �6$   �� 50      $��30 �6$   .+�52      �� $��	Set Items��6   
�������$��25(�6>   $��Center=�6L   $��40��53      $	��Top=�6    9   3FiberPullStrain.CustomControl.view.ClearableTextBox  55      -   5          Name$ inBoxDistance��55   4   $��1��6K   	   tblBoundText$	 Input Distance��56   4   �� 57      -   5        $ btnDistanceSetOrigin��57      $��2��67   $��
Set Origin��6D   
������ 
�������$��20(�58   +   69   �� 59      -   5        
 ��Name$

 cbmm��59      $��3��6)   $��mm��66   
�������$��20(�6U   $��Center=�5:      $��Center=�68    d� 	IsChecked$
 TrueY�5;      6,   �� 5<      -   5        $
 cbinch��5<      $��4��6+   $
��inch��68   
�������$��20(�6Y   $��Center=�5=      $��Center=�68   5>        5?      -   5        $ 
inBoxForce��5?   4   $��5��6H   $	 Input Force��5@   4   �� 5A      -   5        $ btnForceSetOrigin��5A      $��6��64   $��
Set Origin��6A   
������ 
�������$��20(�5B   +   69   �� 5C      -	   5        $
 cbgrams��5C      $��7��6,   $��grams��69   
�������$��20(�6[   $��Center=�5D      $��Center=�68   $
 trueY�5E      6,   �� 5F      -
   5        $
 cbnewton��5F      $��8��6-   $��Newton��6:   
�������$��20(�6]   $��Center=�5G      $��Center=�68   6Z   5I      5J      5K      5L      