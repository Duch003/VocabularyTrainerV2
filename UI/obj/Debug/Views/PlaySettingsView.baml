   M S B A M L   `   `   `  ����=  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:UI.ViewsUI.Views  5      [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5          UI.Views.PlaySettingsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:UI.Views     5      Rforms@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5      
������.��5
      � $��Stretch=�6   $��Stretch=�6)     � RowDefinitions  5   
   �� 5      $��20 �6   6*   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   �� 5      $��20 �6   6*    � ColumnDefinitions 5   
   �� 5      $��20 �6   6,   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $��* �6   6+   �� 5       $��20 �6   6,   .+�5$   
   �� $��1��6   $��1��6!   $��5��61   $��30(�6E      HeaderTextBlockStyle#��[  6S   Game settings6�   �� 5'   
   $��3��6   $��1��6!   $��Center=�61   #��[  6L   $��21��6z   $��0,1,0,16q�6�   Subject area:6�   �� 5(   
   $��2��6   $��2��6!     DescriptionTextBlockStyle#��[ 61   Book6j   �� 5)   
   $��2��6   $��3��6!   #��[ 61   	Chapter6m   �� 5*   
   $��2��6   $��4��6!   #��[ 61   
Form class6p   �� 5.   
   -   5         ��Name$ BookComboBox��5.      $��3��6r   $��2��6     PlaySettingsComboBoxStyle#��[ 6�   ��6)   �� *
BookOption+ �� SelectedItem 6L   �� *SelectedBook+6�   �� 5/   
   $��3��6b   $��3��6o   #��[ 6   ��6   �� *ChapterOption+ 69   �� *SelectedChapter+6�   �� 50   
   $��3��6f   $��4��6s   #��[ 6�   ��6   �� *FormClassOption+ 6;   �� *SelectedFormClass+6�   �� 53   
   $��5��6   $��1��6!   #��[  61   $��Center=�6_   $��21��6z   $��0,1,0,16q�6�   Scoring system:6�   �� 54   
   $��4��6   $��2��6!   #��[ 61   Points to pass6t   �� 55   
   $��4��6   $��3��6!   #��[ 61   Points for good anwser6|   �� 56   
   $��4��6   $��4��6!   #��[ 61   Points for bad anwser6{   �� 5:   
   -   5         ��Name$ PointsToPass��5:   /   $��5��6   $��2��6     NumericInputTextBoxStyle#��[ 6E   6y   �� 5;   
   -   5        $ PointsPerGoodAnwser��5;   /   $��5��6   $��3��6   #��[ 6L   6�   �� 5<   
   -   5        $ PointsPerBadAnwser��5<   /   $��5��6   $��4��6   #��[ 6K   6   �� 5?   
   $��7��6   $��1��6!   #��[  61   $��Center=�6_   $��21��6z   $��0,1,0,16q�6�   Time challange:6�   �� 5@   
   $��6��6   $��2��6!   #��[ 61   
Activate6n   �� 5A   
   $��6��6   $��3��6!   #��[ 61   Time per question6w   �� 5B   
   $��6��6   $��4��6!   #��[ 61   Duration of game6v   �� 5E   
   $��7��6   $��2��6    $��Time challange mode��6o     SimpleCheckBoxStyle#��[ 6�   $��14��6�   $��14,12,42,12q�6�   $	��134��6�    d� 	IsChecked 60   �� *EnableTimeChallange+ �� Mode$ OneWayToSource=�6�   �� 5F   
   -   5        $ TimePerQuestion��5F   f   $��7��6   $��3��6   #��[ 6   {�6/   �� *EnableTimeChallange+$ OneWay=�6�   �� 5G   
   -   5        $ TimePerGame��5G   f   $��7��6   $��4��6   #��[ 6{   {�6/   �� *EnableTimeChallange+$ OneWay=�6�   �� 5J   
   $��3��6   $��5��6    �� Padding$ 10q�6.   $��10q�6;   $��4q�6G   
���   �$��5��6o   ._�5K      �� -   5         ��Name$ StartButton��5K      $��10q�6,   
���"�"�$��Consolas)�6Q   $��30(�6g   $��5q�6v   .��6�   Play6�   5L      �� 5O   
   -   5        	 ��Name$	 	ErrorList��5O      $��9��6'   $��1��64   $��2��6D   
���  ��$
��Wrap=�6i   $	��340��6}   6�   �� 5P   
   
 �� Orientation$
 Vertical=�6   $��9��6,   $��3��69   $��3��6I   
����� �.+�5Q      ��  �� TextAlignment$ Center=�6    �� Padding$ 5q�6/   $��Center=�6;   $
��Bold"�6X   $��Consolas)�6j   $��30(�6�   ==========RULES==========6�   �� 5R      -	   5        $	 Rules��5R      $ 5q�6'   $ Justify=�63   $��Stretch=�6K   $��Consolas)�6i   $��18(�6   $��WrapWithOverflow=�6�   6�   5S      5T      5U      