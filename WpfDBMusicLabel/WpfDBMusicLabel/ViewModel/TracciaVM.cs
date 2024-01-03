using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfDBMusicLabel.musiclabeldb;

namespace WpfDBMusicLabel.ViewModel
{
    partial class TracciaVM : AbstractVM
    {

        [ObservableProperty]
        private ObservableCollection<Traccia>? tracce;

        [ObservableProperty]
        private Traccia? currentSelectedTrack = null;

        [ObservableProperty]
        private List<Firmatario>? firmatari;

        [ObservableProperty]
        private List<ProgettoMusicale>? progetti;

        [ObservableProperty]
        private List<string> subActionsList = new()
        {
            "Vedi dati"
        };

        public TracciaVM()
        {
            _dbContext = new();
            _dbContext.Tracce.Load();
            Tracce = _dbContext.Tracce.Local.ToObservableCollection();
        }

        override public bool ExecuteSubAction()
        {
            if (CurrentSelectedTrack != null)
            {
                switch (CurrentSubAction)
                {
                    case "Vedi dati":
                        _dbContext.Entry(CurrentSelectedTrack).Collection(t => t.IdProgettos).Load();
                        _dbContext.Entry(CurrentSelectedTrack).Collection(t => t.IdFirmatarios).Load();
                        Firmatari = CurrentSelectedTrack.IdFirmatarios.ToList();
                        Progetti = CurrentSelectedTrack.IdProgettos.ToList();
                        break;
                    default:
                        return false;
                }
                return true;
            }
            return false;
        }

        protected override void ResetInsert()
        {
            // throw new NotImplementedException();
        }
    }
}
