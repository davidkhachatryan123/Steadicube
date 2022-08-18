using System.Diagnostics;

namespace Steadicube.Classes
{
    public class JoystickCapture
    {
        private static readonly int max = 65535;
        private static readonly int center = max / 2;
        private static readonly int start_offset = 2000;

        // PS4
        public static void Capture(SharpDX.DirectInput.JoystickOffset offset, int value, JoystickMovement joystickMovement)
        {
            switch (offset)
            {
                case SharpDX.DirectInput.JoystickOffset.X:
                    if (value > center + start_offset)
                        joystickMovement.Left_Stick_X = -ExtensionMethods.Map(value, center, 0, 0, 100);
                    else if (value < center - start_offset)
                        joystickMovement.Left_Stick_X = ExtensionMethods.Map(value, center, max, 0, 100);
                    else
                        joystickMovement.Left_Stick_X = 0;
                    break;

                case SharpDX.DirectInput.JoystickOffset.Y:
                    if (value > center + start_offset)
                        joystickMovement.Left_Stick_Y = -ExtensionMethods.Map(value, center, 0, 0, 100);
                    else if (value < center - start_offset)
                        joystickMovement.Left_Stick_Y = ExtensionMethods.Map(value, center, max, 0, 100);
                    else
                        joystickMovement.Left_Stick_Y = 0;
                    break;


                case SharpDX.DirectInput.JoystickOffset.Z:
                    if (value > center + start_offset)
                        joystickMovement.Right_Stick_X = -ExtensionMethods.Map(value, center, 0, 0, 100);
                    else if (value < center - start_offset)
                        joystickMovement.Right_Stick_X = ExtensionMethods.Map(value, center, max, 0, 100);
                    else
                        joystickMovement.Right_Stick_X = 0;
                    break;

                case SharpDX.DirectInput.JoystickOffset.RotationZ:
                    if (value > center + start_offset)
                        joystickMovement.Right_Stick_Y = -ExtensionMethods.Map(value, center, max, 0, 100);
                    else if (value < center - start_offset)
                        joystickMovement.Right_Stick_Y = ExtensionMethods.Map(value, center, 0, 0, 100);
                    else
                        joystickMovement.Right_Stick_Y = 0;
                    break;



                case SharpDX.DirectInput.JoystickOffset.RotationX:
                    if (value > start_offset)
                        joystickMovement.L2 = -ExtensionMethods.Map(value, start_offset, max, 0, 100);
                    else
                        joystickMovement.L2 = 0;
                    break;

                case SharpDX.DirectInput.JoystickOffset.RotationY:
                    if (value > start_offset)
                        joystickMovement.R2 = ExtensionMethods.Map(value, start_offset, max, 0, 100);
                    else
                        joystickMovement.R2 = 0;
                    break;


                case SharpDX.DirectInput.JoystickOffset.Buttons4:
                    if (value > 0)
                        joystickMovement.L1 = true;
                    else
                        joystickMovement.L1 = false;
                    break;
                case SharpDX.DirectInput.JoystickOffset.Buttons5:
                    if (value > 0)
                        joystickMovement.R1 = true;
                    else
                        joystickMovement.R1 = false;
                    break;



                case SharpDX.DirectInput.JoystickOffset.Buttons3:
                    if (value > 0)
                        joystickMovement.Right_Btn_UP = true;
                    else
                        joystickMovement.Right_Btn_UP = false;
                    break;
                case SharpDX.DirectInput.JoystickOffset.Buttons1:
                    if (value > 0)
                        joystickMovement.Right_Btn_DOWN = true;
                    else
                        joystickMovement.Right_Btn_DOWN = false;
                    break;
                case SharpDX.DirectInput.JoystickOffset.Buttons2:
                    if (value > 0)
                        joystickMovement.Right_Btn_RIGHT = true;
                    else
                        joystickMovement.Right_Btn_RIGHT = false;
                    break;
                case SharpDX.DirectInput.JoystickOffset.Buttons0:
                    if (value > 0)
                        joystickMovement.Right_Btn_LEFT = true;
                    else
                        joystickMovement.Right_Btn_LEFT = false;
                    break;


                case SharpDX.DirectInput.JoystickOffset.PointOfViewControllers0:
                    if (value == 0)
                        joystickMovement.Left_Btn_UP = true;
                    else if (value == 18000)
                        joystickMovement.Left_Btn_DOWN = true;
                    else if (value == 9000)
                        joystickMovement.Left_Btn_RIGHT = true;
                    else if (value == 27000)
                        joystickMovement.Left_Btn_LEFT = true;
                    else
                    {
                        joystickMovement.Left_Btn_UP = false;
                        joystickMovement.Left_Btn_DOWN = false;
                        joystickMovement.Left_Btn_RIGHT = false;
                        joystickMovement.Left_Btn_LEFT = false;
                    }
                    break;
            }
        }
    }
}
