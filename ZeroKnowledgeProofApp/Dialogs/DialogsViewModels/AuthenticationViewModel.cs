using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ZeroKnowledgeProofApp.Dialogs.DialogsViews;
using ZeroKnowledgeProofApp.Other;

namespace ZeroKnowledgeProofApp.Dialogs.DialogsViewModels
{
    class AuthenticationViewModel: BaseVM
    {
        #region Поля

        int iteration = 1;
        UserControl iterationControl;
        DelegateCommand generateNewIteration;

        #endregion

        #region Конструктор

        public AuthenticationViewModel()
        {
            iterationControl = new IterationControlView();
        }

        #endregion

        #region Свойства

        public int Iteration
        {
            get => iteration;
            set
            {
                iteration = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }
        
        public string ButtonText
        {
            get => $"Следующая итерация ({iteration}/7)";
        }

        public UserControl IterationControl
        {
            get => iterationControl;
            set
            {
                iterationControl = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Команды

        public DelegateCommand GenerateNewIteration
        {
            get
            {
                return generateNewIteration ??= new DelegateCommand((arg) =>
                {
                    if (iteration != 7)
                    {
                        Iteration++;
                        IterationControl = new IterationControlView();
                    }
                    else
                    {
                        var window = (Window)arg;
                        window.DialogResult = true;
                    }
                });
            }
        }

        #endregion
    }
}
