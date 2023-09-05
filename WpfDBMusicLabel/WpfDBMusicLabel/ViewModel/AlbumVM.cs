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
    partial class AlbumVM : ObservableRecipient, ISubVM
    {
        private readonly MusiclabeldbContext _dbContext = new();

        [ObservableProperty]
        private ObservableCollection<Album> albums;

        [ObservableProperty]
        private Album? currentSelectedAlbum = null;

        [ObservableProperty]
        private ObservableCollection<Prodotto>? prodotti;

        [ObservableProperty]
        private string? currentSubAction = null;

        [ObservableProperty]
        private Dictionary<string, Visibility> resultVisibility = new()
        {
            { "Prodotti", Visibility.Collapsed },
            { "Tracce", Visibility.Collapsed }
        };

        [ObservableProperty]
        private Visibility albumInsertVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private Visibility albumViewVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private Visibility albumDeleteVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private string? error = null;

        [ObservableProperty]
        private List<string> subActionList = new()
        {
            "Edizioni Fisiche",
            "Vedi tracce"
        };

        public AlbumVM()
        {
            _dbContext.Albums.Load();
            Albums = _dbContext.Albums.Local.ToObservableCollection();
        }

        private void UpdateResults(string key)
        {
            Dictionary<string, Visibility> newResults = new(ResultVisibility);
            newResults.Keys.ToList().ForEach(k => newResults[k] = Visibility.Collapsed);
            newResults[key] = Visibility.Visible;
            ResultVisibility = newResults;
        }

        [RelayCommand]
        private void Confirm() => ExecuteSubAction();

        public bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Edizioni Fisiche":
                    if (CurrentSelectedAlbum != null)
                    {
                        _dbContext.Prodotti.Where(x => x.IdAlbum == CurrentSelectedAlbum.IdAlbum).Load();
                        Prodotti = _dbContext.Prodotti.Local.ToObservableCollection();
                        UpdateResults("Prodotti");
                        return true;
                    }
                    Error = "Album non selezionato";
                    return false;
                case "Vedi tracce":
                    if (CurrentSelectedAlbum != null)
                    {
                        _dbContext.Entry(CurrentSelectedAlbum).Collection(a => a.IdTraccia).Load();
                        UpdateResults("Tracce");
                    return true;
                    }
                    Error = "Album non selezionato";
                    return false;
                default:
                    Error = "The selected sub action is not implemented.";
                    return false;
            }
        }

        public void SetCurrentSubAction(string newSubAction) => CurrentSubAction = newSubAction;

        public void ViewGridSelected()
        {
            AlbumViewVisibility = Visibility.Visible;
            AlbumInsertVisibility = Visibility.Collapsed;
            AlbumDeleteVisibility = Visibility.Collapsed;
        }

        public void InsertGridSelected()
        {
            AlbumInsertVisibility = Visibility.Visible;
            AlbumViewVisibility = Visibility.Collapsed;
            AlbumDeleteVisibility = Visibility.Collapsed;
            CurrentSubAction = "Inserisci";
        }

        public void DeleteGridSelected()
        {
            AlbumDeleteVisibility = Visibility.Visible;
            AlbumViewVisibility = Visibility.Collapsed;
            AlbumInsertVisibility = Visibility.Collapsed;
        }

        public void OtherVMSelected()
        {
            AlbumInsertVisibility = Visibility.Collapsed;
            AlbumViewVisibility = Visibility.Collapsed;
            AlbumInsertVisibility = Visibility.Collapsed;
        }

        public void SaveChanges()
        {
            _dbContext.ChangeTracker.DetectChanges();
            _dbContext.SaveChanges();
        }
    }
}
