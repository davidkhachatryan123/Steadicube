using Steadicube.Classes;
using Steadicube.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Text.Json;
using System.IO;
using Steadicube.View;

using DeviceList = System.Collections.Generic.Dictionary<System.Guid, string>;
using Steadicube.Interfaces;

namespace Steadicube.ViewModel
{
    public class ConfigViewModel : INotifyPropertyChanged
    {
        private Settings settings;
        private JoyStick joystick;
        private DeviceList joystickDeviceList;
        private Serial serial;
        private Cube cube;
        private Camera camera;

        private RelayCommand loadedCommand;
        public RelayCommand LoadedCommand
        {
            get
            {
                return loadedCommand ??
                  (loadedCommand = new RelayCommand(obj =>
                  {
                      settings = new Settings("settings.json");

                      joystick = new JoyStick();
                      joystickDeviceList = joystick.Devices;
                      JoystickDevices = joystickDeviceList.Values;

                      if (settings.Joystick != Guid.Empty)
                          try
                          {
                              JoystickDevice = joystickDeviceList.Where(device => device.Key == settings.Joystick).First().Value;
                          }
                          catch
                          {
                              settings.Joystick = Guid.Empty;
                          }

                      SpeedSliderValue = settings.CameraSpeed;


                      serial = new Serial();
                      ComPortValues = serial.Ports;
                      BaudRateValues = serial.Bauds;

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
                      cube!.Width = 100;
                      cube!.Height = 100;
                      cube!.Length = 100;

                      camera = (Camera)(obj as Config).Camera.DataContext;
                      camera!.position.X = cube.Length / 2;
                      camera!.position.Y = cube.Width / 2;
                      camera!.position.Z = cube.Height;

                      StartProgram();
                  }));
            }
        }

        private void StartProgram()
        {
            if (settings.Joystick != Guid.Empty)
            {
                joystick.SetJoystick(joystick.Devices.Keys.ToList().IndexOf(settings.Joystick));

                joystick.Start();
            }
        }


        private RelayCommand setCommand;
        public ICommand SetCommand => setCommand ??= new RelayCommand(Set);

        private void Set(object commandParameter)
        {

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
                        ComPortValues = serial.Ports;
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
                    settings.Joystick = joystickDeviceList.Where(device => device.Value == joystickDevice).First().Key;

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
                        joystickDeviceList = joystick.Devices;
                        JoystickDevices = joystickDeviceList.Values;
                    }));
            }
        }


        public int SpeedSliderValue
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


        private RelayCommand saveCommand;
        public ICommand SaveCommand => saveCommand ??= new RelayCommand(Save);

        private async void Save(object commandParameter)
        {
            using (FileStream fs = new FileStream("settings.json", FileMode.OpenOrCreate))
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
