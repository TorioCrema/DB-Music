using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using MySql.Data.MySqlClient;
using WpfDBMusicLabel.Model;
using WpfDBMusicLabel.ViewModel;

namespace WpfDBMusicLabel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void ActionsListCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VModel.SetCurrentAction(((ComboBox)e.Source).SelectedItem.ToString());
        }

        private void ActionsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VModel.CurrentSubAction = ((ComboBox)e.Source).SelectedItem.ToString();
        }
    }
}
