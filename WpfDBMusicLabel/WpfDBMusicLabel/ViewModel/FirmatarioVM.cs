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
    internal partial class FirmatarioVM : AbstractVM
    {
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

        public FirmatarioVM()
        {
            _dbContext = new();
            _dbContext.Firmatari.Load();
            Firmatari = _dbContext.Firmatari.Local.ToObservableCollection();
        }

        override public bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Inserisci Firmatario":
                    if (NewFirmatario != null && CheckFirmatario())
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

        private bool CheckFirmatario()
        {
            if (NewFirmatario.Cf.Length != 16 || NewFirmatario.Ruolo == "RUOLO")
            {
                return false;
            }
            return true;
        }

        new public void InsertGridSelected()
        {
            base.InsertGridSelected();
            CurrentSubAction = "Inserisci";
        }

        protected override void ResetInsert()
        {
            NewFirmatario = new()
            {
                Cf = "CODICE FISCALE",
                Nome = "NOME",
                Cognome = "COGNOME",
                IndVia = "VIA",
                IndNumCivico = 0,
                IndCitta = "CITTA",
                Ruolo = "RUOLO"
            };
            ContractEnd = DateTime.Now;
            ContractStart = DateTime.Now;
            ContractPay = 0;
        }
    }
}
