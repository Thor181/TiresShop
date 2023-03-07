using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

/// <summary>
/// Работа с изображением
/// </summary>
internal class ImageWorker
{
    internal static byte[] ConvertImageToByteArray(string fileImage)
    {
        byte[] imageData;
        using (System.IO.FileStream fs = new System.IO.FileStream(fileImage, System.IO.FileMode.Open, System.IO.FileAccess.Read))
        {
            imageData = new byte[fs.Length];
            fs.Read(imageData, 0, imageData.Length);
        }
        return imageData;
    }
    /// <summary>
    /// Загружает изображение в ControlImage.Source
    /// </summary>
    /// <param name="imageByteArray"></param>
    /// <returns>out byte[]</returns>
    internal static BitmapImage LoadImage(out byte[] imageByteArray)
    {
        OpenFileDialog loadPhoto = new OpenFileDialog();
        loadPhoto.Filter = "Файлы изображений|*.bmp;*.jpg;*.gif;*.png;*.tif|Все файлы|*.*";
        if ((bool)loadPhoto.ShowDialog()!)
        {
            var image = new BitmapImage(new Uri(loadPhoto.FileName));
            imageByteArray = ImageWorker.ConvertImageToByteArray(loadPhoto.FileName);
            return image;
        }
        imageByteArray = new byte[0];
        return new BitmapImage();
    }

    internal static ImageSource ByteArrayToImageSource(byte[] sourceArray)
    {
        BitmapImage bitmapImage = new();
        using (MemoryStream ms = new MemoryStream(sourceArray))
        {
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();
        }
        return bitmapImage;
    }
}

