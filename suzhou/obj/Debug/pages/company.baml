   M S B A M L   `   `   `  ����A  =suzhou, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:suzhou.pagessuzhou.pages  5          suzhou.pages.company   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:suzhou.pages     5      -   5          N� 
Foreground
  ����� N� Title$ platform��5
      .��5      � .+�5   
   � $��	0,100,0,0q�6    � RowDefinitions 5      �� 5      $
��auto �6    60   �� 5      6"   �� 5      $
��auto �6    60    � ColumnDefinitions 5      �� 5      $
��auto �6#   62   �� 5      6%   �� 5      6%   �� 5      $
��auto �6#   62   .+�5      �� $��0��6   $��1��6&   $	��top=�67   $	��300��6O   ��5      �� 5       �� ImageSource$ /image/background/bc1.png��6!   5      .+�5       �� -   5         ��Name$ company_name��5       $��企业统计信息汇总��62   $��24(�6B   $��20,10,10,10q�6P   � 5!       5"      �� 5#      $	��180 �6+   69   �� 5$      $��* �6+   67   .+�5&      �� $��0��6    $��	订单数��60   $��24(�6;   $��
30,20,0,10q�6I   �� 5'      -   5        $ ord_num��5'       $��1��61   $��0��6A   $��24(�6J    �� TextAlignment$ Center=�6X   $��Center=�6o   6�   5(      � 5)       5*      �� 5+      $	��180 �6+   69   �� 5,      $��* �6+   67   .+�5.      �� $��0��6    $��	设备数��60   $��24(�6;   $��
30,10,0,10q�6I   �� 5/      -   5        $ eqp_num��5/       $��1��61   $��0��6A   $��24(�6J   $ Center=�6X   $��Center=�6o   6�   50      � 51       52      �� 53      $	��180 �6+   69   �� 54      $��* �6+   67   .+�56      �� $��0��6    $��运行设备数��60   $��24(�6=   $��
30,10,0,10q�6K   �� 57      -   5        $ excute_ord_num��57       $��1��68   $��0��6H   $��24(�6Q   $ Center=�6_   $��Center=�6v   6�   58      59      �� 5;      $��1��6   $��2��6&   $��0,10,0,0q�67   $
��auto��6I   ��5=      �� 5>      $ /image/background/bc2.png��5?      5@      .+�5B      �� $��查询模块��6   $��24(�6(   $��20,15,10,10q�66   �� 5C      $��企业查询��6   $��24(�6(   $��30,20,10,10q�66   ]� 5D      $
��auto��6   .+�5E      �� -   5        $	��et1��5E   w   $
��Left=�6   $�� ��64   $��28(�6<   $��30,10,10,10q�6J   $	��240��6_   $��50��6k   6w   �� 5F      -   5        $��Right=�5F      $��搜索��64   $��24(�6A   $��0,5,10,5q�6O   $��70��6a   $��50��6l   6x   5G      �� 5H      $��订单查询��6   $��24(�6(   $��30,10,10,10q�66   ]� 5I      $
��auto��6   .+�5J      �� -   5        $	��et2��5J   w   $
��Left=�6   $�� ��64   $��28(�6<   $��30,10,10,10q�6J   $	��240��6_   $��50��6k   6w   �� 5K      -	   5        $��Right=�5K      $��搜索��64   $��24(�6A   $��0,5,10,5q�6O   $��70��6a   $��50��6l   6x   5L      �� 5M      $��模具查询��6   $��24(�6(   $��30,10,10,10q�66   ]� 5N      $
��auto��6   .+�5O      �� -
   5        $	��et3��5O   w   $
��Left=�6   $�� ��64   $��28(�6<   $��30,10,10,10q�6J   $	��240��6_   $��50��6k   6w   �� 5P      -   5        $��Right=�5P      $��搜索��64   $��24(�6A   $��0,5,10,5q�6O   $��70��6a   $��50��6l   6x   5Q      �� 5R      $��物流查询��6   $��24(�6(   $��30,10,10,10q�66   ]� 5S      $
��auto��6   .+�5T      �� -   5        $	��et4��5T   w   $
��Left=�6   $�� ��64   $��28(�6<   $��30,10,10,10q�6J   $	��240��6_   $��50��6k   6w   �� 5U      -   5        $��Right=�5U      $��搜索��64   $��24(�6A   $��0,5,10,5q�6O   $��70��6a   $��50��6l   6x   5V      5W      � 5Z      -   5         �Name$ pageGrid��5Z      $��0��6%   $��1��62   $��2��6B   $��2��6S   .+�5[      (  "System.Windows.Controls.WebBrowser  -   5          Name$ 
webBrowser��5[      $��
20,20,10,0q�61   5\      � 5_      -   5        $
 main��5_      $��0��6!   $��1��6.   $��2��6>   $��2��6O   $��10,0,0,0q�6c    5`      �� 5a      $
��auto �6$   64   �� 5b      $��* �6$   61    5d      �� 5e      6)   �� 5f      6)   .+�5i      � $��2��6   $��2��6(   $��0,0,0,0q�6<   ��5j      �� 5n      .��5o      � 	 � Color$	 #3399CC��6+   
 � Offset$	
 0.9Q�6;   6J   � 5p      $	 #167158��6+   $	
 0.1Q�6;   6J   5q      5s      �� 5u      -   5        $ company_Name��5u      $��2��62   $��48(�6F   
�������$ Center=�6g   $��0,30,0,0q�6~   6�   �� 5v      $��1��6   $��2��6,   $��	0,20,0,30q�6@   $��Hidden=�6S   .��5w      � -   5         �Name$ 
equipments��5w       66   5x      5y      � 5|      $��0��6   $��3��6!   $��2��61   $��1��6B   $��10,0,0,0q�6V   ��5}      �� 5~      $ /image/background/bc3.png��5      5�      .+�5�      �� $��企业列表��6   $��24(�6(   
�������$
 Left=�6I   $��20, 15, 10, 15q�6^   $	��340��6v   �� 5�      $	��370��6   $
��auto��6+   $��Hidden=�6:   $��Hidden=�6a   $��	0,60,0,20q�6�   .��5�      �� -   5         ��Name$ companyList��5�   !   5�      5�      5�      5�      5�      5�      