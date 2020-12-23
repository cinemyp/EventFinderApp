using DAL.Repositories;
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
        ILoginWindow window;
        IDbCrud tm;
        public string LoginContent { get; set; }
        public string PasswordContent
        {
            get { return window.GetPassword(); }
        }

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
                            window.Close(true);
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
                            window.Close(); //мы просто закрываем окно, и говорим, что не залогинились
                        else
                        {

                        }
                    }
                ));
            }
        }

        public LoginViewModel(ILoginWindow window, IDbCrud tm)
        {
            this.tm = tm;
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
