using Component.Tools;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OExam.App.ViewModels;
using OExam.Client.Service;
using OExam.Client.Utils;
using System.Windows;

namespace OExam.Client.ViewModel
{
    public class LoginViewModel: ViewModelBase
    {
        public LoginViewModel()
        {
            LoginOnCommand = new RelayCommand<string>(CheckUser);

        }
        private void CheckUser(string pws)
        {
            LUser.LoginPassword = pws;
            UserDataService uds = new UserDataService();
            uds.CheckAndGetUser(LUser, (oresult) =>
            {
                if(oresult.ResultType == EOperationType.Success)
                {
                   
                    if(oresult.AppentData != null)
                    {
                        //UserData udata = new UserData();

                        AuthToken.Token = oresult.Message;

                        MainWindow mwindow = new MainWindow(oresult.AppentData);
                        mwindow.Show();
                        mwindow.Closed += Mwindow_Closed;
                        Showing = Visibility.Hidden;

                    }
                    else
                    {
                        ModernDialog.ShowMessage("用户为空", "提示", MessageBoxButton.OK);
                    }
                }
                else
                {
                    ModernDialog.ShowMessage(oresult.Message, "提示", MessageBoxButton.OK);
                }
                
            });
        }

        private void Mwindow_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }

        public RelayCommand<string> LoginOnCommand { get; set;}
        private LoginUser _luser = new LoginUser();
        public LoginUser LUser
        {
            get
            {
                return _luser;
            }
            set
            {
                if (_luser == value)
                    return;
                _luser = value;
                RaisePropertyChanged(() => LUser);
            }
        }

        private Visibility _showing = Visibility.Visible;
        public Visibility Showing
        {
            get
            {
                return _showing;
            }
            set
            {
                if (_showing == value)
                    return;
                _showing = value;
                RaisePropertyChanged(() => Showing);
            }
        }
    }
}
