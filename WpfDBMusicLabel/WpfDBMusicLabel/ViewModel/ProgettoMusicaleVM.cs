using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfDBMusicLabel.musiclabeldb;

namespace WpfDBMusicLabel.ViewModel
{
    partial class ProgettoMusicaleVM : AbstractVM
    {
        [ObservableProperty]
        private ObservableCollection<ProgettoMusicale> progettiMusicali;

        [ObservableProperty]
        private ProgettoMusicale? currentSelectedProject = null;

        [ObservableProperty]
        private ProgettoMusicale newProject = new()
        {
            DataFormazione = DateTime.Now,
            DataScioglimento = null
        };

        [ObservableProperty]
        private Firmatario? selectedFirmatario = null;

        [ObservableProperty]
        private List<Firmatario> firmatarioList = new();

        [ObservableProperty]
        private Firmatario? toRemove = null;

        [ObservableProperty]
        private List<Album> albums = new();

        [ObservableProperty]
        private List<Traccia> tracce = new();

        [ObservableProperty]
        private List<Firmatario> firmatari = new();

        [ObservableProperty]
        private List<Concerto> concerti = new();

        [ObservableProperty]
        private List<Traccia> features = new();

        [ObservableProperty]
        private List<Merchandising> merch = new();

        [ObservableProperty]
        private uint totBiglietti = 0;

        [ObservableProperty]
        private List<string> subActionsList = new()
        {
            "Vedi album",
            "Vedi tracce",
            "Vedi firmatari",
            "Vedi concerti",
            "Vedi features",
            "Vedi merch",
            "Totale biglietti venduti"
        };

        [ObservableProperty]
        private List<string> typeList = new()
        {
            "Band",
            "Solista"
        };

        [ObservableProperty]
        private string? selectedType = null;

        [ObservableProperty]
        private Dictionary<string, Visibility> resultVisibility = new()
        {
            { "Albums", Visibility.Collapsed },
            { "Tracks", Visibility.Collapsed },
            { "Firmatari", Visibility.Collapsed },
            { "Concerts", Visibility.Collapsed },
            { "Features", Visibility.Collapsed },
            { "Merch", Visibility.Collapsed },
            { "Biglietti", Visibility.Collapsed }
        };

        public ProgettoMusicaleVM()
        {
            _dbContext.ProgettiMusicali.Load();
            _dbContext.Firmatari.Load();
            _dbContext.Luoghi.Load();
            Firmatari = _dbContext.Firmatari.Local.ToList();
            ProgettiMusicali = _dbContext.ProgettiMusicali.Local.ToObservableCollection();
        }

        public override bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Vedi album":
                    if (CurrentSelectedProject != null) { 
                        _dbContext.Entry(CurrentSelectedProject).Collection(p => p.Albums).Load();
                        Albums = CurrentSelectedProject.Albums.ToList();
                        UpdateResults("Albums");
                        return true;
                    }
                    return false;
                case "Vedi tracce":
                    if (CurrentSelectedProject != null)
                    {
                        _dbContext.Entry(CurrentSelectedProject).Collection(p => p.Traccia).Load();
                        Tracce = CurrentSelectedProject.Traccia.ToList();
                        UpdateResults("Tracks");
                        return true;
                    }
                    return false;
                case "Vedi firmatari":
                    if (CurrentSelectedProject != null)
                    {
                        _dbContext.Entry(CurrentSelectedProject).Collection(p => p.IdFirmatarios).Load();
                        Firmatari = CurrentSelectedProject.IdFirmatarios.ToList();
                        UpdateResults("Firmatari");
                        return true;
                    }
                    return false;
                case "Vedi concerti":
                    if (CurrentSelectedProject != null)
                    {
                        _dbContext.Entry(CurrentSelectedProject).Collection(p => p.IdConcertos).Load();
                        Concerti = CurrentSelectedProject.IdConcertos.ToList();
                        UpdateResults("Concerts");
                        return true;
                    }
                    return false;
                case "Vedi features":
                    if (CurrentSelectedProject != null)
                    {
                        _dbContext.Entry(CurrentSelectedProject).Collection(p => p.IdTraccia).Load();
                        Features = CurrentSelectedProject.IdTraccia.ToList();
                        Features.ForEach(f => _dbContext.Entry(f).Reference(x => x.IdProgettoNavigation).Load());
                        UpdateResults("Features");
                        return true;
                    }
                    return false;
                case "Vedi merch":
                    if (CurrentSelectedProject != null)
                    {
                        _dbContext.Entry(CurrentSelectedProject).Collection(p => p.Merchandisings).Load();
                        Merch = CurrentSelectedProject.Merchandisings.ToList();
                        UpdateResults("Merch");
                        return true;
                    }
                    return false;
                case "Totale biglietti venduti":
                    if (CurrentSelectedProject != null)
                    {
                        TotBiglietti = 0;
                        _dbContext.Entry(CurrentSelectedProject).Collection(p => p.IdConcertos).Load();
                        CurrentSelectedProject.IdConcertos.ToList()
                            .ForEach(x => _dbContext.Entry(x).Collection(c => c.Bigliettos).Load());
                        CurrentSelectedProject.IdConcertos.ToList()
                            .ForEach(c => c.Bigliettos.ToList().ForEach(b => TotBiglietti += b.NumVenduti));
                        UpdateResults("Biglietti");
                        return true;
                    }
                    return false;
                case "Inserisci":
                    if (CheckProject())
                    {
                        _dbContext.ProgettiMusicali.Local.Add(NewProject);
                        _dbContext.SaveChanges();
                        ResetInsert();
                        ShowSuccess();
                        return true;
                    }
                    Error = "Dati invalidi.";
                    ShowError();
                    return false;
                default: return false;
            }
        }

        [RelayCommand]
        private void ConfirmAction() => ExecuteSubAction();

        [RelayCommand]
        private void AddFirmatario()
        {
            if (SelectedFirmatario != null
                && !FirmatarioList.Contains(SelectedFirmatario)
                && (SelectedType != "Solista" || FirmatarioList.Count == 0))
            {
                List<Firmatario> newFirmatariList = new(FirmatarioList);
                newFirmatariList.Add(SelectedFirmatario);
                FirmatarioList = newFirmatariList;
            }
        }

        [RelayCommand]
        private void RemoveFirmatario()
        {
            if (ToRemove != null)
            {
                List<Firmatario> newFirmatariList = new(FirmatarioList);
                newFirmatariList.Remove(ToRemove);
                FirmatarioList = newFirmatariList;
            }
        }

        private void UpdateResults(string key)
        {
            Dictionary<string, Visibility> newResults = new(ResultVisibility);
            newResults.Keys.ToList().ForEach(k => newResults[k] = Visibility.Collapsed);
            newResults[key] = Visibility.Visible;
            ResultVisibility = newResults;
        }

        private bool CheckProject()
        {
            if (SelectedType != null)
            {
                NewProject.Tipo = SelectedType;
                if (NewProject.Nome != null && NewProject.Nome.Length > 0
                    && (NewProject.DataScioglimento == null || NewProject.DataScioglimento > NewProject.DataFormazione))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            switch (e.PropertyName)
            {
                case "SelectedType":
                    if (SelectedType == "Solista" && FirmatarioList.Count >= 2)
                    {
                        FirmatarioList = new();
                    }
                    break;
            }
        }

        public new void InsertGridSelected()
        {
            base.InsertGridSelected();
            SetCurrentSubAction("Inserisci");
        }

        protected override void ResetInsert()
        {
            NewProject = new()
            {
                DataFormazione = DateTime.Now,
            };
            FirmatarioList = new();
        }
    }
}
