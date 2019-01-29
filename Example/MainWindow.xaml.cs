using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowPositionSaver;

namespace Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WPS.WPS_Window_Constructor(this);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //WPS.WPS_Window_Constructor - can be here, but better place it in ctor
            //WPS.WPS_Window_Constructor - to by tady mohlo být, ale lepší je to v konstruktoru
            WPS.WPS_Window_Loaded(this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WPS.WPS_Window_Closing(this);
        }
    }
}
