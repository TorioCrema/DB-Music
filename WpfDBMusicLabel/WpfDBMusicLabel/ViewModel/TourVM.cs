using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        private List<Luogo>? luoghi;

        [ObservableProperty]
        private Luogo? currentSelectedLuogo = null;

        [ObservableProperty]
        private DateTime dataConcerto = DateTime.Now;

        [ObservableProperty]
        private Visibility tourInsertVisibilty = Visibility.Collapsed;

        [ObservableProperty]
        private Visibility tourViewVisibility = Visibility.Collapsed;

        public TourVM()
        {
            _dbContext = new();
            _dbContext.Tours.Load();
            Tours = _dbContext.Tours.Local.ToObservableCollection();
        }

        [RelayCommand]
        private void SubAction() => ExecuteSubAction();

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
        private void SaveChanges()
        {
            _dbContext.ChangeTracker.DetectChanges();
            _dbContext.SaveChanges();
        }

        public void SetCurrentSubAction(string newSubAction) => CurrentSubAction = newSubAction;

        /*protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            switch (e.PropertyName)
            {
                case "CurrentSubAction":
                    if (CurrentSelectedTour != null)
                    {
                        ExecuteSubAction();
                    }
                    break;
                case "CurrentSelectedTour":
                    if (CurrentSubAction != null)
                    {
                        ExecuteSubAction();
                    }
                    break;
            }
        }*/

        public void InsertGridSelected()
        {
            TourInsertVisibilty = Visibility.Visible;
            TourViewVisibility = Visibility.Collapsed;
            CurrentSubAction = "Inserisci";
        }

        public void ViewGridSelected()
        {
            TourInsertVisibilty = Visibility.Collapsed;
            TourViewVisibility = Visibility.Visible;
            CurrentSubAction = null;
        }

        public void OtherVMSelected() => TourInsertVisibilty = Visibility.Collapsed;
    }
}
