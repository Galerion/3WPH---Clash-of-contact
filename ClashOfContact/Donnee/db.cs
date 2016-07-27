using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ClashOfContact.Donnee
{
    class db
    {
        SQLiteConnection dbConn;
        //Le chemin de l'emplacement du fichier sqlite
        public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "ClashOfContact.sqlite"));

        //Création de la table
        public bool onCreate(string DB_PATH)
        {
            try
            {
                if (!CheckFileExists(DB_PATH).Result)
                {
                    using (dbConn = new SQLiteConnection(DB_PATH))
                    {
                        dbConn.CreateTable<Combattant>();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Verification si la base de données existe déjà
        public async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Methode pour ajouter un combattant
        public void Insert(Combattant combattant)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Insert(combattant);
                });
            }
        }

        //Methode qui retourne tous les combattants
        public ObservableCollection<Combattant> ReadCombattants()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<Combattant> myCollection = dbConn.Table<Combattant>().ToList<Combattant>();
                ObservableCollection<Combattant> ListeCombattant = new ObservableCollection<Combattant>(myCollection);
                return ListeCombattant;
            }
        }

        //Methode qui retourne un combattant spécifique
        public Combattant ReadCombattant(int id)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingconact = dbConn.Query<Combattant>("select * from Combattant where Id =" + id).FirstOrDefault();
                return existingconact;
            }
        }
        //Methode qui supprime et recréer la base de données
        public void DeleteAllCombattant()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.DropTable<Combattant>();
                dbConn.CreateTable<Combattant>();
                dbConn.Dispose();
                dbConn.Close(); 
            }
        } 
    }
}
