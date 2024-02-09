using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using WpfDBMusicLabel.musiclabeldb;

namespace WpfDBMusicLabel.ViewModel
{
    partial class TracciaVM : AbstractVM
    {
        static readonly string NEW_NAME = "Inserire Nome";
        static readonly string NEW_TEXT = "Inserire Testo";

        [ObservableProperty]
        private ObservableCollection<Traccia>? tracce;

        [ObservableProperty]
        private Traccia? currentSelectedTrack = null;

        [ObservableProperty]
        private List<Firmatario>? firmatari;

        [ObservableProperty]
        private List<ProgettoMusicale>? progetti;

        [ObservableProperty]
        private ProgettoMusicale? selectedFeature = null;

        [ObservableProperty]
        private ObservableCollection<ProgettoMusicale> features = new();

        [RelayCommand]
        private void AddFeature()
        {
            if (SelectedFeature != null 
                && Features.All(x => x.IdProgetto != SelectedFeature.IdProgetto))
            {
                Features.Add(SelectedFeature);
            }
        }

        [ObservableProperty]
        private Traccia newTrack = new()
        {
            Nome = NEW_NAME,
            Durata = 0,
            Testo = NEW_TEXT
        };

        [ObservableProperty]
        private DateTime dataPubblicazione = DateTime.Now;

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
            _dbContext.ProgettiMusicali.Load();
            Progetti = _dbContext.ProgettiMusicali.Local.ToList();
        }

        override public bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Vedi dati":
                    if (CurrentSelectedTrack != null)
                    {
                        _dbContext.Entry(CurrentSelectedTrack).Collection(t => t.IdProgettos).Load();
                        _dbContext.Entry(CurrentSelectedTrack).Collection(t => t.IdFirmatarios).Load();
                        Firmatari = CurrentSelectedTrack.IdFirmatarios.ToList();
                        Progetti = CurrentSelectedTrack.IdProgettos.ToList();
                    }
                    break;
                case "Inserisci":
                    if (checkTrack())
                    {
                        NewTrack.DataPubblicazione = DataPubblicazione;
                        NewTrack.IdProgettos = Features.ToList();
                        _dbContext.Tracce.Local.Add(NewTrack);
                        SaveChanges();
                        ResetInsert();
                        ShowSuccess();
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
            return true;
        }

        protected override void ResetInsert()
        {
            NewTrack = new();
            SelectedFeature = null;
            Features = new();
            DataPubblicazione = new();
        }

        private bool checkTrack()
        {
            if (NewTrack.IdProgettoNavigation == null
                || NewTrack.Nome == NEW_NAME
                || NewTrack.Durata == 0)
            {
                return false;
            }
            return true;
        }
    }
}
