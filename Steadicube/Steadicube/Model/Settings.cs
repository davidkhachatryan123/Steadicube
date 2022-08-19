using Steadicube.Enums;
using System.IO;
using System.Text.Json;

namespace Steadicube.Model
{
    public class Settings
    {
        public string ComPort { get; set; } = string.Empty;
        public int BaudRate { get; set; }
        public Guid Joystick { get; set; }
        public double CameraSpeed { get; set; }

        public Cube cube { get; set; }
        public Camera camera { get; set; }



        public Settings()
        {
            this.ComPort = "COM1";
            this.BaudRate = 9600;
            this.CameraSpeed = 1.0;
        }

        public Settings(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open))
                {
                    Settings _settings = JsonSerializer.Deserialize<Settings>(fs);

                    this.ComPort = _settings!.ComPort;
                    this.BaudRate = _settings!.BaudRate;
                    this.Joystick = _settings!.Joystick;
                    this.CameraSpeed = _settings!.CameraSpeed;

                    this.cube = _settings.cube;
                    this.camera = _settings.camera;
                }
            }
        }

        public Settings(string ComPort, int BaudRate, Guid Joystick, int CameraSpeed, Cube cube, Camera camera)
        {
            this.ComPort = ComPort;
            this.BaudRate = BaudRate;
            this.Joystick = Joystick;
            this.CameraSpeed = CameraSpeed;

            this.cube = cube;
            this.camera = camera;
        }
    }
}
