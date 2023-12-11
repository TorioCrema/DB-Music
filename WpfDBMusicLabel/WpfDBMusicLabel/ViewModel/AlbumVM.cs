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
    partial class AlbumVM : AbstractVM
    {
        [ObservableProperty]
        private ObservableCollection<Album> albums;

        [ObservableProperty]
        private Album? currentSelectedAlbum = null;

        [ObservableProperty]
        private List<Prodotto>? prodotti;

        [ObservableProperty]
        private List<ProgettoMusicale>? artists = null;

        [ObservableProperty]
        private ProgettoMusicale? selectedArtist = null;

        [ObservableProperty]
        private ObservableCollection<Traccia>? artistTracks = null;

        [ObservableProperty]
        private Dictionary<string, Visibility> resultVisibility = new()
        {
            { "Prodotti", Visibility.Collapsed },
            { "Tracce", Visibility.Collapsed }
        };

        [ObservableProperty]
        private List<string> subActionList = new()
        {
            "Edizioni Fisiche",
            "Vedi tracce"
        };

        public AlbumVM()
        {
            _dbContext.Albums.Load();
            _dbContext.ProgettiMusicali.Load();
            Artists = _dbContext.ProgettiMusicali.Local.ToList();
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

        public override bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Edizioni Fisiche":
                    if (CurrentSelectedAlbum != null)
                    {
                        _dbContext.Entry(CurrentSelectedAlbum).Collection(a => a.Prodotti).Load();
                        Prodotti = CurrentSelectedAlbum.Prodotti.ToList();
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

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            switch (e.PropertyName)
            {
                case nameof(SelectedArtist):
                    ArtistTracks = new();
                    if (SelectedArtist != null)
                    {
                        _dbContext.Entry(SelectedArtist).Collection(x => x.Traccia).Load();
                        foreach (var traccia in _dbContext.Tracce.Local)
                        {
                            if (traccia.IdProgettoNavigation.Equals(SelectedArtist))
                            {
                                ArtistTracks.Add(traccia);
                            }
                        }
                    }
                    break;
                case nameof(CurrentSelectedAlbum):
                    ExecuteSubAction();
                    break;
            }
        }

        protected override void ResetInsert()
        {
            SelectedArtist = null;
            ArtistTracks = null;
            _dbContext.Albums.Load();
            _dbContext.ProgettiMusicali.Load();
            Artists = _dbContext.ProgettiMusicali.Local.ToList();
            Albums = _dbContext.Albums.Local.ToObservableCollection();
        }
    }
}
