   M S B A M L   `   `   `  ����L  HTimeManagementApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:TimeManagementAppTimeManagementApp  5   	   %    TimeManagementApp.StartSemester   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:TimeManagementApp     5   	     � Title$  Start Semester��5   	   $	��450��6    $	��800��6-   .��5	      � ���	#FFE3D6B8.+�5   
   o� ���	#FFE3D6B8.@�5      m� $
��Menu��6   
������ .@�5      m� -   5        $
��Home��5      6)   m� 5      -   5        $
��Exit��5      6)   5      5      �� 5   
   $��Semester Information��6   $��Center=�60   $	��Top=�6M   $��Arial Black)�6e   $��24(�6~   $��0,20,0,0q�6�   �� 5   
   $��0,60,0,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��10,10q�62   .+�5      �� $��Number of weeks:��6   $	��120��63   $��Center=�6?   �� 5      -   5         ��Name$ weeksno��5      $	��200��6+   �� 5      -   5         ��Name$ 
errorLabel��5      $3��-Invalid input. Enter a valid number of weeks.��60   
���  ��$y�	Collapsed=�6v   5      �� 5      $ 
Horizontal=�6   $��10,10q�62   .+�5      �� $��Start Date:��6   $	��120��6.   $��Center=�6:   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ 
datePicker��5      $	��200��61   5       �� 5!      -   5        $ dateErrorLabel��5!      $��Invalid start date.��60   
���  ��$y�	Collapsed=�6\   5#      �� 5$   
   -   5        $
��Save��5$      $��Right=�6    $��Bottom=�6<   $	��100��6W   $��34��6c   $��	0,0,28,26q�6o   ���	#FF1986DA���	#FFDDB311$��Arial)�6�   $��16(�6�   5&      5'      