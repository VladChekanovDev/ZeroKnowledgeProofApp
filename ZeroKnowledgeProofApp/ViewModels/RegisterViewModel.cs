using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using ZeroKnowledgeProofApp.Dialogs.DialogsViewModels;
using ZeroKnowledgeProofApp.Dialogs.DialogsViews;
using ZeroKnowledgeProofApp.Entities;
using ZeroKnowledgeProofApp.Models;
using ZeroKnowledgeProofApp.Other;
using ZeroKnowledgeProofApp.Views;

namespace ZeroKnowledgeProofApp.ViewModels
{
    class RegisterViewModel: BaseVM
    {
        #region Поля

        string lastName;
        string firstName;
        string login;
        DelegateCommand closeWindow;
        DelegateCommand minimizeWindow;
        DelegateCommand openLoginWindow;
        DelegateCommand openGenerateKeysDialog;

        #endregion

        #region Свойства

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(IsRegisterActive));
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(IsRegisterActive));
            }
        }

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(IsRegisterActive));
            }
        }

        public bool IsRegisterActive => !string.IsNullOrWhiteSpace(lastName)
            && !string.IsNullOrWhiteSpace(firstName)
            && !string.IsNullOrWhiteSpace(login);

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
                return minimizeWindow ??= new DelegateCommand((obj) =>
                {
                    Application.Current.MainWindow.WindowState = WindowState.Minimized;
                });
            }
        }

        public DelegateCommand OpenLoginWindow
        {
            get
            {
                return openLoginWindow ??= new DelegateCommand((obj) =>
                {
                    var loginWindow = new LoginView();
                    loginWindow.Show();
                    Application.Current.MainWindow.Close();
                    Application.Current.MainWindow = loginWindow;
                });
            }
        }

        public DelegateCommand OpenGenerateKeysDialog
        {
            get
            {
                return openGenerateKeysDialog ??= new DelegateCommand((obj) =>
                {
                    if (new UserModel().IsUserExists(login))
                    {
                        var error = new ErrorView("Пользователь с таким логином уже существует");
                        error.ShowDialog();
                    }
                    else
                    {
                        var dialog = new GenerateKeysView();
                        if (dialog.ShowDialog() == true)
                        {
                            var vm = (GenerateKeysViewModel)dialog.DataContext;
                            var newuser = new User(firstName, lastName, login, vm.N.ToString(), vm.V0.ToString(), vm.S.ToString());
                            new UserModel().Add(newuser);
                        }
                    }
                });
            }
        }

        #endregion
    }
}
