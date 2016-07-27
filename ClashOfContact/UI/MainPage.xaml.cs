using ClashOfContact.Page;
using ClashOfContact.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;
using ClashOfContact.Donnee;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;


namespace ClashOfContact
{

    public sealed partial class MainPage
    {
        static public BitmapImage Arene;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            //Ajoute l'image pas defaut de l'anère
            AddImageDefaut();

            //Change l'apparance du boutton pin/unpin
            ToggleAppBarButton();
        }

        public void AddImageDefaut()
        {
            Uri arenaUri = new Uri("ms-appx:///Assets/arena.png", UriKind.Absolute);
            Arene = new BitmapImage(arenaUri);

        }

        private void Combatant_Click(object sender, RoutedEventArgs e)
        {

            if (!Frame.Navigate(typeof(AjoutCombattant)))
            {
                throw new Exception("Navigation Failed !");
            }


        }

        private void Liste_Click(object sender, RoutedEventArgs e)
        {
            if (!Frame.Navigate(typeof(ListeCombattants)))
            {
                throw new Exception("Navigation Failed !");
            }
        }

        private async void Combat_Click(object sender, RoutedEventArgs e)
        {
            //Verifie si dans la base de données il existe au moins 2 combatants
            db sqlite = new db();
            int nombreCombattants = sqlite.ReadCombattants().Count();
            if (nombreCombattants >= 2)
            {
                //si oui, lance un combat
                if (!Frame.Navigate(typeof(Combat)))
                {
                    throw new Exception("Navigation Failed !");
                }
            }
            else
            {
                //si non, affiche un message indiquant à l'utilisateur qu'il doit ajouter 2 combatants
                MessageDialog msg = new MessageDialog("Veuillez ajouter au moins 2 contacts avant de lancer un combat.", "Attention !");
                await msg.ShowAsync();
            }
        }

        private void ToggleAppBarButton()
        {
            
            if (!SecondaryTile.Exists("ClashOfContacts"))
            {
                //Si le secondary tile n'existe pas, affiche "Pin" ainsi que le symbole correspondant
                this.PinUnpinButton.Label = "Pin";
                this.PinUnpinButton.Icon = new SymbolIcon(Symbol.Pin);
            }
            else
            {
                //Si le secondary tile existe, affiche "Unpin" ainsi que le symbole correspondant
                this.PinUnpinButton.Label = "Unpin";
                this.PinUnpinButton.Icon = new SymbolIcon(Symbol.UnPin);
            }
            //Met à jour l'interface
            this.PinUnpinButton.UpdateLayout();
        }

        //Methode appeler lorsque l'on clic sur le "Pin/Unpin"
        private async void PinUnpinButton_Click(object sender, RoutedEventArgs e)
        {
            //Si le secondary tile existe, l'efface
            if (SecondaryTile.Exists("ClashOfContacts"))
            {
                var tile = new SecondaryTile("ClashOfContacts");
                await tile.RequestDeleteAsync();
            }
            else // Sinon, le crée
            {
                //Recupère le logo de l'application
                var uriLogo = new Uri("ms-appx:///Assets/arena.png");
                //Préparation du secondary tile
                var tile = new SecondaryTile
                {
                    TileId = "ClashOfContacts",
                    DisplayName = "Clash of contacts",
                    Arguments = "Clash of contact",
                    RoamingEnabled = false

                };
                //Affiche le nom de l'application sur le secondary tile
                tile.VisualElements.ShowNameOnSquare150x150Logo = true;
                //Affiche le logo sur le secondary tile
                tile.VisualElements.Square150x150Logo = uriLogo;

                //Creation du secondary tile
                await tile.RequestCreateAsync();
            }
            //Change l'apparance du boutton pin/unpin
            ToggleAppBarButton();
        }

        
    }
}
