using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfDBMusicLabel.Model
{
    public class InputChecker
    {
        private readonly Regex _regex;

        public InputChecker(Regex regex)
        {
            _regex = regex;
        }

        private bool IsTextAllowed(string text) => !_regex.IsMatch(text);

        public void Check(object sender, TextCompositionEventArgs e) => e.Handled = !IsTextAllowed(e.Text);
    }
}
