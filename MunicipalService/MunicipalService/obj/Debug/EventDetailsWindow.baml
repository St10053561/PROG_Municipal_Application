   M S B A M L   `   `   `  ����  MunicipalService)    #MunicipalService.EventDetailsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Event Details��5   	   $	��400��6   $	��600��6,    � WindowStartupLocation$ CenterScreen=�5   	   ���#F2F2F2 � 
ResizeMode$ NoResize=�5   	   .��5      � .+�5	   
   ��  �� CornerRadius$ 10��6   
����������#DDDDDD$��1q�6L    �� Padding$ 15q�6`      DropShadowEffect J� Effect# �   6m   ._�5
      �� $
��Auto=�6   .��5      �� .+�5      �� $��Event Details��6    $��22(�65   $
��Bold"�6C   
�������$��0,0,0,10q�6h   $��Center=�6z   ��5      �� 5       �� 
StartPoint$	 0,0&�62    �� EndPoint$	 1,0&�6C   .��5   "   �  � Color$ #3498DB��6/   	 � Offset$		 0.0Q�6?   � 5   "   $ #2980B9��6/   $		 1.0Q�6?   5      5      �� 5      $��20(�6;   $
��Bold"�6I   ���#2C3E50$��0,5,0,5q�6p   $��Center=�6�   ��6    �� *	EventName+6�   �� 5      $��14(�6?   $��Italic$�6M   ���#7F8C8D$��0,0,0,10q�6u   $��Center=�6�   ��6    �� *EventCategory+6�   �� 5      $��14(�6f   ���#7F8C8D$��0,0,0,5q�6�   $��Center=�6�   ��6    �� *	EventDate+
 �� StringFormat$ 
 Event Date: {0:dd/MM/yyyy}��6�   �� 5!      $��14(�6]   ���#7F8C8D$��0,0,0,10q�6�   $��Center=�6�   ��6    �� *EventLocation+$
 Location: {0}��6�   �� 5$      $
��Wrap=�6B   $��14(�6V   ���#34495E$��	0,10,0,10q�6y    �� Padding$ 5q�6�   ��6    �� *EventDescription+6�   �� 5'      -   5         ��Name$ CountdownTextBlock��5'       $��16(�6<   $
��Bold"�6J   ���#E74C3C$��0,0,0,10q�6q   $��Center=�6�   �� 5*      
�������$ 5q�60   $ 10��6<   $��0,0,0,10q�6N   ._�5+      �� $��Uniform=�6=   $��0,0,0,10q�6O   $	��400��6a   $	��300��6p   ��6    �� *	ImagePath+ 5,      3  -System.Windows.Media.Effects.DropShadowEffect  5-   "      Color$
 Gray��63      
BlurRadius$ 15Q�6@      ShadowDepth$ 5Q�6P   5/      50      �� 53      -   5        $��Close��53      $	��120��6-   $��40��69   $��Right=�6E   $��0,10,0,0q�6a   
�������$
��Bold"�6�   $��0q�6�   ��54      �� 55      $	 0,0&�62   $	 1,0&�6C   .��56   "   � $ #3498DB��6/   $		 0.0Q�6?   � 57   "   $ #2980B9��6/   $		 1.0Q�6?   58       5:        5;      $
 Gray��6/   $ 10Q�6<   $ 5Q�6L   ��5=      �� 5>       �� 
TargetType" ��6$   .�5?   "   ��  �� Property � �� �� Value �#3498DB6?    �� Triggers 5@   "   P� 5A   &    P� Property �@J�IsMouseOver P� Value . . �5B   *   ��  � �� �#2980B96G   5C   '   5E      5G      5H      5I      5J      5K      5L      