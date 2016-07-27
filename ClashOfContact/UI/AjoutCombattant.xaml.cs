using ClashOfContact.Donnee;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Contacts;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace ClashOfContact.Page
{
    
    public sealed partial class AjoutCombattant 
    {
        CoreApplicationView view;
        Contact c;
        string imageBase64;

        public AjoutCombattant()
        {
            this.InitializeComponent();
            view = CoreApplication.GetCurrentView();
        }

        private async void Contact_Click(object sender, RoutedEventArgs e)
        {
            //Affiche le contact picker
            ContactPicker cp = new ContactPicker();
            cp.DesiredFieldsWithContactFieldType.Add(ContactFieldType.PhoneNumber);
            //Récupère le contact choisis
            c = await cp.PickContactAsync();
            if (c != null)
            {
                //Prévisualise le contact choisis
                txtContact.Text = c.FirstName + " " + c.LastName;
            }
            
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
             //Ouvre le File picker dans la galerie de photos
             FileOpenPicker filePicker = new FileOpenPicker();
             filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
             filePicker.ViewMode = PickerViewMode.Thumbnail;

             //Ajotue des filtre des extentions d'image acceptées
             filePicker.FileTypeFilter.Clear();
             filePicker.FileTypeFilter.Add(".bmp");
             filePicker.FileTypeFilter.Add(".png");
             filePicker.FileTypeFilter.Add(".jpeg");
             filePicker.FileTypeFilter.Add(".jpg");
    
             filePicker.PickSingleFileAndContinue();
            
            view.Activated += viewActivated; 

        }
        private void viewActivated(CoreApplicationView sender, IActivatedEventArgs args1)
        {   
            FileOpenPickerContinuationEventArgs args = args1 as FileOpenPickerContinuationEventArgs;

            //Si l'utilisateur à choisis une image
            if (args != null)
            {
                if (args.Files.Count == 0) return;

                view.Activated -= viewActivated;
                //Récupère l'image chosisis
                StorageFile storageFile = args.Files[0];
                //Convertit l'image en base 64 pour la stocker dans la base de données
                imageBase64 = ConvertBitmapImage.ImageToBase64(storageFile).Result;
                //Convertit la base 64 de l'image en BitmapImage pour l'afficher la prévisualisation
                BitmapImage bitmap = ConvertBitmapImage.StringToBitmap(imageBase64);
               
                //Affiche l'image
                img.Source = bitmap;

            }
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            if (!Frame.Navigate(typeof(MainPage)))
            {
                throw new Exception("Navigation Failed !");
            }
        }

        private async void Valider_Click(object sender, RoutedEventArgs e)
        {
            //Si l'utilisateur a choisis un contact et une image
            if (c != null && imageBase64 != null)
            {
                //Création du combattant
                Combattant combattant = new Combattant();
                combattant.Nom = c.LastName;
                combattant.Prenom = c.FirstName;
                combattant.Image = imageBase64;
                Random rnd = new Random();
                combattant.Vie = rnd.Next(50, 201);
                combattant.Force = rnd.Next(1, 21);

                //Ajoute le combattant dans la base de données
                db sqlite = new db();
                sqlite.Insert(combattant);
                
                if (!Frame.Navigate(typeof(MainPage)))
                {
                    throw new Exception("Navigation Failed !");
                }
            }
            else //Si l'utilisateur n'a pas choisis de contact et d'image
            {
                MessageDialog msgbox = new MessageDialog("Veulliez ajouter un contact et une image.");
                await msgbox.ShowAsync();  
            }
            
            
        }
    }
}
