   M S B A M L   `   `   `  ����R  NTimeManagementApp.WPFUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:TimeManagementApp.WPFUI.ViewsTimeManagementApp.WPFUI.Views  5      ,    &TimeManagementApp.WPFUI.Views.MainPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:TimeManagementApp.WPFUI.Views     5      .��5	      � $��
0,126,0,10q�6   .+�5
   
   �� $
��Left=�63   $��	10,10,0,0q�6N   $
��Bold"�6a   $��15(�6s   $	��Top=�6�   ��6   �� *LoggedInPerson+6�   �� 5   
   $��Week filter:��6   $
��Left=�6'   $��	10,40,0,0q�6B   $��13(�6V   $	��Top=�6d   (  "System.Windows.Controls.DatePicker  5   
   $
��Left=�6^   $��	12,72,0,0q�6y   $	��Top=�6�   $	��187��6�       SelectedDate  6   �� *
FilterDate+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��Showing result for: ��6   $
��Left=�6/   $��
10,101,0,0q�6J   $��13(�6_   $	��Top=�6m   �� 5   
   $
��Left=�6W   $��136,101,0,0q�6r   $
��Bold"�6�   $��13(�6�   $	��Top=�6�   ��6   �� *FilterDisplay+$ PropertyChanged=�6�   �� 5   
   $��
Add Module��6   $
��Left=�6&   $��
624,72,0,0q�6A   $	��Top=�6U   $	��150��6m   �� 5   
   $��Center=�67   $��	0,150,0,0q�6T   $	��Top=�6g   $	��900��6   ��6   �� *UserModules+ �� View 5      �� 5      .P�5      �� $��Module name��6%    �� Width$	 100��6:    �� DisplayMemberBinding 6F   �� *Name+�� 5      $��Semester name��6%   $	 100��6<    6H   �� *SemesterName+�� 5      $��Module code��6%   $	 100��6:    6F   �� *Code+�� 5      $��Credits��6%   $	 100��66    6B   �� *	Credits+�� 5      $��Hours per week��6%   $	 100��6=    6I   �� *HoursPerWeek+�� 5      $��
Hours used��6%   $	 100��69    6E   �� *TotalHoursSpent+�� 5      $��Hours remaining��6%   $	 100��6>    6J   �� *HoursRemaining+�� 5      $��Self Study Hours��6%   $	 100��6?    6K   �� *SelfStudyHoursPerWeek+5       5"      5$      5%      