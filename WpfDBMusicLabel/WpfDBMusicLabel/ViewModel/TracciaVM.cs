using CommunityToolkit.Mvvm.ComponentModel;
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
    partial class TracciaVM : ObservableRecipient, ISubVM
    {
        private readonly MusiclabeldbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<Traccia>? tracce;

        [ObservableProperty]
        private Traccia? currentSelectedTrack = null;

        [ObservableProperty]
        private ObservableCollection<Firmatario>? firmatari;

        [ObservableProperty]
        private string? currentSubAction = null;

        [ObservableProperty]
        private Visibility trackViewVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private Visibility trackInsertVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private Visibility trackDeleteVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private string? error = null;

        [ObservableProperty]
        private List<string> subActionsList = new()
        {
            "Vedi dati"
        };

        public TracciaVM(MusiclabeldbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbContext.Tracce.Load();
            Tracce = this._dbContext.Tracce.Local.ToObservableCollection();
        }

        public bool ExecuteSubAction()
        {
            if (CurrentSelectedTrack != null)
            {
                //Per avere solo i firmatari, trovare un modo per caricare tutti i dati
                this._dbContext.Firmatari.Where(firmatario => firmatario.IdTraccia.Any(traccia => traccia.IdTraccia == CurrentSelectedTrack.IdTraccia)).Load();
                Firmatari = _dbContext.Firmatari.Local.ToObservableCollection();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetCurrentSubAction(string newSubAction) => currentSubAction = newSubAction;

        public void ViewGridSelected()
        {
            TrackViewVisibility = Visibility.Visible;
            TrackInsertVisibility = Visibility.Collapsed;
            TrackDeleteVisibility = Visibility.Collapsed;
        }

        public void InsertGridSelected()
        {
            TrackInsertVisibility = Visibility.Visible;
            TrackViewVisibility = Visibility.Collapsed;
            TrackDeleteVisibility = Visibility.Collapsed;
        }

        public void DeleteGridSelected()
        {
            TrackDeleteVisibility = Visibility.Visible;
            TrackViewVisibility = Visibility.Collapsed;
            TrackInsertVisibility = Visibility.Collapsed;
        }

        public void OtherVMSelected()
        {
            TrackViewVisibility = Visibility.Collapsed;
            TrackInsertVisibility = Visibility.Collapsed;
            TrackDeleteVisibility = Visibility.Collapsed;
        }
    }
}
