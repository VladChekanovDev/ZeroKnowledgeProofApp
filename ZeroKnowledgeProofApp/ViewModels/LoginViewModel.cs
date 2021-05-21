﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using ZeroKnowledgeProofApp.Other;
using ZeroKnowledgeProofApp.Views;

namespace ZeroKnowledgeProofApp.ViewModels
{
    class LoginViewModel: BaseVM
    {
        #region Поля

        string login;
        string n;
        string v;
        DelegateCommand openRegisterWindow;
        DelegateCommand closeWindow;
        DelegateCommand minimizeWindow;

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

        public string N
        {
            get => n;
            set
            {
                n = value;
                OnPropertyChanged(nameof(IsLoginActive));
            }
        }

        public string V
        {
            get => v;
            set
            {
                v = value;
                OnPropertyChanged(nameof(IsLoginActive));
            }
        }

        public bool IsLoginActive => !string.IsNullOrWhiteSpace(login)
            && !string.IsNullOrWhiteSpace(n)
            && !string.IsNullOrWhiteSpace(v);

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

        #endregion
    }
}
