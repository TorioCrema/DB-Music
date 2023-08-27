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
        private List<string> subActionList = new();

        private readonly Dictionary<string, (ActionType, List<string>)> _actionMap = new()
        {
            {"Tour", (ActionType.Tour, new(){"Concerti"})},
            {"Album", (ActionType.Album, new(){ })},
            {"Firmatario", (ActionType.Firmatario, new(){ })},
            {"Traccia", (ActionType.Traccia, new(){ })},
            {"Merchandising", (ActionType.Merchandising, new(){ })},
            {"Prodotto", (ActionType.Prodotto, new(){ })},
            {"Progetto Musicale", (ActionType.ProgettoMusicale, new(){ })}
        };

        [ObservableProperty]
        private ActionType? currentAction = null;

        [ObservableProperty]
        private string? currentSubAction = null;

        [ObservableProperty]
        private Tour? currentSelectedTour = null;

        private readonly MusiclabeldbContext _dbContext = new();

        [ObservableProperty]
        private ObservableCollection<ProgettoMusicale>? progetti;

        [ObservableProperty]
        private ObservableCollection<Tour>? tours;

        [ObservableProperty]
        private ObservableCollection<Concerto>? concerti;

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
            _dbContext.ProgettoMusicales.Load();
            _dbContext.Tours.Load();
            Progetti = _dbContext.ProgettoMusicales.Local.ToObservableCollection();
            Tours = _dbContext.Tours.Local.ToObservableCollection();
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            switch (e.PropertyName)
            {
                case "CurrentSubAction":
                    this.ExecuteSubAction(CurrentSubAction);
                    break;
            }
        }

        public bool SetCurrentAction(string action)
        {
            if (_actionMap.TryGetValue(action, out (ActionType, List<string>) newAction))
            {
                CurrentAction = newAction.Item1;
                ActionBtsEnabled = true;
                SubActionList = newAction.Item2;
                return true;
            }
            else
            {
                Error = "The selected action is not implemented";
                return false;
            }
        }

        private void ExecuteSubAction(string subAction)
        {
            switch (subAction)
            {
                case "Concerti":
                    _dbContext.Concertos.Where(x => x.IdTour == CurrentSelectedTour.IdTour).Load();
                    Concerti = _dbContext.Concertos.Local.ToObservableCollection();
                    break;
            }
        }
    }
}
