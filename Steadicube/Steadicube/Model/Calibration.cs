using Steadicube.Classes;
using Steadicube.ViewModel;

namespace Steadicube.Model
{
    public class Calibration
    {
        private RelayCommand a_Plus;
        public RelayCommand A_Plus
        {
            get
            {
                return a_Plus ??
                  (a_Plus = new RelayCommand(obj =>
                  {
                      ConfigViewModel.configViewModel.settings.serial.Send("A+");
                  }));
            }
        }
        private RelayCommand a_Minus;
        public RelayCommand A_Minus
        {
            get
            {
                return a_Minus ??
                  (a_Minus = new RelayCommand(obj =>
                  {
                      ConfigViewModel.configViewModel.settings.serial.Send("A-");
                  }));
            }
        }

        private RelayCommand b_Plus;
        public RelayCommand B_Plus
        {
            get
            {
                return b_Plus ??
                  (b_Plus = new RelayCommand(obj =>
                  {
                      ConfigViewModel.configViewModel.settings.serial.Send("B+");
                  }));
            }
        }
        private RelayCommand b_Minus;
        public RelayCommand B_Minus
        {
            get
            {
                return b_Minus ??
                  (b_Minus = new RelayCommand(obj =>
                  {
                      ConfigViewModel.configViewModel.settings.serial.Send("B-");
                  }));
            }
        }

        private RelayCommand c_Plus;
        public RelayCommand C_Plus
        {
            get
            {
                return c_Plus ??
                  (c_Plus = new RelayCommand(obj =>
                  {
                      ConfigViewModel.configViewModel.settings.serial.Send("C+");
                  }));
            }
        }
        private RelayCommand c_Minus;
        public RelayCommand C_Minus
        {
            get
            {
                return c_Minus ??
                  (c_Minus = new RelayCommand(obj =>
                  {
                      ConfigViewModel.configViewModel.settings.serial.Send("C-");
                  }));
            }
        }

        private RelayCommand d_Plus;
        public RelayCommand D_Plus
        {
            get
            {
                return d_Plus ??
                  (d_Plus = new RelayCommand(obj =>
                  {
                      ConfigViewModel.configViewModel.settings.serial.Send("D+");
                  }));
            }
        }
        private RelayCommand d_Minus;
        public RelayCommand D_Minus
        {
            get
            {
                return d_Minus ??
                  (d_Minus = new RelayCommand(obj =>
                  {
                      ConfigViewModel.configViewModel.settings.serial.Send("D-");
                  }));
            }
        }
    }
}
