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

namespace WpfDBMusicLabel.ViewModel
{
    partial class ProdottoVM : AbstractVM
    {
        [ObservableProperty]
        private ObservableCollection<Prodotto>? prodotti;

        [ObservableProperty]
        private Prodotto? currentSelectedProduct = null;

        [ObservableProperty]
        private ObservableCollection<Album> albums;

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
            throw new NotImplementedException();
        }
    }
}
