using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDBMusicLabel.Model;

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
    }
}
