﻿using System;
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
using MySql.Data.MySqlClient;
using WpfDBMusicLabel.ViewModel;

namespace WpfDBMusicLabel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MySqlConnection cnn;
        //private VModel vm;

        public MainWindow()
        {
            InitializeComponent();
            DbConnection();
            /*vm = new VModel();
            ActionsListBox.ItemsSource = vm.GetActionsList();*/
        }

        private void DbConnection()
        {
            string connectionString = "SERVER=localhost; PORT=3306; DATABASE=musiclabeldb; UID=root; PASSWORD=sinio";
            try
            {
                cnn = new MySqlConnection(connectionString);
                cnn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cnn.Close();
        }
    }
}
