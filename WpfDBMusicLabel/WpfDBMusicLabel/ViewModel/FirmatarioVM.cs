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
    internal partial class FirmatarioVM : ObservableRecipient, ISubVM
    {
        private readonly MusiclabeldbContext _dbContext;

        [ObservableProperty]
        private string? currentSubAction = null;

        [ObservableProperty]
        private Visibility firmatarioInsertVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private Visibility firmatarioViewVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private Visibility firmatarioDeleteVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private string? error = null;

        [ObservableProperty]
        private Firmatario? currentSelectedFirmatario = null;

        [ObservableProperty]
        private ObservableCollection<Firmatario> firmatari;

        [ObservableProperty]
        private List<string> subActionList = new()
        {
            "Inserisci Firmatario",
            "Inserisci Contratto"
        };

        [ObservableProperty]
        private Firmatario newFirmatario = new()
        {
            Cf = "CODICE FISCALE",
            Nome = "NOME",
            Cognome = "COGNOME",
            IndVia = "VIA",
            IndNumCivico = 0,
            IndCitta = "CITTA",
            Ruolo = "RUOLO"
        };

        [ObservableProperty]
        private DateTime contractStart = DateTime.Now;

        [ObservableProperty]
        private DateTime contractEnd = DateTime.Now;

        [ObservableProperty]
        private uint contractPay = 0;

        public FirmatarioVM(MusiclabeldbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Firmatari.Load();
            Firmatari = _dbContext.Firmatari.Local.ToObservableCollection();
        }

        public bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Inserisci Firmatario":
                    if (NewFirmatario != null && CheckFirmatario(NewFirmatario))
                    {
                        Firmatari.Add(NewFirmatario);
                        return true;
                    }
                    return false;

                case "Inserisci Contratto":
                    if (NewFirmatario != null || ContractPay <= 0)
                    {
                        Contratto newContract = new()
                        {
                            DataInizio = ContractStart,
                            DataFine = ContractEnd,
                            Importo = ContractPay,
                            // TODO get Id of new firmatario
                        };
                        return true;
                    }
                    return false;

                default:
                    Error = "Sub Action not implementetd";
                    return false;
            }
        }

        public void SetCurrentSubAction(string newSubAction) => CurrentSubAction = newSubAction;

        private bool CheckFirmatario(Firmatario firmatario)
        {
            if (firmatario.Cf.Length != 16 || firmatario.Ruolo == "RUOLO")
            {
                return false;
            }
            return true;
        }

        public void ViewGridSelected()
        {
            FirmatarioViewVisibility = Visibility.Visible;
            FirmatarioInsertVisibility = Visibility.Collapsed;
            FirmatarioDeleteVisibility = Visibility.Collapsed;
        }

        public void InsertGridSelected()
        {
            FirmatarioInsertVisibility = Visibility.Visible;
            FirmatarioViewVisibility = Visibility.Collapsed;
            FirmatarioDeleteVisibility = Visibility.Collapsed;
            CurrentSubAction = "Inserisci";
        }

        public void DeleteGridSelected()
        {
            FirmatarioDeleteVisibility = Visibility.Visible;
            FirmatarioViewVisibility = Visibility.Collapsed;
            FirmatarioInsertVisibility = Visibility.Collapsed;
        }

        public void OtherVMSelected()
        {
            FirmatarioViewVisibility = Visibility.Collapsed;
            FirmatarioInsertVisibility = Visibility.Collapsed;
            FirmatarioDeleteVisibility = Visibility.Collapsed;
        }

        public void SaveChanges()
        {
            _dbContext.ChangeTracker.DetectChanges();
            _dbContext.SaveChanges();
        }
    }
}
