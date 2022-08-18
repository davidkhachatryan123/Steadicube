﻿using Steadicube.Classes;
using Steadicube.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Text.Json;
using System.IO;

using DeviceList = System.Collections.Generic.Dictionary<System.Guid, string>;

namespace Steadicube.ViewModel
{
    public class ConfigViewModel : INotifyPropertyChanged
    {
        private Settings settings;
        private JoyStick joystick;
        private DeviceList deviceList;


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
                      deviceList = joystick.Devices;
                      JoystickDevices = deviceList.Values;

                      if (settings.Joystick != Guid.Empty)
                          try
                          {
                              JoystickDevice = deviceList.Where(device => device.Key == settings.Joystick).First().Value;
                          }
                          catch
                          {
                              settings.Joystick = Guid.Empty;
                          }

                      SpeedSliderValue = settings.CameraSpeed;
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
                    settings.Joystick = settings.Joystick = deviceList.Where(device => device.Value == joystickDevice).First().Key;

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
                        deviceList = joystick.Devices;
                        JoystickDevices = deviceList.Values;
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
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
