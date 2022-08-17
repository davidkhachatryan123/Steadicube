using Steadicube.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Steadicube.ViewModel
{
    public class TopViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; }

        public TopViewModel(string title)
        {
            this.Title = title;
        }


        private RelayCommand closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                  (closeCommand = new RelayCommand(obj =>
                  {
                      Application.Current.Shutdown();
                  }));
            }
        }

        private RelayCommand minimizeCommand;
        public RelayCommand MinimizeCommand
        {
            get
            {
                return minimizeCommand ??
                  (minimizeCommand = new RelayCommand(obj =>
                  {
                      Window w = obj as Window;
                      w.WindowState = WindowState.Minimized;
                  }));
            }
        }

        private RelayCommand maximizeCommand;
        public RelayCommand MaximizeCommand
        {
            get
            {
                return maximizeCommand ??
                  (maximizeCommand = new RelayCommand(obj =>
                  {
                      Window w = obj as Window;

                      if (w.WindowState == WindowState.Maximized)
                          w.WindowState = WindowState.Normal;
                      else
                          w.WindowState = WindowState.Maximized;
                  }));
            }
        }

        private RelayCommand topDoubleClickCommand;
        public RelayCommand TopDoubleClickCommand
        {
            get
            {
                return topDoubleClickCommand ??
                  (topDoubleClickCommand = new RelayCommand(obj =>
                  {
                      Window w = obj as Window;

                      if (w.WindowState == WindowState.Maximized)
                          w.WindowState = WindowState.Normal;
                      else
                          w.WindowState = WindowState.Maximized;
                  }));
            }
        }

        private RelayCommand leftClickCommand;
        public RelayCommand LeftClickCommand
        {
            get
            {
                return leftClickCommand ??
                  (leftClickCommand = new RelayCommand(obj =>
                  {
                      Window w = obj as Window;
                      w.DragMove();
                  }));
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
