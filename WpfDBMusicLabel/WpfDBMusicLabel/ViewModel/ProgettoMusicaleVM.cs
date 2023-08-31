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
    partial class ProgettoMusicaleVM : ObservableRecipient, ISubVM
    {
        private readonly MusiclabeldbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<ProgettoMusicale> progettiMusicali;

        [ObservableProperty]
        private ProgettoMusicale? currentSelectedProject = null;

        [ObservableProperty]
        private ObservableCollection<Album>? albums;

        [ObservableProperty]
        private ObservableCollection<Traccia>? tracce;

        [ObservableProperty]
        private ObservableCollection<Firmatario>? firmatari;

        [ObservableProperty]
        private ObservableCollection<Concerto>? concerti;

        [ObservableProperty]
        private string? currentSubAction = null;

        [ObservableProperty]
        private Visibility projectInsertVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private string? error = null;

        [ObservableProperty]
        private ObservableCollection<string> subActionsList = new()
        {
            "Vedi album",
            "Vedi tracce",
            "Vedi firmatari",
            "Vedi concerti"
        };

        public ProgettoMusicaleVM(MusiclabeldbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbContext.ProgettiMusicali.Load();
            ProgettiMusicali = this._dbContext.ProgettiMusicali.Local.ToObservableCollection();
        }

        public bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Vedi album":
                    _dbContext.Albums.Where(album => album.IdProgetto == CurrentSelectedProject.IdProgetto).Load();
                    Albums = _dbContext.Albums.Local.ToObservableCollection();
                    return true;
                case "Vedi tracce":
                    _dbContext.Tracce.Where(traccia => traccia.IdProgetto == CurrentSelectedProject.IdProgetto).Load();
                    Tracce = _dbContext.Tracce.Local.ToObservableCollection();
                    return true;
                case "Vedi firmatari":
                    _dbContext.Firmatari.Include(firmatari => firmatari.IdProgetti).Where(firmatario => firmatario.IdProgetti.Any(progetto => progetto.IdProgetto == CurrentSelectedProject.IdProgetto)).Load();
                    Firmatari = _dbContext.Firmatari.Local.ToObservableCollection();
                    return true;
                case "Vedi concerti":
                    _dbContext.Concerti.Where(concerto => concerto.IdProgetti.Any(progetto => progetto.IdProgetto == CurrentSelectedProject.IdProgetto));
                    Concerti = _dbContext.Concerti.Local.ToObservableCollection();
                    return true;
                default: return false;
            }
        }

        public void InsertGridSelected() => throw new NotImplementedException();

        public void OtherVMSelected() => throw new NotImplementedException();

        public void SetCurrentSubAction(string newSubAction) => CurrentSubAction = newSubAction;
    }
}
