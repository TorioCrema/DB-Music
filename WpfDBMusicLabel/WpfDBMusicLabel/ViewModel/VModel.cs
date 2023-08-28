using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            "Traccia",
            "Merchandising",
            "Prodotto",
            "Progetto Musicale"
        };

        [ObservableProperty]
        private ActionType? currentAction = null;

        [ObservableProperty]
        private ISubVM? currentVM = null;

        private readonly MusiclabeldbContext _dbContext = new();

        [ObservableProperty]
        private TourVM? tourViewModel;

        [ObservableProperty]
        private ObservableCollection<ProgettoMusicale>? progetti;

        [ObservableProperty]
        private string? error = null;

        [ObservableProperty]
        private bool actionBtsEnabled = false;

        [ObservableProperty]
        private Visibility viewGridVisibilty = Visibility.Collapsed;

        [RelayCommand]
        private void VisualizzaSelected()
        {
            ViewGridVisibilty = Visibility.Visible;
        }

        [RelayCommand]
        private void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public VModel()
        {
            /*_dbContext.ProgettoMusicales.Load();*/
            Progetti = _dbContext.ProgettoMusicales.Local.ToObservableCollection();
            TourViewModel = new(_dbContext);
        }

        public bool SetCurrentAction(string action)
        {
            switch (action)
            {
                case "Tour":
                    CurrentVM = TourViewModel;
                    ActionBtsEnabled = true;
                    return true;
            }
            return false;
        }
    }
}
