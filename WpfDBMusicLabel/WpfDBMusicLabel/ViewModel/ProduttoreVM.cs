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
    partial class ProduttoreVM : AbstractVM
    {
        [ObservableProperty]
        private ObservableCollection<Produttore>? produttori;

        [ObservableProperty]
        private List<Prodotto> prodotti;

        [ObservableProperty]
        private List<Merchandising> merch;

        [ObservableProperty]
        private Produttore? currentSelectedProduttore = null;

        [ObservableProperty]
        private Dictionary<string, Visibility> resultVisibility = new()
        {
            { "Prods", Visibility.Collapsed },
            { "Merches", Visibility.Collapsed }
        };

        [ObservableProperty]
        private List<string> subActionsList = new()
        {
            "Vedi prodotti",
            "Vedi merch",
        };

        public ProduttoreVM() 
        {
            _dbContext.Produttori.Load();
            _dbContext.Prodotti.Load();
            _dbContext.Merchandisings.Load();
            Produttori = _dbContext.Produttori.Local.ToObservableCollection();
            Prodotti = _dbContext.Prodotti.Local.ToList();
            Merch = _dbContext.Merchandisings.Local.ToList();
        }

        public override bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Vedi prodotti":
                    if (CurrentSelectedProduttore != null)
                    {
                        _dbContext.Entry(CurrentSelectedProduttore).Collection(p => p.Prodottos).Load();
                        Prodotti = CurrentSelectedProduttore.Prodottos.ToList();
                        UpdateResults("Prods");
                        return true;
                    }
                    return false;
                case "Vedi merch":
                    if (CurrentSelectedProduttore != null)
                    {
                        _dbContext.Entry(CurrentSelectedProduttore).Collection(p => p.Merchandisings).Load();
                        Merch = CurrentSelectedProduttore.Merchandisings.ToList();
                        UpdateResults("Merches");
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }

        protected override void ResetInsert()
        {
            CurrentSelectedProduttore = null;
            Prodotti = new();
            Merch = new();
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
    }
}
