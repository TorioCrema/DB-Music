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
        private Prodotto newProd = new()
        {
            Nome = "",
            Prezzo = 0,
            QtaProdotta = 0,
            CostoFornitura = 0,
            DataUscita = System.DateTime.Now,
            Descrizione = "",
            Formato = null
        };

        [ObservableProperty]
        private ObservableCollection<Album>? albums;

        [ObservableProperty]
        private ObservableCollection<Produttore>? producers;

        [ObservableProperty]
        private Album? selectedAlbum = null;

        [ObservableProperty]
        private Produttore? selectedProducer = null;

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
        private List<string> subActionsList = new()
        {
            "Vedi album"
        };

        public ProdottoVM()
        {
            _dbContext = new();
            _dbContext.Prodotti.Load();
            Prodotti = _dbContext.Prodotti.Local.ToObservableCollection();
            _dbContext.Produttori.Load();
            Producers = _dbContext.Produttori.Local.ToObservableCollection();
            _dbContext.Albums.Load();
            Albums = _dbContext.Albums.Local.ToObservableCollection();
        }

        override public bool ExecuteSubAction()
        {
            switch(CurrentSubAction)
            {
                case "Vedi album":
                    if (CurrentSelectedProduct != null)
                    {
                        _dbContext.Albums.Load();
                        Albums = _dbContext.Albums.Local.ToObservableCollection();
                        return true;
                    }
                    return false;
                case "Inserisci":
                    if (InsertCheck())
                    {
                        NewProd.IdAlbumNavigation = SelectedAlbum;
                        NewProd.IdProduttoreNavigation = SelectedProducer;
                        NewProd.Tipo = Cd ? "CD" : Vinyl ? "Vinile" : "Cassetta";
                        if (NewProd.Tipo == "Vinile")
                        {
                            NewProd.Formato = Giri33 ? (byte)33 : (byte)45;
                        }
                        _dbContext.Prodotti.Local.Add(NewProd);
                        SaveChanges();
                        ResetInsert();
                        return true;
                    }
                    Error = "InsertCheck failed";
                    return false;
            }
            return false;
        }

        protected override void ResetInsert()
        {
            SelectedAlbum = null;
            SelectedProducer = null;
            NewProd = new()
            {
                Nome = "",
                Prezzo = 0,
                QtaProdotta = 0,
                CostoFornitura = 0,
                DataUscita = System.DateTime.Now,
                Descrizione = "",
                Formato = null
            };
            Cd = false;
            Vinyl = false;
            Cassette = false;
            Giri33 = false;
            Giri45 = false;
            _dbContext.Albums.Load();
            Albums = _dbContext.Albums.Local.ToObservableCollection();
            CurrentSubAction = null;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            switch (e.PropertyName)
            {
                case nameof(Vinyl):
                    FormatoVis = Vinyl == true ? Visibility.Visible : Visibility.Collapsed;
                    if (!Vinyl)
                    {
                        Giri33 = false;
                        Giri45 = false;
                    }
                    break;
            }
        }

        private bool CheckProd() => NewProd.Prezzo >= 0 && NewProd.QtaProdotta >= 0
                && NewProd.CostoFornitura >= 0 && NewProd.Descrizione != "";

        private bool CheckSelections() => SelectedProducer != null && SelectedAlbum != null;

        private bool CheckFormat() => (Cd || Vinyl || Cassette) && (Vinyl && (Giri33 || Giri45) || (!Vinyl && !Giri33 && !Giri45));

        private bool InsertCheck() => CheckFormat() && CheckSelections() && CheckProd();
    }
}
