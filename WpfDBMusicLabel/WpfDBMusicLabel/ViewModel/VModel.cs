using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfDBMusicLabel.Model;
using WpfDBMusicLabel.musiclabeldb;

namespace WpfDBMusicLabel.ViewModel
{
    partial class VModel : ObservableRecipient
    {
        [ObservableProperty]
        private List<string> actionsList = new()
        {
            "Tour",
            "Album",
            "Firmatario",
            "Tracce",
            "Merchandising",
            "Prodotto",
            "Progetto Musicale"
        };

        private Dictionary<string, ISubVM> _vmDict = new();

        [ObservableProperty]
        private string? currentAction = null;

        [ObservableProperty]
        private ISubVM? currentVM = null;

        private readonly MusiclabeldbContext _dbContext = new();

        [ObservableProperty]
        private TourVM tourViewModel;

        [ObservableProperty]
        private FirmatarioVM firmatarioViewModel;

        [ObservableProperty]
        private ProgettoMusicaleVM progettoViewModel;

        [ObservableProperty]
        private TracciaVM tracciaViewModel;

        [ObservableProperty]
        private ProdottoVM prodottoViewModel;

        [ObservableProperty]
        private AlbumVM albumViewModel;

        [ObservableProperty]
        private ObservableCollection<ProgettoMusicale>? progetti;

        [ObservableProperty]
        private string? error = null;

        [ObservableProperty]
        private bool actionBtsEnabled = false;

        [ObservableProperty]
        private Visibility viewGridVisibilty = Visibility.Collapsed;

        [ObservableProperty]
        private Visibility insertGridVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private Visibility deleteGridVisibility = Visibility.Collapsed;

        [RelayCommand]
        private void VisualizzaSelected()
        {
            CurrentVM?.ViewGridSelected();
            ViewGridVisibilty = Visibility.Visible;
            InsertGridVisibility = Visibility.Collapsed;
            DeleteGridVisibility = Visibility.Collapsed;
        }

        [RelayCommand]
        private void ExecuteSubAction()
        {
            bool res = CurrentVM != null ? CurrentVM.ExecuteSubAction() : true;
            if (!res)
            {
                CurrentVM?.ShowError();
            }
        }

        [RelayCommand]
        private void InsertSelected()
        {
            CurrentVM?.InsertGridSelected();
            InsertGridVisibility = Visibility.Visible;
            ViewGridVisibilty = Visibility.Collapsed;
            DeleteGridVisibility = Visibility.Collapsed;
        }

        [RelayCommand]
        private void DeleteSelected()
        {
            CurrentVM?.DeleteGridSelected();
            DeleteGridVisibility = Visibility.Visible;
            InsertGridVisibility = Visibility.Collapsed;
            ViewGridVisibilty = Visibility.Collapsed;
        }

        public VModel()
        {
            TourViewModel = new();
            _vmDict.Add("Tour", TourViewModel);
            FirmatarioViewModel = new();
            _vmDict.Add("Firmatario", FirmatarioViewModel);
            ProgettoViewModel = new();
            _vmDict.Add("Progetto Musicale", ProgettoViewModel);
            AlbumViewModel = new();
            _vmDict.Add("Album", AlbumViewModel);
            ProdottoViewModel = new();
            _vmDict.Add("Prodotto", ProdottoViewModel);
            TracciaViewModel = new();
            _vmDict.Add("Tracce", TracciaViewModel);

        }

        public bool SetCurrentAction(string action)
        {
            CurrentVM?.OtherVMSelected();
            _vmDict.TryGetValue(action, out ISubVM? subVM);
            CurrentVM = subVM;
            if (CurrentVM != null)
            {
                ActionBtsEnabled = true;
                return true;
            }
            return false;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            switch (e.PropertyName)
            {
                case nameof(CurrentAction):
                    SetCurrentAction(CurrentAction);
                    break;
            }
        }
    }
}
