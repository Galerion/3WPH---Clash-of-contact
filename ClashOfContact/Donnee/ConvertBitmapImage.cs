using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace ClashOfContact.Donnee
{
    class ConvertBitmapImage
    {
        //Permet de convertir un string en base 64 en BitmapImage
        public static BitmapImage StringToBitmap(string source)
        {
            if (source != null)
            {
                var ims = new InMemoryRandomAccessStream();
                var bytes = Convert.FromBase64String(source);
                var dataWriter = new DataWriter(ims);
                dataWriter.WriteBytes(bytes);
                dataWriter.StoreAsync();
                ims.Seek(0);
                var img = new BitmapImage();
                img.SetSource(ims);
                return img;
            }
            else
            {
                return null;
            }
        }
        //Permet de convertir un BitmapImage en base 64 pour la stocker en base de données
        public static async Task<string> ImageToBase64(StorageFile MyImageFile)
        {
            Stream ms = await MyImageFile.OpenStreamForReadAsync();
            byte[] imageBytes = new byte[(int)ms.Length];
            ms.Read(imageBytes, 0, (int)ms.Length);
            return Convert.ToBase64String(imageBytes);
        }
    }
}
