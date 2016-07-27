using ClashOfContact.Donnee;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace ClashOfContact.UI
{
    public sealed partial class Combat 
    {
        Combattant rndAd;
        int id;
        Combattant joueur;
        Combattant ad;
        int vie;
        int vieAd;
        int degat;
        int degatAd;
        Accelerometer accelerometer;
        AccelerometerReading reading;
        Double readX;
        Double readY;
        Double readZ;
        Double tempX;
        Double tempY;
        Double tempZ;
        public static string resultat;
        public Combat()
        {
            this.InitializeComponent();

            ApplicationDataContainer localSet = ApplicationData.Current.LocalSettings;
            if (localSet.Values.ContainsKey("IdJoueur"))
            {
                //Recupère le combattant choisis par l'utilisateur
                id = (int)localSet.Values["IdJoueur"];
            }
            else
            {
                //Si aucun n'est choisis, prend le premier par defaut
                id = 1;
            }
            
            db sqlite = new db();
            //Recupère le nombre de combattants (+1 pour la fonction aléatoire)
            int nombreCombattants = sqlite.ReadCombattants().Count() + 1;
            Random rnd = new Random();

            //Determine un autre combattant tant que celui ci est le même que celui choisis par le combatant 
            do
            {
                rndAd = sqlite.ReadCombattant(rnd.Next(1, nombreCombattants));
            } while (id == rndAd.Id);
            
            //Recupère les combattants respectifs
            joueur = sqlite.ReadCombattant(id);
            ad = sqlite.ReadCombattant(rndAd.Id);
            
            //Assigne la vie des combattants respectifs
            vieAd = ad.Vie;
            vie = joueur.Vie;

            //Affiche les données des deux combattants sur l'écran
            NomAd.Text = ad.NomEntier;
            VieAd.Text = vieAd + "/" + ad.Vie;
            ImageAd.Source = ConvertBitmapImage.StringToBitmap(ad.Image);
            Nom.Text = joueur.NomEntier;
            Vie.Text = vie + "/" + joueur.Vie;
            Image.Source = ConvertBitmapImage.StringToBitmap(joueur.Image);

            //Initialise l'accelerometre
            SetupAccel();
        }

        private void SetupAccel()
        {
            accelerometer = Accelerometer.GetDefault();
            //Definie un interval minimum entre deux secousse du téléphone
            accelerometer.ReportInterval = 350;
            //Recupère la position initial du téléphone
            GetCurrentPosition();
            //Ajoute la méthode quand les valeurs de l'accelerometre changent
            accelerometer.ReadingChanged += OnSensorReadingChanged;
        }

        private void GetCurrentPosition()
        {
            reading = accelerometer.GetCurrentReading();
            tempX = reading.AccelerationX;
            tempY = reading.AccelerationY;
            tempZ = reading.AccelerationZ;
        }
        private async void OnSensorReadingChanged(Object sender, AccelerometerReadingChangedEventArgs e)
        {
            reading = accelerometer.GetCurrentReading();
            //Récupère la position du téléphone
            readX = reading.AccelerationX;
            readY = reading.AccelerationY;
            readZ = reading.AccelerationZ;
            //|| readY > tempY + 0.4 || readY < tempY + 0.4 || readZ > tempZ + 0.4 || readZ < tempZ -0.4
            //Si le téléphone a bougé
            if (readX > tempX + 0.3 || readX < tempX - 0.3)
            {

                //Permet de faire les modification sur l'écran
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    DealDamage();
                });

                
                //Met la position du téléphone en temporaire
                tempX = readX; tempY = readY; tempZ = readZ;
            }
        }

        private void DealDamage()
        {
            Random rnd = new Random();

            //Determine les dégats infligés par les combattants
            degatAd = ad.Force + rnd.Next(0, 11);
            degat = joueur.Force + rnd.Next(0, 11);
            //Inflige les dégats
            vie = vie - degatAd;
            vieAd = vieAd - degat;

            //Met à jour l'interface
            UpdateUI();

            //Si l'un des deux combattants est mort
            if (vie <= 0 || vieAd <= 0)
            {
                accelerometer.ReadingChanged -= OnSensorReadingChanged;
                //Si l'utilisateur est mort
                if (vie <= 0)
                {
                    resultat = "Defaite !";
                }
                //Si l'adversaire est mort
                else if (vieAd <= 0)
                {
                    resultat = "Victoire !";
                }
                //Envoi sur la page de résultat
                ResultPage();
            }
        }

        private void UpdateUI()
        {
            //Modifie l'affichage de la vie des deux combattants
            Vie.Text = vie + "/" + joueur.Vie;
            VieAd.Text = vieAd + "/" + ad.Vie;
            //Affiche les dégats infligés au millieu de l'écran
            Degat.Text = "Vous avez infligé " + degat + " points de dégat !";
            DegatAd.Text = ad.NomEntier + " vous inflige " + degatAd + " points de dégat !";

        }

        private void ResultPage()
        {
            if (!Frame.Navigate(typeof(Resultat)))
            {
                throw new Exception("Navigation Failed !");
            }
        }
        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            if (!Frame.Navigate(typeof(MainPage)))
            {
                throw new Exception("Navigation Failed !");
            }
        }
    }
}
