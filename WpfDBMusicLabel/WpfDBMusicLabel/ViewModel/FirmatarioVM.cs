﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        private string? error = null;

        [ObservableProperty]
        private ObservableCollection<Firmatario> firmatari;

        [ObservableProperty]
        private List<string> subActionList = new()
        {
            "Inserisci"
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

        public FirmatarioVM(MusiclabeldbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Firmatarios.Load();
            Firmatari = _dbContext.Firmatarios.Local.ToObservableCollection();
        }

        public bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Inserisci":
                    if (NewFirmatario != null && CheckFirmatario(NewFirmatario))
                    {
                        Firmatari.Add(NewFirmatario);
                    }
                    return false;

                default:
                    Error = "Sub Action not implementetd";
                    return false;
            }
        }

        public void InsertGridSelected()
        {
            FirmatarioInsertVisibility = Visibility.Visible;
            CurrentSubAction = "Inserisci";
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

        public void OtherVMSelected() => FirmatarioInsertVisibility = Visibility.Collapsed;
    }
}