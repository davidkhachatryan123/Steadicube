using Steadicube.Classes;
using Steadicube.Enums;
using Steadicube.ViewModel;
using System.Windows.Media.Media3D;

namespace Steadicube.Model
{
    public class Camera
    {
        private readonly double width = 200;
        private readonly double length = 200;
        private readonly double height = 200;

        public Position position { get; set; } = new Position();
        public Position positionBinding { get; set; } = new Position();

        public void MoveCamera(JoystickMovement joyStickMovement, Cube cube, S_Mode mode, Settings settings)
        {
            if (mode == S_Mode.S1)
            {
                if (position.X + joyStickMovement.Left_Stick_X * settings.CameraSpeed >= 0 + length
                    && position.X + joyStickMovement.Left_Stick_X * settings.CameraSpeed <= cube.Length - length)
                    position.X += joyStickMovement.Left_Stick_X * settings.CameraSpeed;
                else
                {
                    if (position.X > cube.Length / 2)
                        position.X = cube.Length - length;
                    else
                        position.X = length;
                }

                if (position.Y - joyStickMovement.Left_Stick_Y * settings.CameraSpeed >= 0 + width
                    && position.Y - joyStickMovement.Left_Stick_Y * settings.CameraSpeed <= cube.Width - width)
                    position.Y -= joyStickMovement.Left_Stick_Y * settings.CameraSpeed;
                else
                {
                    if (position.Y > cube.Width / 2)
                        position.Y = cube.Width - width;
                    else
                        position.Y = width;
                }


                if (position.Z - joyStickMovement.R2 * settings.CameraSpeed >= height)
                    position.Z -= joyStickMovement.R2 * settings.CameraSpeed;
                else
                {
                    if (position.Z < cube.Height / 2)
                        position.Z = height;
                }

                if (position.Z - joyStickMovement.L2 * settings.CameraSpeed <= cube.Height - height)
                    position.Z -= joyStickMovement.L2 * settings.CameraSpeed;
                else
                {
                    if (position.Z > cube.Height / 2)
                    {
                        position.Z = cube.Height - height;
                    }
                }

                StatusBar3DViewModel.statusBar3DViewModel.vector3D = new Vector3D(
                    Math.Round(position.X, 2),
                    Math.Round(position.Y, 2),
                    Math.Round(position.Z, 2));
            }
            else if (mode == S_Mode.S2)
            {

            }
        }
    }
}
