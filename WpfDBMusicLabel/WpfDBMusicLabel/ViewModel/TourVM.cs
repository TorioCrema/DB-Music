using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDBMusicLabel.musiclabeldb;

namespace WpfDBMusicLabel.ViewModel
{
    internal partial class TourVM : ObservableRecipient, ISubVM
    {
        [ObservableProperty]
        private string? currentSubAction = null;

        [ObservableProperty]
        private Tour? currentSelectedTour = null;

        [ObservableProperty]
        private List<string> subActionList = new()
        {
            "Concerti"
        };

        private readonly MusiclabeldbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<Tour>? tours;

        [ObservableProperty]
        private ObservableCollection<Concerto>? concerti;

        [ObservableProperty]
        private string? error;

        public TourVM(MusiclabeldbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Tours.Load();
            Tours = _dbContext.Tours.Local.ToObservableCollection();
        }

        public bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Concerti":
                    ObservableCollection<Concerto> newConcerti = new();
                    _dbContext.Concertos.Where(x => x.IdTour == CurrentSelectedTour.IdTour).Load();
                    _dbContext.Concertos.Local.Where(x => x.IdTour == CurrentSelectedTour.IdTour).ToList().ForEach(x => newConcerti.Add(x));
                    Concerti = newConcerti;
                    return true;
                default:
                    Error = "The selected sub action is not implemented.";
                    return false;
            }
        }

        public void SetCurrentSubAction(string newSubAction)
        {
            CurrentSubAction = newSubAction;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            switch (e.PropertyName)
            {
                case "CurrentSubAction":
                    this.ExecuteSubAction();
                    break;
                case "CurrentSelectedTour":
                    if (CurrentSubAction != null)
                    {
                        this.ExecuteSubAction();
                    }
                    break;
            }
        }
    }
}
