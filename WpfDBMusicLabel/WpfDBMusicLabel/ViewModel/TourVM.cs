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
    partial class TourVM : AbstractVM
    {
        [ObservableProperty]
        private Tour? currentSelectedTour = null;

        [ObservableProperty]
        private Concerto? currentSelectedConcert = null;

        [ObservableProperty]
        private List<Biglietto> biglietti = new();

        [ObservableProperty]
        private List<string> subActionList = new()
        {
            "Vedi concerti"
        };

        [ObservableProperty]
        private ObservableCollection<Tour> tours;

        [ObservableProperty]
        private IEnumerable<Concerto>? concerti;

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

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            switch (e.PropertyName)
            {
                case "CurrentSelectedConcert":
                    if (CurrentSelectedConcert != null)
                    {
                        _dbContext.Entry(CurrentSelectedConcert).Collection(c => c.Bigliettos).Load();
                        Biglietti = CurrentSelectedConcert.Bigliettos.ToList();
                    }
                    break;
            }
        }
        override public bool ExecuteSubAction()
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
                        ResetInsert();
                        ShowSuccess();
                        return true;
                    }
                    Error = "Informazioni necessarie all'inserimento non presenti.";
                    ShowError();
                    return false;

                default:
                    Error = "The selected sub action is not implemented.";
                    ShowError();
                    return false;
            }
        }

        [RelayCommand]
        private void Save() => base.SaveChanges();

        new public void ViewGridSelected()
        {
            base.ViewGridSelected();
            CurrentSubAction = null;
        }

        protected override void ResetInsert()
        {
            NewTourName = "";
            _newTour = new();
            NewConcerts = new();
            DataConcerto = DateTime.Now;
            _dbContext.Luoghi.Load();
            Luoghi = _dbContext.Luoghi.Local.ToList();
        }
    }
}
