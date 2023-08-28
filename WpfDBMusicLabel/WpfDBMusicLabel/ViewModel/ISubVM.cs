using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDBMusicLabel.ViewModel
{
    internal interface ISubVM
    {
        public bool ExecuteSubAction();

        public void SetCurrentSubAction(string newSubAction);
    }
}
