using ClashOfContact.Donnee;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace ClashOfContact
{

    public sealed partial class ListeCombattants 
    {

        public ListeCombattants()
        {
            this.InitializeComponent();
            
            //Récupère la liste des combattants pour l'afficher
            DataContext = GetListe();

        }

        public ObservableCollection<Combattant> GetListe()
        {
            
            db sqlite = new db();
            //Requete sur la base de données retournant tous les combattants sous forme d'obserbableCollection<Combattant>
            var combattants = sqlite.ReadCombattants();
            return combattants;
        }
        
        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            if (!Frame.Navigate(typeof(MainPage)))
            {
                throw new Exception("Navigation Failed !");
            }
        }


        private void Choisir_Click(object sender, RoutedEventArgs e)
        {
            //Récupère le bouton sur lequel l'utilisateur a cliquer
            Button bouton = (Button)sender;
            //Récupère le combattant choisis qui est stocké dans le CommandParameter du bouton
            Combattant param = (Combattant) bouton.CommandParameter;

            //Stock l'id du combattant en local
            ApplicationDataContainer localSet = ApplicationData.Current.LocalSettings;
            localSet.Values["IdJoueur"] = param.Id;
            if (!Frame.Navigate(typeof(MainPage)))
            {
                throw new Exception("Navigation Failed !");
            }
        }
    }
}
