using SharpDX.DirectInput;
using Steadicube.Classes;
using Steadicube.Enums;
using Steadicube.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using DeviceList = System.Collections.Generic.Dictionary<System.Guid, string>;

namespace Steadicube.Model
{
    public class JoyStick : INotifyPropertyChanged
    {
        private DeviceList devices;
        private Joystick joystick;

        public DeviceList Devices
        {
            get
            {
                DirectInput directInput = new DirectInput();

                devices = new DeviceList();

                foreach (var deviceInstance in directInput.GetDevices())
                    devices.Add(deviceInstance.InstanceGuid, deviceInstance.ProductName);

                return devices;
            }
            private set
            {
                OnPropertyChanged("Devices");
            }
        }

        public void SetJoystick(int JoystickIndex)
        {
            DeleteJoystick();

            DirectInput directInput = new DirectInput();
            joystick = new Joystick(directInput, devices!.ElementAt(JoystickIndex).Key);
            joystick.Properties.BufferSize = 128;
            joystick.Acquire();
        }

        public void DeleteJoystick()
        {
            if (joystick != null)
                joystick.Dispose();
        }

        private void Update(JoystickMovement joystickMovement)
        {
            try
            {
                joystick!.Poll();
                var datas = joystick!.GetBufferedData();

                JoystickCapture.Capture(JoystickOffset.Buttons99, -1, joystickMovement);

                if (datas != null)
                    foreach (var state in datas)
                        JoystickCapture.Capture(state.Offset, state.Value, joystickMovement);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void Start(Settings settings)
        {
            JoystickMovement joystickMovement = new JoystickMovement();

            Task task = Task.Run(() =>
            {
                M_Mode m_Mode = M_Mode.M1;
                S_Mode s_Mode = S_Mode.S1;

                while (true)
                {
                    Update(joystickMovement);

                    if (ApplicationInit(joystickMovement))
                    {
                        ChangeMode(joystickMovement.Right_Btn_RIGHT, ref m_Mode, (m_Mode) => { ModeViewModel.modeViewModel.m_Mode = m_Mode; });
                        ChangeS_Mode(joystickMovement.Left_Stick_BTN, ref s_Mode, (s_Mode) => { ModeViewModel.modeViewModel.s_Mode = s_Mode; });

                        ConfigViewModel.configViewModel.camera.MoveCamera(
                            joystickMovement,
                            ConfigViewModel.configViewModel.cube,
                            s_Mode,
                            ConfigViewModel.configViewModel.settings,
                            (vector4D) =>
                            {
                                settings.serial.SendSerial(vector4D);
                            });
                    }
                }
            });
        }


        private bool isInit = false;
        private bool ApplicationInit(JoystickMovement joystickMovement)
        {
            if (joystickMovement.Right_Stick_BTN)
                isInit = true;

            return isInit;
        }

        private DateTime end;
        private bool pressed = false;
        private void ChangeMode(bool btn, ref M_Mode mode, Action<M_Mode> visualize)
        {
            if (!pressed && btn)
            {
                end = DateTime.Now + new TimeSpan(0, 0, 0, 0, 400);

                pressed = true;
                mode = M_Mode.M1;

                return;
            }

            if (pressed)
            {
                if (DateTime.Now < end)
                {
                    if (btn)
                    {
                        if (mode != M_Mode.M4)
                        {
                            mode++;
                            end = DateTime.Now + new TimeSpan(0, 0, 0, 0, 400);
                        }
                    }
                }
                else
                    pressed = false;
            }

            visualize.Invoke(mode);
        }

        private void ChangeS_Mode(bool btn, ref S_Mode mode, Action<S_Mode> visualize)
        {
            if (btn)
                if (mode == S_Mode.S1)
                    mode = S_Mode.S2;
                else
                    mode = S_Mode.S1;

            visualize.Invoke(mode);
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
