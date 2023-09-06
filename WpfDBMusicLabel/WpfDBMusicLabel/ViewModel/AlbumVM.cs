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
    partial class AlbumVM : AbstractVM
    {
        [ObservableProperty]
        private ObservableCollection<Album> albums;

        [ObservableProperty]
        private Album? currentSelectedAlbum = null;

        [ObservableProperty]
        private List<Prodotto>? prodotti;

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

        protected override void ResetInsert()
        {
            throw new NotImplementedException();
        }
    }
}
