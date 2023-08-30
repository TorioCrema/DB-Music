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
        private ObservableCollection<ProgettoMusicale>? progetti;

        [ObservableProperty]
        private string? error = null;

        [ObservableProperty]
        private bool actionBtsEnabled = false;

        [ObservableProperty]
        private Visibility viewGridVisibilty = Visibility.Collapsed;

        [ObservableProperty]
        private Visibility insertGridVisibility = Visibility.Collapsed;

        [RelayCommand]
        private void VisualizzaSelected()
        {
            ViewGridVisibilty = Visibility.Visible;
            InsertGridVisibility = Visibility.Collapsed;
        }

        [RelayCommand]
        private void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        [RelayCommand]
        private void InsertSelected()
        {
            CurrentVM?.InsertGridSelected();
            InsertGridVisibility = Visibility.Visible;
            ViewGridVisibilty = Visibility.Collapsed;
        }

        public VModel()
        {
            _dbContext.ProgettiMusicali.Load();
            Progetti = _dbContext.ProgettiMusicali.Local.ToObservableCollection();
            TourViewModel = new(_dbContext);
            FirmatarioViewModel = new(_dbContext);
        }

        public bool SetCurrentAction(string action)
        {
            CurrentVM?.OtherVMSelected();
            switch (action)
            {
                case "Tour":
                    CurrentVM = TourViewModel;
                    ActionBtsEnabled = true;
                    return true;

                case "Firmatario":
                    CurrentVM = FirmatarioViewModel;
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
                case "CurrentAction":
                    SetCurrentAction(CurrentAction);
                    break;
            }
        }
    }
}
