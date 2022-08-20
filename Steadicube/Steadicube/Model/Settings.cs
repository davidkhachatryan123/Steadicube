using Steadicube.Enums;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Steadicube.Model
{
    public class Settings
    {
        public string ComPort { get; set; } = "COM1";
        public int BaudRate { get; set; } = 9600;
        public Guid JoystickGUID { get; set; }
        public double CameraSpeed { get; set; } = 0.001;

        public Cube cube { get; set; }

        [JsonIgnore]
        public Serial serial { get; set; }
        [JsonIgnore]
        public JoyStick joystick { get; set; }


        public Settings()
        {

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
                    this.JoystickGUID = _settings!.JoystickGUID;
                    this.CameraSpeed = _settings!.CameraSpeed;

                    this.cube = _settings.cube;
                }
            }
        }

        public Settings(string ComPort, int BaudRate, Guid JoystickGUID, int CameraSpeed, Cube cube)
        {
            this.ComPort = ComPort;
            this.BaudRate = BaudRate;
            this.JoystickGUID = JoystickGUID;
            this.CameraSpeed = CameraSpeed;

            this.cube = cube;
        }
    }
}
