   M S B A M L   `   `   `  ����A  =suzhou, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:suzhou.pagessuzhou.pages  5          suzhou.pages.logistics   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:suzhou.pages     5      -   5          N� 
Foreground
  ����� N� Title$ order��5
      .��5      � .+�5   
   � $��	0,100,0,0q�6    � RowDefinitions 5      �� 5      $
��auto �6    60   �� 5      6"   �� 5      $
��auto �6    60    � ColumnDefinitions 5      �� 5      $
��auto �6#   62   �� 5      6%   �� 5      6%   �� 5      $
��auto �6#   62   .+�5      �� $��0��6   $��1��6&   $	��top=�67   $	��300��6O   ��5      �� 5       �� ImageSource$ /image/background/bc1.png��6!   5      .+�5       �� -   5         ��Name$ company_name��5       $��全平台信息汇总��62   $��24(�6A   $��20,10,10,10q�6O   � 5!       5"      �� 5#      $	��180 �6+   69   �� 5$      $��* �6+   67   .+�5&      �� $��0��6    $��总企业数��60   $��24(�6<   $��
30,20,0,10q�6J   �� 5'      -   5        $ CP_sum��5'       $��1��60   $�� ��6@   $��24(�6H    �� TextAlignment$ Center=�6V   $��Center=�6m   6�   5(      � 5)       5*      �� 5+      $	��180 �6+   69   �� 5,      $��* �6+   67   .+�5.      �� $��0��6    $��总设备数��60   $��24(�6<   $��
30,10,0,10q�6J   �� 5/      -   5        $ EQP_sum��5/       $��1��61   $�� ��6A   $��24(�6I   $ Center=�6W   $��Center=�6n   6�   50      � 51       52      �� 53      $	��180 �6+   69   �� 54      $��* �6+   67   .+�56      �� $��0��6    $��总订单数��60   $��24(�6<   $��
30,10,0,10q�6J   �� 57      -   5        $ 	Order_sum��57       $��1��63   $�� ��6C   $��24(�6K   $ Center=�6Y   $��Center=�6p   6�   58      � 59       5:      �� 5;      $	��180 �6+   69   �� 5<      $��* �6+   67   .+�5>      �� $��0��6    $��执行订单数��60   $��24(�6=   $��
30,10,0,10q�6K   �� 5?      -   5        $ 	EXE_Order��5?       $��1��63   $�� ��6C   $��24(�6K   $ Center=�6Y   $��Center=�6p   6�   5@      5A      �� 5C      $��1��6   $��2��6&   $��0,10,0,0q�67   $
��auto��6I   ��5E      �� 5F      $ /image/background/bc2.png��5G      5H      .+�5J      �� $��查询模块��6   $��24(�6(   $��20,15,10,10q�66   �� 5K      $��企业查询��6   $��24(�6(   $��30,20,10,10q�66   ]� 5L      $
��auto��6   .+�5M      �� -   5        $	��et1��5M   w   $
��Left=�6   $�� ��64   $��28(�6<   $��30,10,10,10q�6J   $	��240��6_   $��50��6k   6w   �� 5N      -   5        $��Right=�5N      $��搜索��64   $��24(�6A   $��0,5,10,5q�6O   $��70��6a   $��50��6l   6x   5O      �� 5P      $��订单查询��6   $��24(�6(   $��30,10,10,10q�66   ]� 5Q      $
��auto��6   .+�5R      �� -	   5        $	��et2��5R   w   $
��Left=�6   $�� ��64   $��28(�6<   $��30,10,10,10q�6J   $	��240��6_   $��50��6k   6w   �� 5S      -
   5        $��Right=�5S      $��搜索��64   $��24(�6A   $��0,5,10,5q�6O   $��70��6a   $��50��6l   6x   5T      �� 5U      $��模具查询��6   $��24(�6(   $��30,10,10,10q�66   ]� 5V      $
��auto��6   .+�5W      �� -   5        $	��et3��5W   w   $
��Left=�6   $�� ��64   $��28(�6<   $��30,10,10,10q�6J   $	��240��6_   $��50��6k   6w   �� 5X      -   5        $��Right=�5X      $��搜索��64   $��24(�6A   $��0,5,10,5q�6O   $��70��6a   $��50��6l   6x   5Y      �� 5Z      $��物流查询��6   $��24(�6(   $��30,10,10,10q�66   ]� 5[      $
��auto��6   .+�5\      �� -   5        $	��et4��5\   w   $
��Left=�6   $�� ��64   $��28(�6<   $��30,10,10,10q�6J   $	��240��6_   $��50��6k   6w   �� 5]      -   5        $��Right=�5]      $��搜索��64   $��24(�6A   $��0,5,10,5q�6O   $��70��6a   $��50��6l   6x   5^      5_      �� 5c      $��0��6   $��3��6&   $��3��68   $��Center=�6H   $��	0,0,0,380q�6e   $	��309��6x   ��5d      �� 5e      $ /image/background/bc2.png��5f      5g      .+�5i      �� $��物流情况��6   $��24(�6(   $
 Left=�66   $��
20,10,0,10q�6K   ]� 5j      $
��auto��6   .+�5k      �� $
��Left=�6    $��Center=�66   $��	订单号��6Q   $��24(�6\   $	��100��6j   $��20,0,0,0q�6v   6�   �� 5l      $��Right=�6    $�� ��67   $��24(�6?   $��
10,10,0,10q�6M   $
��auto��6a   5m      ]� 5n      $
��auto��6   .+�5o      �� $
��Left=�6    $��Center=�66   $��物流单号��6Q   $��24(�6]   $	��100��6k   $��20,0,0,0q�6w   6�   �� 5p      $��Right=�6    $�� ��67   $��24(�6?   $��
10,10,0,10q�6M   $
��auto��6a   5q      ]� 5r      $
��auto��6   .+�5s      �� $
��Left=�6    $��Center=�66   $��出库时间��6Q   $��24(�6]   $	��100��6k   $��20,0,0,0q�6w   6�   �� 5t      $��Right=�6    $�� ��67   $��24(�6?   $��
10,10,0,10q�6M   $
��auto��6a   5u      ]� 5v      $
��auto��6   .+�5w      �� $
��Left=�6    $��Center=�66   $��入库时间��6Q   $��24(�6]   $	��100��6k   $��20,0,0,0q�6w   6�   �� 5x      $��Right=�6    $�� ��67   $��28(�6?   $��
10,10,0,10q�6M   $
��auto��6a   5y      5z      �� 5}      $��0��6   $��1��6'   $��2��67   $��2��6H   $��	10,0,10,0q�6\   ��5~      �� 5      $$ /image/background/bc_frame.png��5�      5�      .+�5�      (  "System.Windows.Controls.WebBrowser  -   5          Name$ 
webBrowser��5�      $
��auto��6K   $	��526��6X   $��8,6,8,5q�6e   5�      �� 5�      $��2��6   $��1��6&   $��	10,10,0,0q�67   ��5�      �� 5�      $ /image/background/bc1.png��5�      5�      .+�5�      �� $#��/image/background/company.jpg��6   $��2��6?   $��1��6L   $��	10,7,10,7q�6\   $
��auto��6o   6   5�      �� 5�      $��2��6   $��2��6&   $��
10,10,10,0q�67   ��5�      �� 5�      $ /image/background/bc1.png��5�      5�      .+�5�      �� $!��/image/background/order.jpg��6   $��2��6=   $��2��6J   $��	10,7,10,7q�6Z   $
��auto��6m   6}   5�      �� 5�      $��2��6   $��3��6&   $��0,10,0,0q�66   ��5�      �� 5�      $ /image/background/bc1.png��5�      5�      .+�5�      �� $��设备运转比例��6   $��24(�6+   $��
10,15,10,0q�69   6P   �� 5�      $%��/image/background/run_ratio.jpg��6   $��0,40,0,0q�6A   $	��260��6S   5�      5�      5�      5�      