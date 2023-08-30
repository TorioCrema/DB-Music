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
    partial class AlbumVM : ObservableRecipient, ISubVM
    {
        private readonly MusiclabeldbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<Album> albums;

        [ObservableProperty]
        private Album? currentSelectedAlbum = null;

        [ObservableProperty]
        private ObservableCollection<Prodotto>? prodotti;

        [ObservableProperty]
        private string? currentSubAction = null;

        [ObservableProperty]
        private Visibility albumInsertVisibilty = Visibility.Collapsed;

        [ObservableProperty]
        private string? error = null;

        [ObservableProperty]
        private List<string> subActionList = new()
        {
            "Edizioni Fisiche"
        };

        public AlbumVM(MusiclabeldbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Albums.Load();
            Albums = _dbContext.Albums.Local.ToObservableCollection();
        }

        public bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Edizioni Fisiche":
                    if (CurrentSelectedAlbum != null)
                    {
                        _dbContext.Prodottos.Where(x => x.IdAlbum == CurrentSelectedAlbum.IdAlbum).Load();
                        Prodotti = _dbContext.Prodottos.Local.ToObservableCollection();
                        return true;
                    }
                    Error = "Album non selezionato";
                    return false;
                default:
                    Error = "The selected sub action is not implemented.";
                    return false;
            }
        }

        public void InsertGridSelected()
        {
            AlbumInsertVisibilty = Visibility.Visible;
            CurrentSubAction = "Inserisci";
        }

        public void OtherVMSelected() => AlbumInsertVisibilty = Visibility.Collapsed;

        public void SetCurrentSubAction(string newSubAction) => CurrentSubAction = newSubAction;
    }
}
