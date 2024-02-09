using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfDBMusicLabel.musiclabeldb;

namespace WpfDBMusicLabel.ViewModel
{
    abstract partial class AbstractVM : ObservableRecipient, ISubVM
    {
        protected MusiclabeldbContext _dbContext = new();

        [ObservableProperty]
        private string? currentSubAction = null;

        [ObservableProperty]
        private string? error = null;

        [ObservableProperty]
        private Dictionary<string, Visibility> visibilityDict = new()
        {
            { "Insert", Visibility.Collapsed },
            { "View", Visibility.Collapsed },
            { "Delete", Visibility.Collapsed }
        };

        private void UpdateVisiblitity(string active)
        {
            VisibilityDict.Keys.ToList().ForEach(k => VisibilityDict[k] = Visibility.Collapsed);
            VisibilityDict[active] = Visibility.Visible;
            OnPropertyChanged(nameof(VisibilityDict));
        }

        public void DeleteGridSelected() => UpdateVisiblitity("Delete");

        abstract public bool ExecuteSubAction();

        public void InsertGridSelected()
        {
            UpdateVisiblitity("Insert");
            ResetInsert();
            CurrentSubAction = "Inserisci";
        }

        public void OtherVMSelected()
        {
            VisibilityDict = new()
            {
                { "Insert", Visibility.Collapsed },
                { "View", Visibility.Collapsed },
                { "Delete", Visibility.Collapsed }
            };
            ResetInsert();
        }

        abstract protected void ResetInsert();

        public void SaveChanges()
        {
            _dbContext.ChangeTracker.DetectChanges();
            _dbContext.SaveChanges();
        }

        public void SetCurrentSubAction(string newSubAction) => CurrentSubAction = newSubAction;

        public void ViewGridSelected() => UpdateVisiblitity("View");

        public MessageBoxResult ShowError()
        {
            if (Error == null)
            {
                return MessageBoxResult.OK;
            }
            string message = Error;
            Error = null;
            return MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public MessageBoxResult ShowSuccess() => MessageBox.Show("Inserimento avvenuto con successo", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
