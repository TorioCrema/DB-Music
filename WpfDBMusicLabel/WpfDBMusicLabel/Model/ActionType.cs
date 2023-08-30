using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfDBMusicLabel.Model
{
    public class ActionType
    {
        public static ActionType Tour { get; } = new ActionType("Tour");
        public static ActionType Firmatario { get; } = new ActionType("Firmatario");
        public static ActionType Traccia { get; } = new ActionType("Tracce");
        public static ActionType Album { get; } = new ActionType("Album");
        public static ActionType Prodotto { get; } = new ActionType("Prodotto");
        public static ActionType Merchandising { get; } = new ActionType("Merchandising");
        public static ActionType ProgettoMusicale { get; } = new ActionType("ProgettoMusicale");

        public string Name { get; }
        private ActionType(string str) 
        {
            Name = str;
        }
    }
}
