using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MySqlX.XDevAPI;
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
        private uint dispTot = new();

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
        private ObservableCollection<ProgettoMusicale> progetti;

        [ObservableProperty]
        private ProgettoMusicale selectedProject;

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
            _dbContext.ProgettiMusicali.Load();
            Progetti = _dbContext.ProgettiMusicali.Local.ToObservableCollection();
            Tours = _dbContext.Tours.Local.ToObservableCollection();
            SelectedProject = Progetti.First();
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

        [RelayCommand]
        private void Confirm() => ExecuteSubAction();

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
                            _dbContext.Entry(concerto).Collection(c => c.Bigliettos).Load();
                        }
                        return true;
                    }
                    return false;

                case "Inserisci":
                    if (NewTourName != "" && NewConcerts.Any())
                    {
                        _newTour.Nome = NewTourName;
                        _newTour.Concertos = NewConcerts;
                        _dbContext.Tours.Local.Add(_newTour);
                        foreach (var c in NewConcerts)
                        {
                            SelectedProject.IdConcertos.Add(c);
                        }
                        SaveChanges();
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

        protected override void ResetInsert()
        {
            NewTourName = "";
            SelectedProject = Progetti.First();
            _newTour = new();
            NewConcerts = new();
            DataConcerto = DateTime.Now;
            _dbContext.Luoghi.Load();
            Luoghi = _dbContext.Luoghi.Local.ToList();
        }
    }
}
