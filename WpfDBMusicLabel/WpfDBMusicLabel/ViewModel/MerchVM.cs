using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDBMusicLabel.musiclabeldb;

namespace WpfDBMusicLabel.ViewModel
{
    partial class MerchVM : AbstractVM
    {
        [ObservableProperty]
        private List<ProgettoMusicale> projects;

        [ObservableProperty]
        private ProgettoMusicale selectedProj;

        [ObservableProperty]
        private List<Produttore> producers;

        [ObservableProperty]
        private Produttore selectedProd;

        [ObservableProperty]
        private Merchandising newMerch = new();

        public MerchVM()
        {
            _dbContext.ProgettiMusicali.Load();
            _dbContext.Produttori.Load();
            Projects = _dbContext.ProgettiMusicali.Local.ToList();
            Producers = _dbContext.Produttori.Local.ToList();
            SelectedProd = Producers.First();
            selectedProj = Projects.First();
        }

        public override bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Inserisci":
                    NewMerch.IdProduttoreNavigation = SelectedProd;
                    NewMerch.IdProgettoNavigation = SelectedProj;
                    _dbContext.Merchandisings.Local.Add(NewMerch);
                    SaveChanges();
                    ResetInsert();
                    ShowSuccess();
                    return true;
                default:
                    Error = "Azione non implementata";
                    ShowError();
                    return false;
            }
        }

        protected override void ResetInsert()
        {
            NewMerch = new();
            SelectedProd = Producers.First();
            SelectedProj = Projects.First();
        }
    }
}
