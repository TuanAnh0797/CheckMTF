   M S B A M L   `   `   `  ����C  ?CheckMTF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:CheckMTF.MyUserControlCheckMTF.MyUserControl  5      -    'CheckMTF.MyUserControl.MyControlBarxaml   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:CheckMTF.MyUserControl     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	      -   5        $��MyControlBarTA��5         MaterialDesignBody#���   5
      $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ MouseLeftButtonDown��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *cmd_MoveWindow+   CommandParameter 6G   ��  �� ElementName$ MyControlBarTA��5      .��5      � .+�5   
   (  "MaterialDesignThemes.Wpf.ColorZone  $��5,5,5,5q�6$   
�����_�.��5      �  � ColumnDefinitions 5      �� 5      6)   �� 5       6)   .+�5"      �� $��0��6   	 �� Orientation$	 
Horizontal=�6.   .+�5#      �� $��20��6;   $��20��6G   ��6   �� *imagesource+6U   �� 5$      
�������$��5,0,0,0q�64   $��Center=�6E   $��Cambria Math)�6`   $/��)Phần mềm kiểm tra công đoạn MTF��6z   $��22(�6�   $
��Bold"�6�   5&      �� 5'      $��1��6   $	 
Horizontal=�6-   $��Right=�6F   
������ .+�5(      �� 
�������$��5q�6�   
��� � ���6   �� *cmd_MinimizeWindow+��6D   �� $ MyControlBarTA����6�   0  *MaterialDesignThemes.Wpf.PackIconExtension  
   Kind$
 WindowMinimize=��� 5)      
�������$��5q�6�   
��� � ���6   �� *cmd_NormalWindow+��6B   �� $ MyControlBarTA����6�     $
 WindowRestore=��� 5*      
�������$��5q�6�   
���  ����6   �� *cmd_CloseWindow+��6A   �� $ MyControlBarTA����6�     $
 
CloseThick=�5+      5,      5/      50      51      