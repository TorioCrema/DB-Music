using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        private List<Merchandising> merch;

        [ObservableProperty]
        private List<ProgettoMusicale> projects;

        [ObservableProperty]
        private ProgettoMusicale currentSelectedProject;

        [ObservableProperty]
        private List<Produttore> producers;

        [ObservableProperty]
        private Produttore produttore;

        [ObservableProperty]
        private Merchandising newMerch = new();

        [ObservableProperty]
        private List<string> subActionList = new()
        {
            "Vedi merch"
        };

        public MerchVM()
        {
            _dbContext.ProgettiMusicali.Load();
            _dbContext.Produttori.Load();
            Projects = _dbContext.ProgettiMusicali.Local.ToList();
            Producers = _dbContext.Produttori.Local.ToList();
            Produttore = Producers.First();
            CurrentSelectedProject = Projects.First();
        }

        public override bool ExecuteSubAction()
        {
            switch (CurrentSubAction)
            {
                case "Vedi merch":
                    if (CurrentSelectedProject != null)
                    {
                        _dbContext.Entry(CurrentSelectedProject).Collection(p => p.Merchandisings).Load();
                        Merch = CurrentSelectedProject.Merchandisings.ToList();
                        return true;
                    }
                    Error = "Progetto musicale non selezionato";
                    return false;

                case "Inserisci":
                    NewMerch.IdProduttoreNavigation = Produttore;
                    NewMerch.IdProgettoNavigation = CurrentSelectedProject;
                    _dbContext.Merchandisings.Local.Add(NewMerch);
                    Produttore.NumForniture++;
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
            Produttore = Producers.First();
            CurrentSelectedProject = Projects.First();
        }

        [RelayCommand]
        private void SubAction() => ExecuteSubAction();

        [RelayCommand]
        private void Confirm() => ExecuteSubAction();
    }
}
