   M S B A M L   `   `   `  ����  androbot    androbot.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  
MainWindow��5   	   $	��742��6   $
��1071��6)   .��5      � .+�5
   
   � $	��665��6   $
��1063��6   $��
0,33,0,-33q�6)    � ColumnDefinitions 5      �� 5      $
��531* �6#   �� 5      $
��532* �6#   .+�5      �� -   5         ��Name$ txtName��5      $��23��6C    �� TextWrapping$
 Wrap=�6O   $��71,12,22,630q�6c   $��2��6y   ��6'   �� *
ObjectName+6y   �� 5      $��Name:��6   $
��Left=�6$   $	��Top=�6?   $��	10,10,0,0q�6W   �� 5      $��10,51,10,82q�6   ��6+   �� *
Properties+��5      �� 5      .R�5      ��  �� Orientation$ Vertical=�6%   .+�5      �� ��6(   �� *Name+5      5      5      �� 5      -   5        $ txtPropName��5      $��23��6R   $
 Wrap=�6^   $��106,55,22,587q�6r   $��1��6�   ��6+   �� *NewAndroProperty.Name+6�   �� 5      $��Property name:��6   $��1��6-   $
��Left=�6=   $��	10,51,0,0q�6X   $	��Top=�6k    J� RenderTransformOrigin$ -0.605,-1.115&�6�   �� 5      -   5         ��Name$ btnAdd��5      $��Add property��6%   $��1��6<   $
��Left=�6L   $��
10,113,0,0q�6g   $	��Top=�6{   $��75��6�   6�   �� 5      $��1��6   $(��"First property needs to be the id!��6(   $��10,138,68,495q�6R   6k   �� 5      -   5        $ btnSaveObject��5      $��Save object to file��6,   $��10,554,10,82q�6J   $��1��6`   6p   �� 5      $��Property type:��6   $��1��6-   $
��Left=�6=   $��	10,82,0,0q�6X   $	��Top=�6k   $ -0.605,-1.115&�6�   �� 5       -   5        $ TxtType��5       $��23��6N   $
 Wrap=�6Z   $��106,85,22,557q�6n   $��1��6�   ��6'   �� *NewAndroProperty.Type+6�   �� 5!      -   5        $7��1Create Database classes of all created objects...��5!      $��
10,0,10,29q�6Q   $��2��6|   $��35��6�   $��Bottom=�6�   5"      �� 5=   
   -   5        $ txtPath��5=      $��23��6#   $��10,10,190,0q�6/   $
 Wrap=�6D   $	��Top=�6u   {�.  ��6X   �� *ProjectPath+6�   �� 5>   
   -   5        $ 	btnBrowse��5>      $��Browse project...��6$   $	��Top=�6@   $��	0,10,10,0q�6X   $��Right=�6k   $	��175��6�   $��23��6�   6�   5@      5A      