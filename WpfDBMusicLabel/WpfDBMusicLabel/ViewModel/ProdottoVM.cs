using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDBMusicLabel.musiclabeldb;
using System.Windows;
using System.Data.Entity;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfDBMusicLabel.ViewModel
{
    partial class ProdottoVM : AbstractVM
    {
        [ObservableProperty]
        private ObservableCollection<Prodotto>? prodotti;

        [ObservableProperty]
        private Prodotto? currentSelectedProduct = null;

        [ObservableProperty]
        private Prodotto newProd = new();

        [ObservableProperty]
        private ObservableCollection<Album>? albums;

        [ObservableProperty]
        private Album? selectedAlbum = null;

        [ObservableProperty]
        private Visibility formatoVis = Visibility.Collapsed;

        [ObservableProperty]
        private bool cd = false;

        [ObservableProperty]
        private bool vinyl = false;

        [ObservableProperty]
        private bool cassette = false;

        [ObservableProperty]
        private bool giri33 = false;

        [ObservableProperty]
        private bool giri45 = false;

        [ObservableProperty]
        private string? sellPrice = null;

        [ObservableProperty]
        private string? qty = null;
 
        [ObservableProperty]
        private List<string> subActionsList = new()
        {
            "Vedi album"
        };

        public ProdottoVM()
        {
            _dbContext = new();
            _dbContext.Prodotti.Load();
            Prodotti = _dbContext.Prodotti.Local.ToObservableCollection();
        }

        override public bool ExecuteSubAction()
        {
            if (CurrentSelectedProduct != null)
            {
                _dbContext.Albums.Where(x => x.Prodotti.Any(y => y.Formato == CurrentSelectedProduct.Formato)).Load(); 
                Albums = _dbContext.Albums.Local.ToObservableCollection();
                return true;
            }
            return false;
        }

        protected override void ResetInsert()
        {
            _dbContext.Albums.Load();
            Albums = _dbContext.Albums.Local.ToObservableCollection();
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            switch (e.PropertyName)
            {
                case nameof(Vinyl):
                    FormatoVis = Vinyl == true ? Visibility.Visible : Visibility.Collapsed;
                    break;
            }
        }
    }
}
