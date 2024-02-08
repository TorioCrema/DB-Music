using System;
using System.Collections.Generic;
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
using WpfDBMusicLabel.Model;

namespace WpfDBMusicLabel.ViewGrids.Templates.InsertTemplates
{
    /// <summary>
    /// Interaction logic for TrackInsertGrid.xaml
    /// </summary>
    public partial class TrackInsertGrid : UserControl
    {
        private readonly InputChecker _checker = InputCheckerGenerator.IntChecker();

        public TrackInsertGrid()
        {
            InitializeComponent();
        }

        private void IntegerInputCheck(object sender, TextCompositionEventArgs e) => _checker.Check(sender, e);
    }
}
