using Steadicube.Classes;
using Steadicube.Enums;
using Steadicube.ViewModel;
using System.Windows.Media.Media3D;

namespace Steadicube.Model
{
    public class Camera
    {
        public readonly double width = 200;
        public readonly double length = 200;
        public readonly double height = 200;

        public Position position { get; set; } = new Position();
        public Position positionBinding { get; set; } = new Position();

        public void MoveCamera(JoystickMovement joyStickMovement, Cube cube, S_Mode mode, Settings settings, Action<Vector4D> sendArduino)
        {
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

                StatusBar3DViewModel.statusBar3DViewModel.vector3D = new Vector3D(
                    Math.Round(position.X, 2),
                    Math.Round(position.Y, 2),
                    Math.Round(position.Z, 2));


                sendArduino.Invoke(GetVectors(StatusBar3DViewModel.statusBar3DViewModel.vector3D, cube));
            }
            else if (mode == S_Mode.S2)
            {

            }
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
    }
}
