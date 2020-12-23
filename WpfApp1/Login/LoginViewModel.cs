using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.ViewModels.Interfaces;

namespace WpfApp1
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        ICloseable window;
        TransactionManager tm;
        public string LoginContent { get; set; }
        public string PasswordContent { get; set; }

        private RelayCommand signIn;
        public RelayCommand SignIn
        {
            get
            {
                return signIn ??
                    (signIn = new RelayCommand(obj =>
                    {
                        bool result = tm.SignIn(new UserModel(LoginContent, PasswordContent));

                        if (result)
                            window.Close();
                        else
                        {
                            //TODO: создать сообщение о том, что успешно зашли
                        }
                    }
                ));
            }
        }
        private RelayCommand signOn;
        public RelayCommand SignOn
        {
            get
            {
                return signOn ??
                    (signOn = new RelayCommand(obj =>
                    {
                        bool result = tm.SignOn(new UserModel(LoginContent, PasswordContent));

                        if (result)
                            window.Close();
                        else
                        {

                        }
                    }
                ));
            }
        }

        public LoginViewModel(ICloseable window)
        {
            tm = new TransactionManager();
            this.window = window;
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        #endregion
    }
}
