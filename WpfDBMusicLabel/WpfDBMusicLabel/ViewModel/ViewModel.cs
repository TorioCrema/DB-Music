using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDBMusicLabel.Model;

namespace WpfDBMusicLabel.ViewModel
{
    public class ViewModel : ObservableRecipient
    {
        private ActionTypeList actionsList = new ActionTypeList();

        public ViewModel() 
        {
            this.actionsList.Actions = new List<ActionType>
            {
                ActionType.Tour,
                ActionType.Album,
                ActionType.Firmatario,
                ActionType.Traccia,
                ActionType.Merchandising,
                ActionType.Prodotto,
                ActionType.ProgettoMusicale
            };
        }
    }
}
