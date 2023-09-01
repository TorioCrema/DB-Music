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
    partial class ProdottoVM : ObservableRecipient, ISubVM
    {
        private readonly MusiclabeldbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<Prodotto>? prodotti;

        [ObservableProperty]
        private Prodotto? currentSelectedProduct = null;

        [ObservableProperty]
        private ObservableCollection<Album> albums;

        [ObservableProperty]
        private string? currentSubAction = null;

        [ObservableProperty]
        private Visibility productInsertVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private string? error = null;
        [ObservableProperty]
        private List<string> subActionsList = new()
        {
            "Vedi album"
        };

        public ProdottoVM(MusiclabeldbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbContext.Prodotti.Load();
            this.Prodotti = this._dbContext.Prodotti.Local.ToObservableCollection();
        }

        public bool ExecuteSubAction()
        {
            if (CurrentSelectedProduct != null)
            {
                _dbContext.Albums.Where(x => x.Prodotti.Any(y => y.Formato == CurrentSelectedProduct.Formato)).Load(); 
                Albums = _dbContext.Albums.Local.ToObservableCollection();
                return true;
            }
            return false;
        }

        public void SetCurrentSubAction(string newSubAction) => CurrentSubAction = newSubAction;

        public void InsertGridSelected() => throw new NotImplementedException();

        public void OtherVMSelected() => throw new NotImplementedException();

        public void ViewGridSelected()
        {
            throw new NotImplementedException();
        }
    }
}
