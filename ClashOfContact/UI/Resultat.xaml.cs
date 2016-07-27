using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ClashOfContact.UI
{
    
    public sealed partial class Resultat 
    {
        public Resultat()
        {
            this.InitializeComponent();
            //Affiche si l'utilisateur a gagné ou perdu
            resultat.Text = Combat.resultat;
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            if (!Frame.Navigate(typeof(MainPage)))
            {
                throw new Exception("Navigation Failed !");
            }
        }

        private void Rejouer_Click(object sender, RoutedEventArgs e)
        {
            if (!Frame.Navigate(typeof(Combat)))
            {
                throw new Exception("Navigation Failed !");
            }
        }
    }
}
