   M S B A M L   `   `   `  ����=  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:UI.ViewsUI.Views  5          UI.Views.InputOutputView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:UI.Views     5      
�����p�.��5	      �   � RowDefinitions  5
   
   �� 5      $��20 �6   6*   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��20 �6   6*    � ColumnDefinitions 5   
   �� 5      $��20 �6   6,   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $��* �6   6+   �� 5       $��20 �6   6,   .+�5$   
   �� $��1��6   $��1��6!   $��2��61      HeaderTextBlockStyle#��[  6E   Save records in external file6�   �� 5'   
   -   5         ��Name$ SelectPathToSave��5'      $��2��6+   $��1��68     IOWindowButtonStyle#��[ 6H   .��6u   Select path6�   �� 5(   
   -   5         ��Name$ SelectedPathToSave��5(      $��2��60   $��2��6=   $��2��6M   $
��Wrap=�6^    �� Padding$ 10q�6r   $	��450��6   $
��Bold"�6�   
����  �6�   �� 5+   
   -   5        $ SaveRecords��5+      $��3��6&   $��1��63   #��[ 6C   .��6p   Save records6~   �� 5.   
   $��4��6   $��1��6!   $��2��61   #��[  6E   !Load records from external file6�   �� 51   
   -   5        $ SelectPathToLoad��51      $��5��6+   $��1��68   #��[ 6H   .��6u   Select path6�   �� 52   
   -   5        $ SelectedPathToLoad��52      $��5��60   $��2��6=   $��2��6M   $ 10q�6^   $
��Wrap=�6k   $	��450��6   $
��Bold"�6�   
����  �6�   �� 55   
   -   5        $ LoadRecords��55      $��6��6&   $��1��63   #��[ 6C   .��6p   Load records6~   &   System.Windows.Controls.DataGrid  58   
   -   5          Name$ RepositoryView��58      $��Stretch=�6+   $��7��6I   $��1��6V   $��3��6f   $��3��6z     DataGridDefaultStyle#��[ 6�      Columns 59      0  *System.Windows.Controls.DataGridTextColumn  5:      ,  &System.Windows.Controls.DataGridColumn   Header$ Polish��6%      MaxWidth$	 350Q�6U   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 65   �� 
 �� Path$
 Polish�   ElementStyle 5;      �� 5<      .�5=      ��  �� Property � �� �� Value$
 Wrap=�6G   �� 5>       �@��TextAlignment$ Center=�6H   �� 5?       � ��$ Center=�6L   �� 5@       �@��Padding$ 7q�6B   6N   5A      5C        5D      $ English��6%   $	 350Q�6W   	 66   �� $
 English� 5E      �� 5F      .�5G      ��  � ��$
 Wrap=�6G   �� 5H       �@��TextAlignment$ Center=�6H   �� 5I       � ��$ Center=�6L   �� 5J       �@��Padding$ 7q�6B   6N   5K      5M        5N      $
 Book��6%   $	 200Q�6Q   	 63   �� $

 Book� 5O      �� 5P      .�5Q      ��  � ��$
 Wrap=�6G   �� 5R       �@��TextAlignment$ Center=�6H   �� 5S       � ��$ Center=�6L   �� 5T       �@��Padding$ 7q�6B   6N   5U      5W        5X      $ 
Form class��6%   $	 200Q�6\   	 69   �� $
 	FormClass� 5Y      �� 5Z      .�5[      ��  � ��$
 Wrap=�6G   �� 5\       �@��TextAlignment$ Center=�6H   �� 5]       � ��$ Center=�6L   �� 5^       �@��Padding$ 7q�6B   6N   5_      5a        5b      $ Chapter��6%   $	 200Q�6W   	 66   �� $
 Chapter� 5c      �� 5d      .�5e      ��  � ��$
 Wrap=�6G   �� 5f       �@��TextAlignment$ Center=�6H   �� 5g       � ��$ Center=�6L   �� 5h       �@��Padding$ 7q�6B   6N   5i      5k      5m      �� 5n   
   -   5        $ LoadIntoDatabase��5n      $��10��6+   $��1��69   #��[ 6I   .��6v   Move all records6�   �� 5o   
   -	   5        $ 
ClearTable��5o      $��10��6%   $��2��63   #��[ 6C   .��6p   Clear table6}   5p      5q      