   M S B A M L   `   `   `  ����F  BCursova_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Cursova_WPFCursova_WPF  5   	       Cursova_WPF.PlayList   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   +localclr-namespace:Cursova_WPF     5   	   -   5          �Name$  Window1��5   (    � Title$
 List��5   	   $	��395��6   $��465.423��6#    � WindowStyle$
 None=�5   2   $y�Visible=�6E    � 
ResizeMode$ NoResize=�6Z    � WindowStartupLocation$ Manual=�6p    � 	Resources 5       �� Source�� 5   
   $ Default.xaml?�6   %    .��5	      � 
���   � � RowDefinitions 5
   
   �� 5      $
��355* �6   �� 5      $	��32* �6   .+�5      �� -   5        $��playList��5      $��Stretch=�6'   $��Stretch=�6C   
���   �
�������$��1��6�   $ �� IsSynchronizedWithCurrentItem$ FalseY�6�   $��Disabled=�6�   ��6b   �� 	 �� ElementName$	 Window1��
 �� Path$

 list�5      a� 5   
   -   5        $��toolBar��5      $��Stretch=�6&   $��Bottom=�6D   $��1��6a   
���   � �� Orientation$ 
Horizontal=�6�   .+�5      �� -   5           HoveredImage#��[  5      $��Icon/Add.png��6:   $��20��6Q   $��5,3,0,5q�6]   $
��Left=�6o   6�   �� 5      -   5        #��[  5      $��Icon/Dell.png��6:   $��20��6R   $��5,3,0,5q�6^   $
��Left=�6p   6�   �� 5      -   5        #��[  5      $��Icon/AddFolder.png��6:   $��20��6W   $��5,3,0,5q�6c   $
��Left=�6u   6�   �� 5      #��[  6   $��Icon/Information.png��6:   $��20��6Y   $��5,3,5,5q�6e   $
��Left=�6w   �� 5      
�������$��2q�6)   $��22��6>   $	��240��6J   ._�5       �� $	��200��6   $
��Left=�6&   
�������$��0q�6�   ��6A   Z� ��6W   Z�  �� SelectionBrush 6�   Z� 6�   5!      �� 5"      #��[  6   $��Icon/Search.png��6:   $��15��6T   $��	-20,0,0,0q�6`   $
��Left=�6t    J� RenderTransformOrigin$ -0.155,1.805&�6�   �� 5#      -   5        #��[  5#      $��Icon/Cancel.png��6:   $��20��6T   $��97,3,5,5q�6`   $
��Left=�6s   $ 	1.38,0.26&�6�   5$      5(      5)      