using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using ZeroKnowledgeProofApp.Other;

namespace ZeroKnowledgeProofApp.ViewModels
{
    class LoginViewModel: BaseVM
    {
        #region Поля

        DelegateCommand closeWindow;
        DelegateCommand minimizeWindow;

        #endregion

        #region Свойства



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

        #endregion
    }
}
