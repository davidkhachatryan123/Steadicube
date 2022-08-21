using Steadicube.Classes;
using Steadicube.Enums;
using Steadicube.ViewModel;
using System.Diagnostics;
using System.Windows.Media.Media3D;

namespace Steadicube.Model
{
    public class Camera
    {
        public readonly double width = 0;
        public readonly double length = 0;
        public readonly double height = 0;


        public Position rotation { get; set; } = new Position();
        public Position rotation1 { get; set; } = new Position();


        public Position position { get; set; } = new Position();
        public Position positionBinding { get; set; } = new Position();

        public void MoveCamera(JoystickMovement joyStickMovement, Cube cube, S_Mode mode, Settings settings, Action<Vector4D> sendArduino)
        {
            rotation1.X = 90;
            rotation.Z = 0;

            if (mode == S_Mode.S1)
            {
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
            }
            else if (mode == S_Mode.S2)
            {
                if ((position.X >= 0 && position.X <= cube.Length)
                    && (position.Y >= 0 && position.Y <= cube.Width))
                {
                    position.X += (settings.CameraSpeed * joyStickMovement.Left_Stick_Y) * Math.Cos(rotation.Z);
                    position.Y -= (settings.CameraSpeed * joyStickMovement.Left_Stick_Y) * Math.Sin(rotation.Z);

                    position.X += (settings.CameraSpeed * joyStickMovement.Left_Stick_X) * Math.Cos(rotation.Z);
                    position.Y += (settings.CameraSpeed * joyStickMovement.Left_Stick_X) * Math.Sin(rotation.Z);
                }
                else
                {
                    if (position.X <= 0)
                        position.X = 0;
                    if (position.X >= cube.Length)
                        position.X = cube.Length;
                    if (position.Y <= 0)
                        position.Y = 0;
                    if (position.Y >= cube.Width)
                        position.Y = cube.Width;
                }
            }


            StatusBar3DViewModel.statusBar3DViewModel.vector3D = new Vector3D(
                    Math.Round(position.X, 2),
                    Math.Round(position.Y, 2),
                    Math.Round(position.Z, 2));


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
