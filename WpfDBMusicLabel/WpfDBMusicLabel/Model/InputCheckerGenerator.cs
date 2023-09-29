using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDBMusicLabel.Model
{
    public class InputCheckerGenerator
    {
        static public InputChecker IntChecker() => new(new("[^0-9]+"));
    }
}
