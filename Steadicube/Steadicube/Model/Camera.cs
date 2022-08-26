using Steadicube.Classes;
using Steadicube.Enums;
using Steadicube.ViewModel;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Windows.Media.Media3D;

namespace Steadicube.Model
{
    public class Camera
    {
        public readonly double width = 0;
        public readonly double length = 0;
        public readonly double height = 0;

        public Position rotation { get; set; } = new Position();
        public Position rotationForArduino { get; set; } = new Position();

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
            }
            else if (mode == S_Mode.S2)
            {
                float yaw = (float)(Math.PI * -rotation.Z / 180); // -rotation.Z
                float pitch = (float)(Math.PI * rotation.X / 180); // rotation.X

                Vector3 translationVector = new Vector3(
                    (float)(joyStickMovement.Left_Stick_X),
                    (float)(-joyStickMovement.Left_Stick_Y),
                    (float)(-joyStickMovement.R2 - joyStickMovement.L2));

                translationVector = Vector3.Normalize(translationVector);


                Matrix4x4 translationMatrix = Matrix4x4.CreateTranslation(translationVector);

                Matrix4x4 x_rotationMatrix = Matrix4x4.CreateRotationX(pitch);
                Matrix4x4 z_rotationMatrix = Matrix4x4.CreateRotationZ(yaw);

                Matrix4x4 result;
                result = Matrix4x4.Multiply(translationMatrix, x_rotationMatrix);
                result = Matrix4x4.Multiply(result, z_rotationMatrix);


                Vector3 newAddingVector = result.Translation;

                position.X += float.IsNaN(newAddingVector.X) ? 0 : newAddingVector.X * settings.CameraSpeed;
                position.Y += float.IsNaN(newAddingVector.Y) ? 0 : newAddingVector.Y * settings.CameraSpeed;
                position.Z += float.IsNaN(newAddingVector.Z) ? 0 : newAddingVector.Z * settings.CameraSpeed;



                /*if (!running)
                {
                    running = true;

                    sender = new UdpClient();
                }

                string message1 = "x:" + position.X;
                string message2 = "y:" + position.Y;
                string message3 = "z:" + position.Z;
                string message4 = "a:" + rotation.Z;
                string message5 = "p:" + rotation.X;
                byte[] data1 = Encoding.Unicode.GetBytes(message1);
                byte[] data2 = Encoding.Unicode.GetBytes(message2);
                byte[] data3 = Encoding.Unicode.GetBytes(message3);
                byte[] data4 = Encoding.Unicode.GetBytes(message4);
                byte[] data5 = Encoding.Unicode.GetBytes(message5);

                sender.Send(data1, data1.Length, "127.0.0.1", 8004);
                sender.Send(data2, data2.Length, "127.0.0.1", 8004);
                sender.Send(data3, data3.Length, "127.0.0.1", 8004);
                sender.Send(data4, data4.Length, "127.0.0.1", 8004);
                sender.Send(data5, data5.Length, "127.0.0.1", 8004);*/
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
                if (rotationForArduino.X + Math.Abs(joystickMovement.Right_Stick_Y * settings.Servo_X_Speed) <= settings.Servo_X_Max)
                    rotationForArduino.X += Math.Abs(joystickMovement.Right_Stick_Y * settings.Servo_X_Speed);
                else
                    rotationForArduino.X = settings.Servo_X_Max;
            }
            else
            {
                if (rotationForArduino.X - Math.Abs(joystickMovement.Right_Stick_Y * settings.Servo_X_Speed) >= 0)
                    rotationForArduino.X -= Math.Abs(joystickMovement.Right_Stick_Y * settings.Servo_X_Speed);
                else
                    rotationForArduino.X = 0;
            }

            rotation.X = settings.Servo_X_Center - rotationForArduino.X; // convert arduino angle to rotation angle for pitch

            sendArduino.Invoke(rotation.Z, rotationForArduino.X);
        }
    }
}
