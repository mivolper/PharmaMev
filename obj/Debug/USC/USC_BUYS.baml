   M S B A M L   `   `   `  ����D  @PharmaMev, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:PharmaMev.USCPharmaMev.USC  5          PharmaMev.USC.USC_BUYS   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:PharmaMev.USC     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialdesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5        .��5
      � .+�5   
   � $	��920��6   $	��350��6   
�������$��Right=�6;   $	��Top=�6W     J� Effect  5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ 	LightGray��6#      ShadowDepth$ 3Q�65      RenderingBias$ Quality=�6E   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��Top=�62   $��0 20 0 0q�6J   $��50��6\   $��RightToLeft=�6h   .+�5      �� -   5         ��Name$ txtBuyID��5       %  !MaterialDesignFloatingHintTextBox#��[  6,   $	��200��6g   $��18(�6s   $��Hacen Saudi Arabia)�6�   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ رقم الفاتورة��6�   $��RightToLeft=�6�   {�.  $��20 0 0 0q�6�   6  (  "System.Windows.Controls.DatePicker  5      -   5          Name$ 
txtBuyDate��5       ( $MaterialDesignFloatingHintDatePicker#��[ 61   $	��200��6o   $��18(�6{   $��Hacen Saudi Arabia)�6�   $! تاريخ الفاتورة��6�   $��RightToLeft=�6�   $��50��6�   {�.  $��20 0 0 0q�6  �� 5      -   5        $ txtUser��5      #��[  6+   $	��200��6f   $��18(�6r   $��Hacen Saudi Arabia)�6�   $ المستخدم��6�   $��RightToLeft=�6�   {�.  $��20 0 0 0q�6�   �� 5      -   5         ��Name$ cmbCaule��5       & "MaterialDesignFloatingHintComboBox#��[ 6-   $ المورد��6i   $��Hacen Saudi Arabia)�6�   $	��200��6�   $��18(�6�   $��RightToLeft=�6�   	 �� 
IsReadOnly	 . $��20 0 0 0q�6�   5      �� 5      $ 
Horizontal=�6   $	��Top=�62   $��	0 100 0 0q�6J   $��50��6]   $��RightToLeft=�6i   .+�5      �� -   5        
 ��Name$
 	btnNewBuy��5      $��فاتورة جديدة��6,   $��Hacen Saudi Arabia)�6C   $	��250��6c   $��40��6o   $��Bottom=�6{   $��Right=�6�   $��65 0 0 0q�6�   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�6�   6�   �� 5      -   5        $ 
txtBarcode��5      #��[  6.   $	��200��6i   $��18(�6u   $��Hacen Saudi Arabia)�6�   $ باركود المنتج��6�   $��RightToLeft=�6�   {�.  $��40 0 0 0q�6  6  �� 5      -   5        $
 
btnSaveBuy��5      $��حفظ الفاتورة ��6-   $��Hacen Saudi Arabia)�6E   $	��250��6e   $��40��6q   $��Bottom=�6}   $��Right=�6�   $��40 0 0 0q�6�   $ Depth3=�6�   {�.  6  5      �� 5      $ 
Horizontal=�6   $	��Top=�62   $��	0 180 0 0q�6J   $��50��6]   $��RightToLeft=�6i   .+�5      �� -	   5        $ txtProductName��5      #��[  62   $	��200��6m   $��18(�6y   $��Hacen Saudi Arabia)�6�   $ اسم المنتج��6�   $��RightToLeft=�6�    �� 
IsReadOnly . $��20 0 0 0q�6  6  �� 5       -
   5        $ txtCost��5       #��[  6+   $	��200��6f   $��18(�6r   $��Hacen Saudi Arabia)�6�   $ التكلفة��6�   $��RightToLeft=�6�   {�.  $��20 0 0 0q�6�   �� 5!      -   5        $ txtQuantity��5!      #��[  6/   $	��200��6j   $��18(�6v   $��Hacen Saudi Arabia)�6�   $ الكمية��6�   $��RightToLeft=�6�   {�.  $��20 0 0 0q�6�     5"      -   5        $ 	txtExDate��5"      #��[ 60   $	��200��6n   $��18(�6z   $��Hacen Saudi Arabia)�6�   $. (تاريخ انتهاء الصلاحية��6�   $��RightToLeft=�6�   {�.  $��20 0 0 0q�6  5$      �� 5&      -   5        $
 btnNewProduct��5&      $��جديد��6,   $��Hacen Saudi Arabia)�6;   $	��200��6[   $��40��6g   $��Bottom=�6s   $��Right=�6�   $��	0 0 20 35q�6�   $ Depth3=�6�   6�   �� 5'      -   5        $
 btnAddProduct��5'      $��
اضافة��6,   $��Hacen Saudi Arabia)�6<   $	��200��6\   $��40��6h   $��Bottom=�6t   $��Right=�6�   $��
0 0 245 35q�6�   $ Depth3=�6�   {�.  6  �� 5(      -   5        $
 btnEditProduct��5(      $��
تعديل��6-   $��Hacen Saudi Arabia)�6=   $	��200��6]   $��40��6i   $��Bottom=�6u   $
��Left=�6�   $��
245 0 0 35q�6�   $ Depth3=�6�   {�.  6  �� 5)      -   5        $
 btnDeletProduct��5)      $��حذف��6.   $��Hacen Saudi Arabia)�6<   $	��200��6\   $��40��6h   $��Bottom=�6t   $
��Left=�6�   $��	20 0 0 35q�6�   $ Depth3=�6�   {�.  6  5+      � 5-   
   $	��355��6   $��Bottom=�6   
�������  5.        5/      $ 	LightGray��6#   $ 3Q�65   $ Quality=�6E   .+�52      &   System.Windows.Controls.DataGrid  -   5          Name$ dgBill��52         
IsReadOnly . $
��1000��69   $��Right=�6F      AutoGenerateColumns .  
�������$��RightToLeft=�6�      SelectionMode$ Extended=�6�   $��Hacen Saudi Arabia)�6�   $��14(�6     SelectionUnit$ FullRow=�6  $��Visible=�6(  $��Disabled=�6]  ��6~   ��    Columns 53      0  *System.Windows.Controls.DataGridTextColumn  54      -   5        IDProductsColumn��,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 110 54   D      Header$ ر. المنتج��6P   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6c   �� *
ID_Product+  55      -   5        NameProductsColumn��$	 137 55   F   $ اسم المنتج��6R    6f   �� *Name+  56      -   5        
CostColumn��$	 110 56   >   $ التكلفة��6J      
Foreground
 �  �� 6[   �� *Cost+6t     57      -   5        QuantityColumn��$	 110 57   B   $ الكمية��6N    �	#FF2196F3 6^   �� *Quantit_BuysAction+6�     58      -   5        EX_DateColumn��$ SizeToHeader 58   A   $/ )تاريخ انتهاء الصلاحية ��6V    6v   �� *	EX_Date+  59      -   5        PrieceColumn��$	 110 59   @   $! التكلفة الكلية��6L   
 �  �� 6d   �� *	totatCost+6�   5;      �� 5=      $	��Top=�6   $
��Left=�60   $	��180��6K   $��50��6W   ���	#FF6E6E6E$��80 0 0 0q�6z   $$��اجمالي الفاتورة ��6�    �� Padding$ 	25 12 0 0q�6�   $��18(�6�   $��Hacen Saudi Arabia)�6�   �� 5>      -   5         ��Name$ txtbBuyCost��5>      $	��Top=�6-   $
��Left=�6E   $	��240��6`   $��50��6l   
���   �$��	50 50 0 0q�6�   $��0��6�   $ 0 5 0 0q�6�   $��30(�6�   $��Hacen Saudi Arabia)�6�    �� TextAlignment$ Center=�6�   5@      � 5B   
   $	��350��6   $	��370��6   $	��Top=�6(   $
��Left=�6@   
�������  5C        5D      $ 	LightGray��6#   $ 3Q�65   $ Quality=�6E   .+�5G      �� $��شاشة البحث ��6   $��RightToLeft=�6+   $��Hacen Saudi Arabia)�6G   $��22(�6g   ���	#FF555454$��	0 20 20 0q�6�   $	��300��6�   $��40��6�   $��Right=�6�   $	��Top=�6�   �� 5I      -   5        $ txtSearchBill��5I      #��[  6-   $&  بحث برقم الفاتورة��6h   $��Hacen Saudi Arabia)�6�   $	��140��6�   $��18(�6�   $��RightToLeft=�6�   $	��Top=�6�   $��Right=�6	  $��0 100 115 50q�6%  6;  �� 5J      -   5        $
 btnNext��5J      $��70��6&   $��50��61   $
��Left=�6=   $��20 100 0 50q�6X   $	��Top=�6m   .��5K      '
  !MaterialDesignThemes.Wpf.PackIcon
  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ 	ArrowLeft=�6*   $��30��6;   $��30��6F   5L      �� 5M      -   5        $
 btnBack��5M      $��70��6&   $��50��61   $��Right=�6=   $��0 100 20 50q�6Y   $	��Top=�6n   .��5N      
  $ 
ArrowRight=�6*   $��30��6<   $��30��6G   5O      5Q      5S      5T      