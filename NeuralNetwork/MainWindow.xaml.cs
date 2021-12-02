using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NeuralNetwork.NeuralNetworkComponents;

namespace NeuralNetwork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string _filepath;
        private NeuralNet v;
        //BitmapImage img;
        Bitmap img;
        List<System.Drawing.Color> pixelsColor;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();

            var result = fd.ShowDialog();
            if (result == true)
            {
                _filepath = fd.FileName;
                img = new Bitmap(_filepath);

                //var f = BitmapToColorsArray(img);
                var monochromaticImage = ColorImageToMonochromatic(img);
                var list = MonochromaticImageToBinaryList(monochromaticImage);
            }
        
        }

        private List<System.Drawing.Color> BitmapToColorsArray(Bitmap bitmap)
        {
            
            var pixelsColor = new List<System.Drawing.Color>();
            for(int i = 0; i < bitmap.Height; ++i)
            {
                for(int j = 0; j < bitmap.Width; ++j)
                {
                    pixelsColor.Add(bitmap.GetPixel(j, i));
                    
                }
            }
            return pixelsColor;
        }

        private Bitmap ColorImageToMonochromatic(Bitmap img)//
        {
            var image = img;
            byte R, G, B;
            for (int i = 0; i < image.Height; ++i)
            {
                for (int j = 0; j < image.Width; ++j)
                {
                    var px = img.GetPixel(j, i);
                    R = G = B = (byte)((px.R + px.G + px.B) / 3);
                    
                    var color = System.Drawing.Color.FromArgb(1, R, G, B);
                    image.SetPixel(j, i, color);
                   
                }
            }

            return image;
        }

        //monochromatic image on input
        private List<int> MonochromaticImageToBinaryList(Bitmap img)
        {
            var binaryList = new List<int>();

            for (int i = 0; i < img.Height; ++i)
            {
                for (int j = 0; j < img.Width; ++j)
                {
                    var px = img.GetPixel(j, i);
                    double x = (double)px.R / 255;
                    x = Math.Round(x, 0);
                    binaryList.Add((int)x);

                    

                }
            }

            return binaryList;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
