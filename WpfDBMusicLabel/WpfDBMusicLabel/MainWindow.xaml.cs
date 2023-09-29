using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private readonly InputChecker _checker = InputCheckerGenerator.IntChecker();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void IntegerInputCheck(object sender, TextCompositionEventArgs e) => _checker.Check(sender, e);
    }
}
