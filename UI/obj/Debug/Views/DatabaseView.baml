   M S B A M L   `   `   `  ����=  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:UI.ViewsUI.Views  5          UI.Views.DatabaseView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:UI.Views     5      
��� � �.��5
      �   � ColumnDefinitions  5   
   �� 5      $��20 �6   6,   �� 5      $��* �6   6+   �� 5      $��* �6   6+   �� 5      $��* �6   6+   �� 5      $��* �6   6+   �� 5      $��* �6   6+   �� 5      $��20 �6   6,    � RowDefinitions 5   
   �� 5      $��20 �6   6*   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   �� 5      $
��Auto �6   6,   �� 5      $��20 �6   6*   .+�5   
   �� -   5         ��Name$ Pattern��5      $��1��6#   $��1��60   $��5��6@   $	��200��6T   $��7q�6c   $��15(�6o   6   &   System.Windows.Controls.DataGrid  5    
   -   5          Name$ RepositoryView��5          DataGridDefaultStyle#��[  6+   $��2��6Y   $��1��6f   $��5��6v   $��Center=�6�    �� SelectedItem 6�   �� *SelectedEntity+   Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn   Header$ Polish��6%      MaxWidth$	 350Q�6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� 	 �� Path$	 Polish�
   ElementStyle
 5#      �� 5$      .�5%      ��  �� Property � �� �� Value$
 Wrap=�6G   �� 5&       �@��TextAlignment$ Center=�6H   �� 5'       � ��$ Center=�6L   �� 5(       �@��Padding$ 7q�6B   6N   5)      5+        5,      $ English��6%   $	 350Q�6W    66   �� $	 English�
 5-      �� 5.      .�5/      ��  � ��$
 Wrap=�6G   �� 50       �@��TextAlignment$ Center=�6H   �� 51       � ��$ Center=�6L   �� 52       �@��Padding$ 7q�6B   6N   53      55        56      $
 Book��6%   $	 200Q�6Q    63   �� $
	 Book�
 57      �� 58      .�59      ��  � ��$
 Wrap=�6G   �� 5:       �@��TextAlignment$ Center=�6H   �� 5;       � ��$ Center=�6L   �� 5<       �@��Padding$ 7q�6B   6N   5=      5?        5@      $ 
Form class��6%   $	 200Q�6\    69   �� $	 	FormClass�
 5A      �� 5B      .�5C      ��  � ��$
 Wrap=�6G   �� 5D       �@��TextAlignment$ Center=�6H   �� 5E       � ��$ Center=�6L   �� 5F       �@��Padding$ 7q�6B   6N   5G      5I        5J      $ Chapter��6%   $	 200Q�6W    66   �� $	 Chapter�
 5K      �� 5L      .�5M      ��  � ��$
 Wrap=�6G   �� 5N       �@��TextAlignment$ Center=�6H   �� 5O       � ��$ Center=�6L   �� 5P       �@��Padding$ 7q�6B   6N   5Q      5S      5U      5V      5W      