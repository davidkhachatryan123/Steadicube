using System.IO;
using System.Text.Json;

namespace Steadicube.Model
{
    public class Settings
    {
        public string ComPort { get; set; } = string.Empty;
        public int BaudRate { get; set; }
        public Guid Joystick { get; set; }
        public int CameraSpeed { get; set; }

        public Settings()
        {
            this.ComPort = "COM1";
            this.BaudRate = 9600;
            this.CameraSpeed = 10;
        }

        public Settings(string FilePath)
        {
            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                Settings _settings = JsonSerializer.Deserialize<Settings>(fs);

                this.ComPort = _settings!.ComPort;
                this.BaudRate = _settings!.BaudRate;
                this.Joystick = _settings!.Joystick;
                this.CameraSpeed = _settings!.CameraSpeed;
            }
        }

        public Settings(string ComPort, int BaudRate, Guid Joystick, int CameraSpeed)
        {
            this.ComPort = ComPort;
            this.BaudRate = BaudRate;
            this.Joystick = Joystick;
            this.CameraSpeed = CameraSpeed;
        }
    }
}
