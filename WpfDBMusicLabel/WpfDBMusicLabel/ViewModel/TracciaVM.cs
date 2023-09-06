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
        private ObservableCollection<Firmatario>? firmatari;

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
                //Per avere solo i firmatari, trovare un modo per caricare tutti i dati
                _dbContext.Firmatari.Where(firmatario => firmatario.IdTraccia.Any(traccia => traccia.IdTraccia == CurrentSelectedTrack.IdTraccia)).Load();
                Firmatari = _dbContext.Firmatari.Local.ToObservableCollection();
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void ResetInsert()
        {
            throw new NotImplementedException();
        }
    }
}
