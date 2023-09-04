using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    partial class ProgettoMusicaleVM : ObservableRecipient, ISubVM
    {
        private readonly MusiclabeldbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<ProgettoMusicale> progettiMusicali;

        [ObservableProperty]
        private ProgettoMusicale? currentSelectedProject = null;

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
        private string? currentSubAction = null;

        [ObservableProperty]
        private Visibility projectInsertVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private Visibility projectViewVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private string? error = null;

        [ObservableProperty]
        private List<string> subActionsList = new()
        {
            "Vedi album",
            "Vedi tracce",
            "Vedi firmatari",
            "Vedi concerti",
            "Vedi features"
        };

        [ObservableProperty]
        private Dictionary<string, Visibility> resultVisibility = new()
        {
            { "Albums", Visibility.Collapsed },
            { "Tracks", Visibility.Collapsed },
            { "Firmatari", Visibility.Collapsed },
            { "Concerts", Visibility.Collapsed },
            { "Features", Visibility.Collapsed }
        };

        public ProgettoMusicaleVM()
        {
            _dbContext = new();
            _dbContext.ProgettiMusicali.Load();
            ProgettiMusicali = _dbContext.ProgettiMusicali.Local.ToObservableCollection();
        }

        public bool ExecuteSubAction()
        {
            if (CurrentSelectedProject == null)
            {
                Error = "Selezionare un progetto";
                return false;
            }
            switch (CurrentSubAction)
            {
                case "Vedi album":
                    _dbContext.Entry(CurrentSelectedProject).Collection(p => p.Albums).Load();
                    Albums = CurrentSelectedProject.Albums.ToList();
                    UpdateResults("Albums");
                    return true;
                case "Vedi tracce":
                    _dbContext.Entry(CurrentSelectedProject).Collection(p => p.Traccia).Load();
                    Tracce = CurrentSelectedProject.Traccia.ToList();
                    UpdateResults("Tracks");
                    return true;
                case "Vedi firmatari":
                    _dbContext.Entry(CurrentSelectedProject).Collection(p => p.IdFirmatarios).Load();
                    Firmatari = CurrentSelectedProject.IdFirmatarios.ToList();
                    UpdateResults("Firmatari");
                    return true;
                case "Vedi concerti":
                    _dbContext.Entry(CurrentSelectedProject).Collection(p => p.IdConcertos).Load();
                    Concerti = CurrentSelectedProject.IdConcertos.ToList();
                    UpdateResults("Concerts");
                    return true;
                case "Vedi features":
                    _dbContext.Entry(CurrentSelectedProject).Collection(p => p.IdTraccia).Load();
                    Features = CurrentSelectedProject.IdTraccia.ToList();
                    Features.ForEach(f => _dbContext.Entry(f).Reference(x => x.IdProgettoNavigation).Load());
                    UpdateResults("Features");
                    return true;
                default: return false;
            }
        }

        [RelayCommand]
        private void ConfirmAction() => ExecuteSubAction();

        private void UpdateResults(string key)
        {
            Dictionary<string, Visibility> newResults = new(ResultVisibility);
            newResults.Keys.ToList().ForEach(k => newResults[k] = Visibility.Collapsed);
            newResults[key] = Visibility.Visible;
            ResultVisibility = newResults;
        }

        public void InsertGridSelected()
        {
            ProjectInsertVisibility = Visibility.Visible;
            ProjectViewVisibility = Visibility.Collapsed;
        }

        public void OtherVMSelected()
        {
            ProjectInsertVisibility = Visibility.Collapsed;
            ProjectViewVisibility = Visibility.Collapsed;
        }
        public void SetCurrentSubAction(string newSubAction) => CurrentSubAction = newSubAction;

        public void ViewGridSelected()
        {
            ProjectInsertVisibility = Visibility.Collapsed;
            ProjectViewVisibility = Visibility.Visible;
        }
    }
}
