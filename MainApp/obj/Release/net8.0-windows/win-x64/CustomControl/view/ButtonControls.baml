   M S B A M L   `   `   `  ���� J  FFiberPullStrain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:FiberPullStrain.CustomControl.view"FiberPullStrain.CustomControl.view  7    1FiberPullStrain.CustomControl.view.ButtonControls   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Blocal0clr-namespace:FiberPullStrain.CustomControl.view     .���   � RowDefinitions  �� $	��100 ��� �� $��55 �.+��� -    ��Name$ btStart��
������ $��3q�
��� ���$��1q�$��60(�����  �� 
TargetType" ��.���  �� Property � �� �� Value
 ����� �� Triggers ��  �� Binding �� *	IsRunning+ �� Value True.a���  � ��$
 Stop����  � ��
 �  ����  �� *	IsRunning+	 False.a���  � ��$ Start����  � ��
 ��� ��� -   $
 Exit��
������ $��3��$��3q�
��� ���$��1q�
�������$
��Exit��$��33(�$��45��$��Bottom=��� $��1��
��� ���$��2q�$��3q�._��   �� $��40 ��� $��50 ��� $��30 ��� $	��50* ��� $��30 ��� $��50 ��� $��30 ��� $	��50* ��� $��30 �.+��� $��	Set Items��
�������$��25(�$��Center=�$��40��$	��Top=�9   3FiberPullStrain.CustomControl.view.ClearableTextBox  -     Name$ inBoxDistance��$��1��� $��2��	 � ColumnDefinitions	 �� �� .+��� -   
 ��Name$

 cbmm��$��0��$��mm��
�������$��20(�$��Center=�$��Center=� d� 	IsChecked$
 TrueY��� -   $
 cbinch��$��1��$
��inch��
�������$��20(�$��Center=�$��Center=��� $��3��
�������$��1q�._�(� .���� -    ��Name$ lbCurrentDistance��
��� ������� *lb_Current_Distance+ �� UpdateSourceTrigger$ PropertyChanged=��� -   $ btnDistanceSetOrigin��$��4��$��
Set Origin��
������ 
�������$��20(�  -   $ 
inBoxForce��$��5��� $��6��	 �� �� .+��� -	   $
 cbgrams��$��0��$��grams��
�������$��20(�$��Center=�$��Center=�$
 trueY��� -
   $
 cbnewton��$��1��$��Newton��
�������$��20(�$��Center=�$��Center=��� $��7��
�������$��1q�._�(� .���� -   $ lbCurrentForce��
��� ������� *lb_Current_Force+$ PropertyChanged=��� -   $ btnForceSetOrigin��$��8��$��
Set Origin��
������ 
�������$��20(�