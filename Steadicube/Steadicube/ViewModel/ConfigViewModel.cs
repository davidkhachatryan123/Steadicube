using Steadicube.Classes;
using Steadicube.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Text.Json;
using System.IO;
using Steadicube.View;

using DeviceList = System.Collections.Generic.Dictionary<System.Guid, string>;
using System.Windows.Media.Media3D;

namespace Steadicube.ViewModel
{
    public class ConfigViewModel : INotifyPropertyChanged
    {
        public static ConfigViewModel configViewModel { get; set; }

        public Settings settings;
        public Cube cube;
        public Model.Camera camera;

        private DeviceList joystickDeviceList;


        private RelayCommand loadedCommand;
        public RelayCommand LoadedCommand
        {
            get
            {
                return loadedCommand ??
                  (loadedCommand = new RelayCommand(obj =>
                  {
                      configViewModel = this;


                      settings = new Settings("settings.json");

                      settings.joystick = new JoyStick();
                      joystickDeviceList = settings.joystick.Devices;
                      JoystickDevices = joystickDeviceList.Values;

                      if (settings.JoystickGUID != Guid.Empty)
                          try
                          {
                              JoystickDevice = joystickDeviceList.Where(device => device.Key == settings.JoystickGUID).First().Value;
                          }
                          catch
                          {
                              settings.JoystickGUID = Guid.Empty;
                          }

                      SpeedSliderValue = settings.CameraSpeed;


                      settings.serial = new Serial();
                      ComPortValues = settings.serial.Ports;
                      BaudRateValues = settings.serial.Bauds;


                      try
                      {
                          if (ComPortValues.Where(port => port == settings.ComPort).First() != string.Empty)
                              ComPortValue = settings.ComPort;
                      }
                      catch
                      {
                          ComPortValue = ComPortValues.FirstOrDefault();
                      }

                      BaudRateValue = settings.BaudRate.ToString();


                      cube = (Cube)(obj as Config).Cube.DataContext;
                      cube!.WidthBind = settings!.cube?.WidthBind == null ? 30000 : settings.cube.WidthBind;
                      cube!.HeightBind = settings!.cube?.HeightBind == null ? 30000 : settings.cube.HeightBind;
                      cube!.LengthBind = settings!.cube?.LengthBind == null ? 30000 : settings.cube.LengthBind;

                      camera = (Model.Camera)(obj as Config).Camera.DataContext;
                      camera!.position.X = camera.positionBinding.X = cube.LengthBind / 2;
                      camera!.position.Y = camera.positionBinding.Y = cube.WidthBind / 2;
                      camera!.position.Z = camera.positionBinding.Z = cube.HeightBind;
                      camera!.rotationForArduino = new Position(settings.Servo_X_Center, 0, 0);

                      Set(null);
                  }));
            }
        }


        CancellationTokenSource cancellationTokenSource;
        private void StartProgram()
        {
            if (cancellationTokenSource != null)
                cancellationTokenSource.Cancel();

            settings.joystick.DeleteJoystick();

            settings.serial.ClosePort();
            settings.serial.OpenPort(settings.ComPort, settings.BaudRate);


            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken ct = cancellationTokenSource.Token;

            if (settings.joystick.SetJoystick(settings.joystick.Devices.Keys.ToList().IndexOf(settings.JoystickGUID)))
            {
                settings.joystick.Start(settings, ct);
            }
        }


        private RelayCommand setCommand;
        public ICommand SetCommand => setCommand ??= new RelayCommand(Set);

        private void Set(object commandParameter)
        {
            cube.Width = cube.WidthBind;
            cube.Height = cube.HeightBind;
            cube.Length = cube.LengthBind;


            camera.position.X = camera.positionBinding.X;
            camera.position.Y = camera.positionBinding.Y;
            camera.position.Z = camera.positionBinding.Z;


            settings.cube = this.cube;

            Save(null);
        }

        private IEnumerable<string> comPortValues;

        public IEnumerable<string> ComPortValues
        {
            get { return comPortValues; }
            set
            {
                comPortValues = value;

                OnPropertyChanged("ComPortValues");
            }
        }
        private string comPortValue;
        public string ComPortValue
        {
            get => comPortValue;
            set
            {
                comPortValue = value;
                settings.ComPort = comPortValue;

                OnPropertyChanged("ComPortValue");
            }
        }

        private IEnumerable<string> baudRateValues;

        public IEnumerable<string> BaudRateValues
        {
            get { return baudRateValues; }
            set
            {
                baudRateValues = value;

                OnPropertyChanged("BaudRateValues");
            }
        }
        private string baudRateValue;
        public string BaudRateValue
        {
            get => baudRateValue;
            set
            {
                baudRateValue = value;
                settings.BaudRate = Convert.ToInt32(baudRateValue);

                OnPropertyChanged("BaudRateValue");
            }
        }

        private RelayCommand serialRefreshBtn;
        public RelayCommand SerialRefreshBtn
        {
            get
            {
                return serialRefreshBtn ??
                    (serialRefreshBtn = new RelayCommand(obj =>
                    {
                        ComPortValues = settings.serial.Ports;
                    }));
            }
        }


        private IEnumerable<string> joystickDevices;

        public IEnumerable<string> JoystickDevices
        {
            get { return joystickDevices; }
            set
            {
                joystickDevices = value;

                OnPropertyChanged("JoystickDevices");
            }
        }
        private string joystickDevice;
        public string JoystickDevice
        {
            get => joystickDevice;
            set
            {
                joystickDevice = value;

                if (joystickDevice != null)
                {
                    settings.JoystickGUID = joystickDeviceList.Where(device => device.Value == joystickDevice).First().Key;

                    OnPropertyChanged("JoystickDevice");
                }
            }
        }

        private RelayCommand joystickRefreshBtn;
        public RelayCommand JoystickRefreshBtn
        {
            get
            {
                return joystickRefreshBtn ??
                    (joystickRefreshBtn = new RelayCommand(obj =>
                    {
                        joystickDeviceList = settings.joystick.Devices;
                        JoystickDevices = joystickDeviceList.Values;
                    }));
            }
        }


        public double SpeedSliderValue
        {
            set
            {
                settings.CameraSpeed = value;

                OnPropertyChanged("SpeedSliderValue");
            }
            get
            {
                if (settings != null)
                    return settings.CameraSpeed;

                return 0;
            }
        }


        private RelayCommand _A_NullCommand;
        public RelayCommand A_NullCommand
        {
            get
            {
                return _A_NullCommand ??
                  (_A_NullCommand = new RelayCommand(obj =>
                  {
                      camera.position.X = 0;
                      camera.position.Y = 0;
                      camera.position.Z = 0;
                  }));
            }
        }

        private RelayCommand _B_NullCommand;
        public RelayCommand B_NullCommand
        {
            get
            {
                return _B_NullCommand ??
                  (_B_NullCommand = new RelayCommand(obj =>
                  {
                      camera.position.X = cube.Length - camera.length;
                      camera.position.Y = 0;
                      camera.position.Z = 0;
                  }));
            }
        }

        private RelayCommand _C_NullCommand;
        public RelayCommand C_NullCommand
        {
            get
            {
                return _C_NullCommand ??
                  (_C_NullCommand = new RelayCommand(obj =>
                  {
                      camera.position.X = 0;
                      camera.position.Y = cube.Width - camera.width;
                      camera.position.Z = 0;
                  }));
            }
        }

        private RelayCommand _D_NullCommand;
        public RelayCommand D_NullCommand
        {
            get
            {
                return _D_NullCommand ??
                  (_D_NullCommand = new RelayCommand(obj =>
                  {
                      camera.position.X = cube.Length - camera.length;
                      camera.position.Y = cube.Width - camera.width;
                      camera.position.Z = 0;
                  }));
            }
        }



        private RelayCommand saveCommand;
        public ICommand SaveCommand => saveCommand ??= new RelayCommand(Save);

        private async void Save(object commandParameter)
        {
            if (File.Exists("settings.json"))
                File.Delete("settings.json");

            using (FileStream fs = new FileStream("settings.json", FileMode.Create))
            {
                await JsonSerializer.SerializeAsync<Settings>(fs, settings);
            }

            StartProgram();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
