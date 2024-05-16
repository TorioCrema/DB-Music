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
        private string newAlbumName = "";

        [ObservableProperty]
        private DateTime newAlbumDate = DateTime.Now;

        [ObservableProperty]
        private ProgettoMusicale? newAlbumProj = null; 

        [ObservableProperty]
        private uint newAlbumDuration = 0;

        [ObservableProperty]
        private ObservableCollection<Traccia>? selectedTracks = null;

        [ObservableProperty]
        private Traccia? currentSelectedTraccia = null;

        private Album _newAlbum = new();

        [ObservableProperty]
        private List<Traccia> newTracce = new();

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
            _dbContext.Tracce.Load();
            Artists = _dbContext.ProgettiMusicali.Local.ToList();
            Albums = _dbContext.Albums.Local.ToObservableCollection();
            ArtistTracks = _dbContext.Tracce.Local.ToObservableCollection();
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

                case "Inserisci":
                    if (!checkAlbumName(NewAlbumName))
                    {
                        Error = "Nome dell'album già esistente";
                        ShowError();
                        return false;
                    }
                    else if (NewAlbumName != null 
                        && SelectedArtist != null 
                        && NewTracce.Count > 0)
                    {
                        _newAlbum.Nome = NewAlbumName;
                        _newAlbum.Durata = NewAlbumDuration;
                        _newAlbum.IdProgetto = SelectedArtist.IdProgetto;
                        _newAlbum.DataPubblicazione = NewAlbumDate;
                        foreach (var t in NewTracce)
                        {
                            var existingTrack = _dbContext.Tracce.Local.FirstOrDefault(tr => tr.IdTraccia == t.IdTraccia);
                            if (existingTrack != null)
                            {
                                _newAlbum.IdTraccia.Add(existingTrack);
                            }
                        }
                        _dbContext.Albums.Local.Add(_newAlbum);
                        SaveChanges();
                        ResetInsert();
                        ShowSuccess();
                        return true;
                    }
                    Error = "Informazioni necessarie all'inserimento non presenti.";
                    ShowError();
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
                    NewTracce = new();
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

        [RelayCommand]
        private void Save() => base.SaveChanges();

        protected override void ResetInsert()
        {
            SelectedArtist = null;
            ArtistTracks = null;
            NewAlbumDate = DateTime.Now;
            NewAlbumDuration = 0;
            NewAlbumName = "";
            NewTracce = new();
            _dbContext.Albums.Load();
            _dbContext.ProgettiMusicali.Load();
            Artists = _dbContext.ProgettiMusicali.Local.ToList();
            Albums = _dbContext.Albums.Local.ToObservableCollection();
        }

        private bool checkAlbumName(string albumName)
        {
            foreach (var album in SelectedArtist.Albums)
            {
                if (album.Nome.ToLower().Equals(albumName.ToLower()))
                {
                    return false;
                }
            }
            return true;
        }

        [RelayCommand]
        private void AddTraccia()
        {
            if (CurrentSelectedTraccia != null)
            {
                List<Traccia> tracce = new List<Traccia>();
                NewTracce.ForEach(t => tracce.Add(t));
               
                if (!NewTracce.Any(t => t.Nome.Equals(CurrentSelectedTraccia.Nome)))
                {
                    tracce.Add(new Traccia()
                    {
                        Nome = CurrentSelectedTraccia.Nome,
                        Durata = CurrentSelectedTraccia.Durata,
                        IdTraccia = CurrentSelectedTraccia.IdTraccia
                    });
                    NewTracce = tracce;
                }
            }
        }

        [RelayCommand]
        private void SubAction() => ExecuteSubAction();
    }
}
