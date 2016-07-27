using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace ClashOfContact
{
    [SQLite.Table("Combattant")]
    public class Combattant
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]  
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Force { get; set; }
        public int Vie { get; set; }
        public string Image { get; set; }
        public string Statistique { get { return "Vie : " + Vie + " - Force : " + Force; } }
        public string NomEntier { get { return Nom + " " + Prenom; } }

        public Combattant() { }
    }

    
}

