using Steadicube.Classes;
using Steadicube.Enums;
using Steadicube.ViewModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Steadicube.Model
{
    public class Camera
    {
        public readonly double width = 0;
        public readonly double length = 0;
        public readonly double height = 0;

        private Position rotatedPosition = new Position();
        private Position ancacgnacac = new Position();


        public Position rotation { get; set; } = new Position();
        public Position rotation1 { get; set; } = new Position();

        public Position position { get; set; } = new Position();
        public Position positionBinding { get; set; } = new Position();

        UdpClient sender;
        bool running = false;

        bool Mode_2_first = false;
        public void MoveCamera(JoystickMovement joyStickMovement, Cube cube, S_Mode mode, Settings settings, Action<Vector4D> sendArduino)
        {
            if (mode == S_Mode.S1)
            {
                Mode_2_first = true;

                if (position.X + joyStickMovement.Left_Stick_X * settings.CameraSpeed >= length / 2
                    && position.X + joyStickMovement.Left_Stick_X * settings.CameraSpeed <= cube.Length - length / 2)
                    position.X += joyStickMovement.Left_Stick_X * settings.CameraSpeed;
                else
                {
                    if (position.X > cube.Length / 2)
                        position.X = cube.Length - length / 2;
                    else
                        position.X = length / 2;
                }

                if (position.Y - joyStickMovement.Left_Stick_Y * settings.CameraSpeed >= width / 2
                    && position.Y - joyStickMovement.Left_Stick_Y * settings.CameraSpeed <= cube.Width - width / 2)
                    position.Y -= joyStickMovement.Left_Stick_Y * settings.CameraSpeed;
                else
                {
                    if (position.Y > cube.Width / 2)
                        position.Y = cube.Width - width / 2;
                    else
                        position.Y = width / 2;
                }


                if (position.Z - joyStickMovement.R2 * settings.CameraSpeed >= height / 2)
                    position.Z -= joyStickMovement.R2 * settings.CameraSpeed;
                else
                {
                    if (position.Z < cube.Height / 2)
                        position.Z = height / 2;
                }

                if (position.Z - joyStickMovement.L2 * settings.CameraSpeed <= cube.Height - height / 2)
                    position.Z -= joyStickMovement.L2 * settings.CameraSpeed;
                else
                {
                    if (position.Z > cube.Height / 2)
                    {
                        position.Z = cube.Height - height / 2;
                    }
                }


                StatusBar3DViewModel.statusBar3DViewModel.vector3D = new Vector3D(
                    Math.Round(position.X, 2),
                    Math.Round(position.Y, 2),
                    Math.Round(position.Z, 2));
            }
            else if (mode == S_Mode.S2)
            {
                double alfa = 45;
                double betta = 45;
                double gamma = 0;


                if (Mode_2_first)
                {
                    alfa = -45;
                    betta = -45;

                    /*rotatedPosition.X = position.X * Math.Cos(alfa) - position.Y * Math.Sin(alfa);
                    rotatedPosition.Y = position.X * Math.Sin(alfa) + position.Y * Math.Cos(alfa);*/

                    double a = position.X * (Math.Cos(alfa) * Math.Cos(gamma) - Math.Sin(alfa) * Math.Cos(betta) * Math.Sin(gamma))
                                      + position.Y * (-Math.Sin(alfa) * Math.Cos(gamma) - Math.Cos(alfa) * Math.Cos(betta) * Math.Sin(gamma))
                                      + position.Z * (Math.Sin(betta) * Math.Sin(gamma));
                    double b = position.X * (Math.Cos(alfa) * Math.Sin(gamma) + Math.Sin(alfa) * Math.Cos(betta) * Math.Cos(gamma))
                                      + position.Y * (-Math.Sin(alfa) * Math.Sin(gamma) + Math.Cos(alfa) * Math.Cos(betta) * Math.Cos(gamma))
                                      + position.Z * (-Math.Sin(betta) * Math.Cos(gamma));
                    double c = position.Y * (Math.Cos(alfa) * Math.Sin(betta))
                                      + position.Z * (Math.Cos(betta));

                    position.X = a;
                    position.Y = b;
                    position.Z = c;

                    /*rotatedPosition.X = position.X;
                    rotatedPosition.Y = position.Y;
                    rotatedPosition.Z = position.Z;*/

                    Mode_2_first = false;

                    return;
                }


                if (rotatedPosition.X >= 0)
                {
                    ancacgnacac.X = position.X;

                    position.X += joyStickMovement.Left_Stick_X * settings.CameraSpeed;

                    rotatedPosition.X = position.X * (Math.Cos(alfa) * Math.Cos(gamma) - Math.Sin(alfa) * Math.Cos(betta) * Math.Sin(gamma))
                                      + position.Y * (-Math.Sin(alfa) * Math.Cos(gamma) - Math.Cos(alfa) * Math.Cos(betta) * Math.Sin(gamma))
                                      + position.Z * (Math.Sin(betta) * Math.Sin(gamma));

                    if (rotatedPosition.X < 0)
                    {
                        rotatedPosition.X = 0;

                        position.X = ancacgnacac.X;
                        position.Y = ancacgnacac.Y;
                        position.Z = ancacgnacac.Z;
                    }
                }

                if (rotatedPosition.Y >= 0)
                {
                    ancacgnacac.Y = position.Y;

                    position.Y -= joyStickMovement.Left_Stick_Y * settings.CameraSpeed;

                    rotatedPosition.Y = position.X * (Math.Cos(alfa) * Math.Sin(gamma) + Math.Sin(alfa) * Math.Cos(betta) * Math.Cos(gamma))
                                      + position.Y * (-Math.Sin(alfa) * Math.Sin(gamma) + Math.Cos(alfa) * Math.Cos(betta) * Math.Cos(gamma))
                                      + position.Z * (-Math.Sin(betta) * Math.Cos(gamma));

                    if (rotatedPosition.Y < 0)
                    {
                        rotatedPosition.Y = 0;

                        position.X = ancacgnacac.X;
                        position.Y = ancacgnacac.Y;
                        position.Z = ancacgnacac.Z;
                    }
                }

                if (rotatedPosition.Z >= 0)
                {
                    ancacgnacac.Z = position.Z;

                    position.Z -= joyStickMovement.R2 * settings.CameraSpeed;
                    position.Z -= joyStickMovement.L2 * settings.CameraSpeed;

                    rotatedPosition.Z = position.Y * (Math.Cos(alfa) * Math.Sin(betta))
                                        + position.Z * (Math.Cos(betta));

                    if (rotatedPosition.Z < 0)
                    {
                        rotatedPosition.Z = 0;

                        position.X = ancacgnacac.X;
                        position.Y = ancacgnacac.Y;
                        position.Z = ancacgnacac.Z;
                    }
                }



                if (!running)
                {
                    running = true;

                    sender = new UdpClient();
                }

                string message1 = "x:" + rotatedPosition.X;
                string message2 = "y:" + rotatedPosition.Y;
                string message3 = "z:" + rotatedPosition.Z;
                byte[] data1 = Encoding.Unicode.GetBytes(message1);
                byte[] data2 = Encoding.Unicode.GetBytes(message2);
                byte[] data3 = Encoding.Unicode.GetBytes(message3);

                sender.Send(data1, data1.Length, "127.0.0.1", 8005);
                sender.Send(data2, data2.Length, "127.0.0.1", 8005);
                sender.Send(data3, data3.Length, "127.0.0.1", 8005);



                StatusBar3DViewModel.statusBar3DViewModel.vector3D = new Vector3D(
                    Math.Round(rotatedPosition.X, 2),
                    Math.Round(rotatedPosition.Y, 2),
                    Math.Round(rotatedPosition.Z, 2));
            }


            sendArduino.Invoke(GetVectors(StatusBar3DViewModel.statusBar3DViewModel.vector3D, cube));
        }

        private Vector4D GetVectors(Vector3D cameraPos, Cube cube)
        {
            return new Vector4D
            {
                A = Math.Sqrt(
                Math.Pow(0 - (cameraPos.X - length / 2), 2) +
                Math.Pow(0 - (cameraPos.Y - width / 2), 2) +
                Math.Pow(0 - (cameraPos.Z - height / 2), 2)
                ),
                B = Math.Sqrt(
                Math.Pow(cube.Length - (cameraPos.X + length / 2), 2) +
                Math.Pow(0 - (cameraPos.Y - width / 2), 2) +
                Math.Pow(0 - (cameraPos.Z - height / 2), 2)
                ),
                C = Math.Sqrt(
                Math.Pow(0 - (cameraPos.X - length / 2), 2) +
                Math.Pow(cube.Width - (cameraPos.Y + width / 2), 2) +
                Math.Pow(0 - (cameraPos.Z - height / 2), 2)
                ),
                D = Math.Sqrt(
                Math.Pow(cube.Length - (cameraPos.X + length / 2), 2) +
                Math.Pow(cube.Width - (cameraPos.Y + width / 2), 2) +
                Math.Pow(0 - (cameraPos.Z - height / 2), 2)
                )
            };
        }


        public void Rotate(JoystickMovement joystickMovement, Settings settings, Action<double, double> sendArduino)
        {
            if (joystickMovement.Right_Stick_X > 0)
            {
                if (rotation.Z + Math.Abs(joystickMovement.Right_Stick_X * settings.Servo_Z_Speed) < 360)
                    rotation.Z += Math.Abs(joystickMovement.Right_Stick_X * settings.Servo_Z_Speed);
                else
                    rotation.Z = 0;
            }
            else
            {
                if (rotation.Z - Math.Abs(joystickMovement.Right_Stick_X * settings.Servo_Z_Speed) > 0)
                    rotation.Z -= Math.Abs(joystickMovement.Right_Stick_X * settings.Servo_Z_Speed);
                else
                    rotation.Z = 359;
            }


            if (joystickMovement.Right_Stick_Y > 0)
            {
                if (rotation1.X + Math.Abs(joystickMovement.Right_Stick_Y * settings.Servo_X_Speed) <= settings.Servo_X_Max)
                    rotation1.X += Math.Abs(joystickMovement.Right_Stick_Y * settings.Servo_X_Speed);
                else
                    rotation1.X = settings.Servo_X_Max;
            }
            else
            {
                if (rotation1.X - Math.Abs(joystickMovement.Right_Stick_Y * settings.Servo_X_Speed) >= 0)
                    rotation1.X -= Math.Abs(joystickMovement.Right_Stick_Y * settings.Servo_X_Speed);
                else
                    rotation1.X = 0;
            }

            sendArduino.Invoke(rotation.Z, rotation1.X);
        }
    }
}
