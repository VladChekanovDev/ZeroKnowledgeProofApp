using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZeroKnowledgeProofApp.Dialogs.DialogsViews
{
    /// <summary>
    /// Логика взаимодействия для ErrorView.xaml
    /// </summary>
    public partial class ErrorView : Window
    {
        public ErrorView(string Error)
        {
            InitializeComponent();
            ErrorTextBlock.Text = Error;
        }
    }
}
