using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDBMusicLabel.Model;
using WpfDBMusicLabel.musiclabeldb;

namespace WpfDBMusicLabel.ViewModel
{
    partial class VModel : ObservableRecipient
    {
        [ObservableProperty]
        private List<string> actionsList = new()
        {
            "Tour",
            "Album",
            "Firmatario",
            "Traccia",
            "Merchandising",
            "Prodotto",
            "Progetto Musicale"
        };

        private MusiclabeldbContext _dbContext = new MusiclabeldbContext();

        [RelayCommand]
        private void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public VModel()
        {
             _dbContext.ProgettoMusicales.Load();
            Progetti = _dbContext.ProgettoMusicales.Local.ToObservableCollection();
        }

        [ObservableProperty]
        private ObservableCollection<ProgettoMusicale> progetti;
    }
}
