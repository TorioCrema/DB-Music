using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfDBMusicLabel.ViewModel
{
    internal interface ISubVM
    {
        public bool ExecuteSubAction();

        public void SetCurrentSubAction(string newSubAction);

        public void InsertGridSelected();

        public void ViewGridSelected();

        public void DeleteGridSelected();

        public void OtherVMSelected();

        public void SaveChanges();

        public MessageBoxResult ShowError();
    }
}
