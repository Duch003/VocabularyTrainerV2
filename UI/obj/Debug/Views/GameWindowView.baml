   M S B A M L   `   `   `  ����=  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:UI.ViewsUI.Views  5   	       UI.Views.GameWindowView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:UI.Views     5   	   ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5   	   
����� �$��20(�5   5     � Title$  GameWindowView��5	   	   $
��Auto��6    $	��430��6.   $
��Auto��6>   .��5
      �  � RowDefinitions 5   
   �� 5      $��20 �6   6*   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   �� 5      $��20 �6   6*    � ColumnDefinitions 5   
   �� 5      $��20 �6   6,   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $��20 �6   6,   .+�5    
   �� $��3q�6   
���   �
���k���$��4��6Q   $��1��6b   $��1��6o   6�   �� 5"   
   $��1��6   $��2��6   $��4��6.   $��3��6?   
���   �
�������$��5q�6   6�   �� 5#   
   $��1��6   $��3��6!    !  GameWindowStatsTextBlockStyle#��[  61   Question time6w   �� 5$   
   $��1��6   $��4��6!   #��[  61   
Total time6t   �� 5%   
   $��1��6   $��2��6!   #��[  61   ID6l   �� 5&   
   $��1��6   $��1��6!   $��4��61   $	��300��6B   $��Center=�6Q   $
��Wrap=�6l   $��20(�6�    �� Padding$ 5q�6�    �� TextAlignment$ Center=�6�   $��0,10,0,1q�6�   hfFor given polish expression write the translation in the box on the right side and press Check button.6+  �� 5)   
   -   5         ��Name$ QuestionTimeLeft��5)      $��2��6.   $��3��6;   #��[  6K   6�   �� 5*   
   -   5        $ GameTimeLeft��5*      $��2��6*   $��4��67   #��[  6G   6�   �� 5+   
   -   5        $ ID��5+      $��2��6    $��2��6-   #��[  6=   6v   �� 5.   
   $��3��6   $��2��6"   #��[  62   
Form class6u   �� 5/   
   $��3��6   $��3��6"   #��[  62   	Chapter6r   �� 50   
   $��3��6   $��4��6"   #��[  62   Book6o   �� 53   
   -   5        $ 	FormClass��53      $��4��6'   $��2��64   #��[  6D   $��0 0 0 10q�6{   6�   �� 54   
   -   5        $ Chapter��54      $��4��6%   $��3��62   #��[  6B   $��0 0 0 10q�6y   6�   �� 55   
   -   5        $
 Book��55      $��4��6"   $��4��6/   #��[  6?   $��0 0 0 10q�6v   6�   �� 58   
   $��5��6Z   $��3��6g   $��2��6w   $��5q�6�   $��5q�6�   $��2��6�    �� TextWrapping$
 Wrap=�6�    �� TextAlignment$ Center=�6�   ��6   �� *
AnwserText+��6.   �� *AnwserIndicatorBrush+6�   �� 59   
   -   5        $ QuestionText��59      $��5��6*   $��2��67   $��1��6K   
�������$
��Wrap=�6n   $ Center=�6�   $	��100��6�   $	��600��6�   $��25(�6�   $ 5q�6�   $��5q�6�   6�   �� 5<   
   -   5        $ CorrectAnwser��5<      $��6��6+   $��2��68   $��3��6�   $
��Wrap=�6�   $ Center=�6�   $��15(�6�   $ 5q�6�   $��5 0 5 5q�6�   ��6L   �� *AnwserIndicatorBrush+��6x   �� *AnwserBelowForegroundBrush+6  �� 5=   
   $��6��6   $��1��6   $��5q�6.   
���   �$��2��6V   $��2��6j   
�������6�   �� 5>   
   $��6��6   $��1��6   $��5q�6.   
���   �$��2��6V   6m   �� 5?   
   $��6��6   $��1��6   $��3q�6.   
���   �$��2��6V   6j   �� 5@   
   $��6��6   $��1��6!   $ Center=�61   Points6P   �� 5A   
   $��6��6   $��2��6!   $ Center=�61   Requirement6U   �� 5D   
     Caliburn.Micro.Message   Attach$0 *[Event Click] = [Action CheckButton_Click]��68   $��7��6x   $��4��6�   $��5q�6�   $��5q�6�   ��6   �� *CheckButtonContent+6�   �� 5E   
   -	   5        $ PresentPoints��5E      $��7��6+   $��1��68   $ Center=�6H   6a   �� 5F   
   -
   5        $ RequiredPoints��5F      $��7��6,   $��2��69   $ Center=�6I   6b   5G      5H      