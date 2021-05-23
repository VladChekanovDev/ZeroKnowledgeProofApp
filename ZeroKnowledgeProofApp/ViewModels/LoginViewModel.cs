using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows;
using ZeroKnowledgeProofApp.Dialogs.DialogsViews;
using ZeroKnowledgeProofApp.Models;
using ZeroKnowledgeProofApp.Other;
using ZeroKnowledgeProofApp.Views;

namespace ZeroKnowledgeProofApp.ViewModels
{
    class LoginViewModel: BaseVM
    {
        #region Поля

        string login;
        string s;
        DelegateCommand openRegisterWindow;
        DelegateCommand closeWindow;
        DelegateCommand minimizeWindow;
        DelegateCommand authenticateUser;

        #endregion

        #region Свойства

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(IsLoginActive));
            }
        }

        public string S
        {
            get => s;
            set
            {
                s = value;
                OnPropertyChanged(nameof(IsLoginActive));
            }
        }

        public bool IsLoginActive => !string.IsNullOrWhiteSpace(login)
            && !string.IsNullOrWhiteSpace(s);

        #endregion

        #region Команды

        public DelegateCommand CloseWindow
        {
            get
            {
                return closeWindow ??= new DelegateCommand((obj) =>
                {
                    Application.Current.Shutdown();
                });
            }
        }

        public DelegateCommand MinimizeWindow
        {
            get
            {
                return minimizeWindow ??= new DelegateCommand((arg) =>
                {
                    Application.Current.MainWindow.WindowState = WindowState.Minimized;
                });
            }
        }

        public DelegateCommand OpenRegisterWindow
        {
            get
            {
                return openRegisterWindow ??= new DelegateCommand((obj) =>
                {
                    var registerWindow = new RegisterView();
                    registerWindow.Show();
                    Application.Current.MainWindow.Close();
                    Application.Current.MainWindow = registerWindow;
                });
            }
        }

        public DelegateCommand AuthenticateUser
        {
            get
            {
                return authenticateUser ??= new DelegateCommand((obj) =>
                {
                    var userModel = new UserModel();
                    if (!(userModel.IsUserExists(login)))
                    {
                        new ErrorView("Пользователь не найден. Попробуйте другой логин").ShowDialog();
                    }
                    else
                    {
                        CurrentUserInfo.CurrentUser = userModel.FindUser(login);
                        if (!BigInteger.TryParse(s,out CurrentUserInfo.S))
                        {
                            new ErrorView("Недопустимое значение ключа, попробуйте снова").ShowDialog();
                        }
                        else
                        {
                            var authenticateView = new AuthenticationView();
                            CurrentUserInfo.IsAuthenticated = true;
                            if (authenticateView.ShowDialog() == true)
                            {
                                if (CurrentUserInfo.IsAuthenticated)
                                {
                                    new ErrorView("Пользователь прошел проверку").ShowDialog();
                                }
                                else
                                    new ErrorView("Пользователь не авторизован").ShowDialog();
                            }
                        }
                    }
                });
            }
        }

        #endregion
    }
}
