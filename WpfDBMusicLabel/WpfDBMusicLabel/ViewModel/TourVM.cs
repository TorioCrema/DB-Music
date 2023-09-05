using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfDBMusicLabel.musiclabeldb;

namespace WpfDBMusicLabel.ViewModel
{
    public partial class TourVM : ObservableRecipient, ISubVM
    {
        [ObservableProperty]
        private string? currentSubAction = null;

        [ObservableProperty]
        private Tour? currentSelectedTour = null;

        [ObservableProperty]
        private List<string> subActionList = new()
        {
            "Vedi concerti"
        };

        private readonly MusiclabeldbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<Tour> tours;

        [ObservableProperty]
        private IEnumerable<Concerto>? concerti;

        [ObservableProperty]
        private string? error = null;

        [ObservableProperty]
        private List<Luogo>? luoghi = new();

        [ObservableProperty]
        private Luogo? currentSelectedLuogo = null;

        [ObservableProperty]
        private DateTime dataConcerto = DateTime.Now;

        [ObservableProperty]
        private string newTourName = "";

        [ObservableProperty]
        private List<Concerto> newConcerts = new();

        private Tour _newTour = new();

        [ObservableProperty]
        private Visibility tourViewVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private Visibility tourInsertVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private Visibility tourDeleteVisibility = Visibility.Collapsed;

        public TourVM()
        {
            _dbContext = new();
            _dbContext.Tours.Load();
            Tours = _dbContext.Tours.Local.ToObservableCollection();
        }

        [RelayCommand]
        private void SubAction() => ExecuteSubAction();

        [RelayCommand]
        private void AddConcerto()
        {
            if (CurrentSelectedLuogo != null)
            {
                List<Concerto> concerti = new();
                NewConcerts.ForEach(c => concerti.Add(c));
                concerti.Add(new Concerto()
                { 
                    IdLuogoNavigation = CurrentSelectedLuogo,
                    Data = DataConcerto.Date
                });
                NewConcerts = concerti;
            }
            else
            {
                Error = "Selezionare un luogo per il concerto.";
            }
        }

        [RelayCommand]
        private void AddTour()
        {
            if (NewTourName != "")
            {
                _newTour.Nome = NewTourName;
                _newTour.Concertos = NewConcerts;
                _dbContext.Tours.Add(_newTour);
                _dbContext.SaveChanges();
            }
            else
            {
                Error = "Inserire il nome del tour.";
            }
        }

        public bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Vedi concerti":
                    if (CurrentSelectedTour != null)
                    {
                        _dbContext.Entry(CurrentSelectedTour).Collection(t => t.Concertos).Load();
                        Concerti = CurrentSelectedTour.Concertos.ToList();
                        foreach (var concerto in Concerti)
                        {
                            _dbContext.Entry(concerto).Reference(c => c.IdLuogoNavigation).Load();
                        }
                        return true;
                    }
                    return false;

                case "Inserisci":
                    if (CurrentSelectedTour != null && CurrentSelectedLuogo != null)
                    {
                        Concerto concerto = new()
                        {
                            IdTour = CurrentSelectedTour.IdTour,
                            IdLuogo = CurrentSelectedLuogo.IdLuogo,
                            Data = DataConcerto
                        };
                        _dbContext.Entry(CurrentSelectedTour).Collection(t => t.Concertos).Load();
                        CurrentSelectedTour.Concertos.Add(concerto);
                        return true;
                    }
                    Error = "Informazioni necessarie all'inserimento non presenti.";
                    return false;

                default:
                    Error = "The selected sub action is not implemented.";
                    return false;
            }
        }

        [RelayCommand]
        private void Save() => SaveChanges();

        public void SetCurrentSubAction(string newSubAction) => CurrentSubAction = newSubAction;

        public void ViewGridSelected()
        {
            TourViewVisibility = Visibility.Visible;
            TourInsertVisibility = Visibility.Collapsed;
            TourDeleteVisibility = Visibility.Collapsed;
            CurrentSubAction = null;
        }

        public void InsertGridSelected()
        {
            TourInsertVisibility = Visibility.Visible;
            TourViewVisibility = Visibility.Collapsed;
            TourDeleteVisibility = Visibility.Collapsed;
            NewTourName = "";
            NewConcerts = new();
            DataConcerto = DateTime.Now;
            _dbContext.Luoghi.Load();
            Luoghi = _dbContext.Luoghi.Local.ToList();
        }

        public void DeleteGridSelected()
        {
            TourDeleteVisibility = Visibility.Visible;
            TourViewVisibility = Visibility.Collapsed;
            TourInsertVisibility = Visibility.Collapsed;
        }

        public void OtherVMSelected()
        {
            TourViewVisibility = Visibility.Collapsed;
            TourInsertVisibility = Visibility.Collapsed;
            TourDeleteVisibility = Visibility.Collapsed;
        }

        public void SaveChanges()
        {
            _dbContext.ChangeTracker.DetectChanges();
            _dbContext.SaveChanges();
        }
    }
}
