   M S B A M L   `   `   `  ����   androbot    androbot.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  
MainWindow��$	��742��$
��1071��.��� .+�� $	��665��$
��1063��$��
0,33,0,-33q� � ColumnDefinitions �� $
��531* ��� $
��532* �.+��� -    ��Name$ txtName��$��23�� �� TextWrapping$
 Wrap=�$��71,12,22,630q�$��2������ *
ObjectName+�� $��Name:��$
��Left=�$	��Top=�$��	10,10,0,0q��� $��10,51,10,82q����� *
Properties+���� .R���  �� Orientation$ Vertical=�.+��� ���� *Name+�� -   $ txtPropName��$��23��$
 Wrap=�$��106,55,22,587q�$��1������ *NewAndroProperty.Name+�� $��Property name:��$��1��$
��Left=�$��	10,51,0,0q�$	��Top=� J� RenderTransformOrigin$ -0.605,-1.115&��� -    ��Name$ btnAdd��$��Add property��$��1��$
��Left=�$��
10,113,0,0q�$	��Top=�$��75���� $��1��$(��"First property needs to be the id!��$��10,138,68,495q��� -   $ btnSaveObject��$��Save object to file��$��10,554,10,82q�$��1���� $��Property type:��$��1��$
��Left=�$��	10,82,0,0q�$	��Top=�$ -0.605,-1.115&��� -   $ TxtType��$��23��$
 Wrap=�$��106,85,22,557q�$��1������ *NewAndroProperty.Type+�� -   $7��1Create Database classes of all created objects...��$��
10,0,10,29q�$��2��$��35��$��Bottom=��� -   $ txtPath��$��23��$��10,10,190,0q�$
 Wrap=�$	��Top=�{�.  ���� *ProjectPath+�� -   $ 	btnBrowse��$��Browse project...��$	��Top=�$��	0,10,10,0q�$��Right=�$	��175��$��23��