   M S B A M L   `   `   `  ����D  @ChessGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ChessGame	ChessGame  5           ChessGame.PromotionControl   5      W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:ChessGame     5      $��393.75��5      $
��87.5��5	        � Cursor$
  Hand��5
      $}�False��5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5   
      Color$ Black��6      
BlurRadius$ 10Q�6)      ShadowDepth$ 10Q�69      Opacity$	 0.5Q�6J   .��5      � -   5         �Name$ 
GridSelect��5      
������� J� Clip 5   
   �� 5       �� Rect$ 0, 0, 87.5, 393.75�6    	 �� RadiusX$	 5Q�6:   
 �� RadiusY$
 5Q�6F    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��43.75 �6   .+�5   
   �� -   5         ��Name$ Queen��5      $��0��6A   �� 5   
   -   5        $ Knight��5      $��1��6C   �� 5   
   -   5        $
 Rook��5      $��2��6?   �� 5   
   -   5        $ Bishop��5      $��3��6C   �� 5   
   -   5         ��Name$ BtnClose��5      $��4��61   ��5      �� 5        �� 
TargetType" ��6"   .R�5!      �� ���#f1f1f1._�5"      ��  J� IsHitTestVisible$ False��6    $��10q�69   $E��?C:\Users\84352\Desktop\ChessGame\ChessGame\Assets\Gui\Close.png��6E   5#       �� Triggers 5$      P� 5%       P� Property �@J�IsMouseOver P� Value$
 True��69   . �5&      ��  �� Property � �� �� Value �#f1f1f16;   5'      P� 5(       � ��$
 True��67   . �5)      ��  � �� �#f1f1f16;   5*      5,      5.      5/      50      