   M S B A M L   `   `   `  ???? E  ADragablz, Version=0.0.3.223, Culture=neutral, PublicKeyToken=null5(clr-namespace:Dragablz;assembly=DragablzDragablz  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity E ASteadicube, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Steadicube.ViewSteadicube.View ;"clr-namespace:Steadicube.ViewModelSteadicube.ViewModel 3clr-namespace:Steadicube.ModelSteadicube.Model    Steadicube.View.Config   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  7dragablz(clr-namespace:Dragablz;assembly=Dragablz  XiPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity  W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     /localclr-namespace:Steadicube.View     8	viewmodel"clr-namespace:Steadicube.ViewModel     0Modelclr-namespace:Steadicube.Model     .  (System.Windows.Interactivity.Interaction    Triggers  /  )System.Windows.Interactivity.EventTrigger     	EventName$ Loaded??.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command ?? *LoadedCommand+   CommandParameter ??  ?? RelativeSource ?? *FindAncestor+ ?? AncestorType" >?.??? .+?   Dragablz.TabablzControl  $??2??.@??? $??Scene??.???  ? RowDefinitions ?? $??95 ??? $??95 ??? $??* ??? $??60 ?.+?? -    ?Name$
 Cube?? ?? $??* ??? $??60 ?	 ? ColumnDefinitions	 ?? $??* ??? $??* ??? $??* ?.+??? $??Scene Size:??$
??Bold"?$??	10 5 10 5q??? )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 Length??
???   ?.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush ?#3CAB76   
Foreground ?#3CAB76 !  MaterialDesignOutlinedTextBox#??[     HasClearButton$
 True??$??	10 5 10 5q?$??1??$??0??????  ?? Path$ 
LengthBind? ?? UpdateSourceTrigger$ PropertyChanged=??? $
 Width??
???   ? ?#3CAB76 ?#3CAB76#??[  $
 True??$??	10 5 10 5q?$??1??$??1?????? $ 	WidthBind?$ PropertyChanged=??? $
 Height??
???   ? ?#3CAB76 ?#3CAB76#??[  $
 True??$??	10 5 10 5q?$??1??$??2?????? $ 
HeightBind?$ PropertyChanged=?? -   $ Camera??$??1?? ?? $??* ??? $??60 ?	 ?? $??* ??? $??* ??? $??* ?.+??? $??Camera Position:??$
??Bold"?$??	10 5 10 5q??? $
 X??
???   ? ?#3CAB76 ?#3CAB76#??[  $ False??$??	10 5 10 5q?$??1??$??0?????? $ positionBinding.X?$ PropertyChanged=??? $
 Y??
???   ? ?#3CAB76 ?#3CAB76#??[  $ False??$??	10 5 10 5q?$??1??$??1?????? $ positionBinding.Y?$ PropertyChanged=??? $
 Z??
???   ? ?#3CAB76 ?#3CAB76#??[  $ False??$??	10 5 10 5q?$??1??$??2?????? $ positionBinding.Z?$ PropertyChanged=??? $	??Set??
??????????#3CAB76???#3CAB76$??	10 0 10 0q?$??10?????? *
SetCommand+????  ?? *FindAncestor+" >??? $??Settings??.???  ?? $??95 ??? $??95 ??? $??50 ??? $??50 ??? $??50 ??? $??95 ??? $??* ??? $??60 ?.+??  ?? $??* ??? $??60 ?	 ?? $??* ??? $??* ??? $??60 ?$??60??$??60??.+??? $??Serial Config:??$
??Bold"?$??	10 5 10 5q?? $??1??$??0??$??10 0 0 0q?	 ?? $??50 ??? $??* ?.+??? $
??COM:??$??0??$??Center=?$??Center=??? $??0 5 0 5q?$??1??-  'MaterialDesignThemes.Wpf.ComboBoxAssist   ShowSelectedItem$
 True??   ClassicMode$
 True?? " MaterialDesignOutlinedComboBox#??[  ?? SelectedItem ?? *ComPortValue+???? *ComPortValues+? $??1??$??1??$??0 0 10 0q?	 ?? $??50 ??? $??* ?.+??? $??Baud:??$??0??$??Center=?$??Center=??? $??0 5 0 5q?$??1??$
 True??$
 True??#??[  ?? *BaudRateValue+???? *BaudRateValues+?? $??0 0 10 0q?$??1??$??2??
??????????#3CAB76???#3CAB76$??50??$??50?????? *SerialRefreshBtn+.??'	  !MaterialDesignThemes.Wpf.PackIcon	   	  Kind$ Refresh=?? $??1?? ?? $??* ??? $??60 ?.+??? $??	Joystick:??$??0??$
??Bold"?$??10 0 0 0q?? $??1??	 ?? $??* ??? $??60 ?$??60??$??60??.+??? -    ??Name$ JOYSTICK_VALUE??$??	10 5 10 5q?$
 True??$
 True??#??[ ???? *JoystickDevices+ ?? *JoystickDevice+?? $??0 0 10 0q?$??1??
??????????#3CAB76???#3CAB76$??50??$??50?????? *JoystickRefreshBtn+.??	  $ Refresh=?? $??2?? ?? $??* ??? $??25 ?.+??? $??Speed:??$??0??$
??Bold"?$??10 0 0 0q??? ???#3CAB76 ? Minimum$ 0Q? ? Maximum$	 0.1Q?$??Center=?$??	10 0 10 0q?$??1?? ? Value ?? *SpeedSliderValue+? $??3?? ?? $??* ??? $??25 ?.+??? $??Camera Z Speed:??$
??Bold"?$??10 0 0 0q??? ???#3CAB76$ 0Q?$	 0.1Q?$??Center=?$??	10 0 10 0q?$??1?? ?? *Z_Speed_SliderValue+? $??4?? ?? $??* ??? $??25 ?.+??? $??Camera X Speed:??$
??Bold"?$??10 0 0 0q??? ???#3CAB76$ 0Q?$	 0.1Q?$??Center=?$??	10 0 10 0q?$??1?? ?? *X_Speed_SliderValue+? $??5?? ?? $??* ??? $??60 ?	 ?? $??* ??? $??* ?.+??? $??Servo X:??$
??Bold"?$??10 0 0 0q??? $	
 Max??
???   ? ?#3CAB76 ?#3CAB76#??[  $
 True??$??	10 5 10 5q?$??1??$??0?????? $ Servo_X_Max_Bind?$ PropertyChanged=??? $
 Center??
???   ? ?#3CAB76 ?#3CAB76#??[  $
 True??$??	10 5 10 5q?$??1??$??1?????? $ Servo_X_Center_Bind?$ PropertyChanged=??? $
??Save??
??????????#3CAB76???#3CAB76$??	10 0 10 0q?$??10?????? *SaveCommand+?? $??Calibration??.???  ?? $	??100 ??? $??20 ??? $	??100 ??? $??* ?.+?? $??0??	 ?? ?? ?? ?? .+??? $??A??$??0??
???????
???????
???????$??50??$??50??$??Center=?$??Center=????? *A_NullCommand+?? $??B??$??1??
???????
???????
???????$??50??$??50??$??Center=?$??Center=????? *B_NullCommand+?? $??C??$??2??
???????
???????
???????$??50??$??50??$??Center=?$??Center=????? *C_NullCommand+?? $??D??$??3??
???????
???????
???????$??50??$??50??$??Center=?$??Center=????? *D_NullCommand+? -   $ Calibration??$??2?? ?? ?? 	 ?? ?? ?? ?? .+??? $	??A +??$??0??$??0??
???   ?
??? ? ?
??? ? ?$??70??$??50??$??Center=?$??Center=????? *A_Plus+?? $	??A -??$??1??$??0??
???   ?
???  ??
???  ??$??70??$??50??$??Center=?$??Center=????? *	A_Minus+?? $	??B +??$??0??$??1??
???   ?
??? ? ?
??? ? ?$??70??$??50??$??Center=?$??Center=????? *B_Plus+?? $	??B -??$??1??$??1??
???   ?
???  ??
???  ??$??70??$??50??$??Center=?$??Center=????? *	B_Minus+?? $	??C +??$??0??$??2??
???   ?
??? ? ?
??? ? ?$??70??$??50??$??Center=?$??Center=????? *C_Plus+?? $	??C -??$??1??$??2??
???   ?
???  ??
???  ??$??70??$??50??$??Center=?$??Center=????? *	C_Minus+?? $	??D +??$??0??$??3??
???   ?
??? ? ?
??? ? ?$??70??$??50??$??Center=?$??Center=????? *D_Plus+?? $	??D -??$??1??$??3??
???   ?
???  ??
???  ??$??70??$??50??$??Center=?$??Center=????? *	D_Minus+