   M S B A M L   `   `   `  ����K  GMunicipalService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MunicipalService.ClassesMunicipalService.Classes  5   	   (    "MunicipalService.ViewReportsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:MunicipalService.Classes     5   	     � Title$  View Reports��5   	   $	��600��6   $	��800��6+    � WindowStartupLocation$ CenterScreen=�67    � WindowStyle$
 None=�5	   	    � AllowsTransparency . 
������  � 	Resources 5
      5   /MunicipalService.Classes.StatusToColorConverter   StatusToColorConverter��%   &	          5   
   6'   5      .��5      � ���#FAFAFA � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� 
�����F�$��40��6(   $��Center=�64    �� CornerRadius$ 0,0,5,5��6O   ._�5      � .+�5      �� $(��"Municipal Service View Reports Hub��6   
�������$��16(�6Y   $
��Bold"�5      $��Center=�6.   $��10,0,0,0q�6I   �� 5       �� Orientation$ 
Horizontal=�6   $��Right=�66   $��Center=�6R   $��0,0,10,0q�6m   .+�5      �� -   5        $��30��5      $��30��6(   $��0q�64   $��0q�6@   
������ .��5       �� $��_��6$   
�������$��16(�6@   $��Center=�6N   $��Center=�6k   ��5!      �� 5"       �� 
TargetType" ��6$   .�5#   "   �� 	 �� Property	 � ��
 �� Value

 ���� 6?    �� Triggers 5$   "   P� 5%   &    P� Property �@J�IsMouseOver P� Value . . �5&   *   �� 	 � ��
 �#4682B46G   5'   '   5)      5+      �� 5.      -   5        $��30��5.      $��30��6(   $��0q�64   $��0q�6@   
������ .��50      �� $��X��6$   
�������$��16(�6@   $��Center=�6N   $��Center=�6k   ��51      �� 52      " ��6$   .�53   "   �� 	 � ��

 ���� 6?    54   "   P� 55   &    �@J�IsMouseOver . . �56   *   �� 	 � ��

 �<��6G   57   '   59      5;      5<      5=      5>      �� 5A   
   $ 5��6   $��15q�6"    �� Padding$ 20q�6.   ���#3E4A59$��1��6P   ._�5B      �� $��View Reports��6   $��30(�6,   $��SemiBold"�6:   
�������$��Center=�6c   5C      �� 5F   
   -   5         ��Name$ ReportsListBox��5F      $��2��6*   $��15q�67   
������ $��0q�6\   ��5G      �� 5H      .R�5I      �� 
�������$ 5��60   $ 15q�6A   $��10q�6N   ���#D0D0D0$��1q�6p   ._�5J      �  � ColumnDefinitions 5K      �� 5L   "   $	��200 �63   �� 5N   "   $
��Auto �63   .+�5P      �� $��SemiBold"�6b   $��16(�6x   $��0��6�   $
��Left=�6�   ��6(   �� *ReportNumber+ �� StringFormat$ Report #{0}��6�   �� 5Q      $��14(�6@   $��1��6�   $��Right=�6�   ��6(   �� *Status+��6N   �� *Status+ �� 	Converter# [  6�   5R      5S      5T      5V      �� 5Y   
   -   5         ��Name$ ReportDetailsBorder��5Y      $��2��6.   $��15q�6;   $ 20q�6G   
�������$ 5��6g   ���#D0D0D0$��1q�6�   $y�	Collapsed=�6�   ._�5Z      �  5[      �� 5\      $
��Auto �6$   �� 5]      $
��Auto �6$   �� 5^      $
��Auto �6$   �� 5_      $��* �6$    5a      �� 5b      $
��Auto �6'   �� 5c      $��* �6'   .+�5f      �� $��Name:��6   $��SemiBold"�6)   $��14(�6?   $��0��6M   $��0��6Z   $��0,0,5,0q�6j   �� 5g      -   5         ��Name$ ReportTitleTextBlock��5g      $��14(�6z   $��0��6�   $��1��6�   ��6:   �� *SelectedItem.Title+ �� ElementName$ ReportsListBox��6�   �� 5i      $��Report Date:��6   $��SemiBold"�60   $��14(�6F   $��1��6T   $��0��6a   $��0,10,5,0q�6q   �� 5j      -   5        $ ReportDateTextBlock��5j      $��14(�6x   $��1��6�   $��1��6�   ��69   �� *SelectedItem.Date+$ ReportsListBox��6�   �� 5l      $��Report Status:��6   $��SemiBold"�62   $��14(�6H   $��2��6V   $��0��6c   $��0,10,5,0q�6s   �� 5m      -   5        $ ReportStatusTextBlock��5m      $��14(�6|   $��2��6�   $��1��6�   ��6;   �� *SelectedItem.Status+$ ReportsListBox����6�   �� *SelectedItem.Status+$ ReportsListBox��# [  �� 5p      -   5        $ FilesListBox��5p      $��3��60   $��2��6=   $��0,10,0,0q�6Q   ��5q      �� 5r      .R�5s      �� $ 
Horizontal=�6)   $��5q�6B   .+�5t   "   �� $��50��6(   $��50��63   $��5q�6_   ��6?   �� *FileIconPath+6_   �� 5u   "   $��Center=�6F   $��5q�6a   ��6,   �� *
FileName+6a   5v      5w      5y      5z      5{      5|      5}      