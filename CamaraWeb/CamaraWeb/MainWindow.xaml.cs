using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace CamaraWeb
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Boolean existeFoto = false;
        KinectSensor miKinect; //instancia de la clase KinectSensor

        public MainWindow()
        {
            InitializeComponent();
        }

        private void windows(object sender, RoutedEventArgs e) //metodo de la ventana de inicio
        {
            miKinect = KinectSensor.KinectSensors.FirstOrDefault();  //usa solo el primer kinect
            //miKinect = KinectSensor.KinectSensors[0] --->array es para manejo del kinect que esta en la posicion n --> manejo de varios kinects

            miKinect.Start();
            miKinect.ColorStream.Enable(); //inicio de señal de la camara RGB
            miKinect.ColorFrameReady += MiKinect_ColorFrameReady;
        }

        private void MiKinect_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using(ColorImageFrame frameImagen = e.OpenColorImageFrame()) //en frameImagen se almacenara la señal que se recibe de la camara RGB
            { //con la llave de cierre del using el objeto se elimina de memoria
                if (frameImagen == null)
                {
                    return;
                } //con esto se verifica que no se reciba un valor nulo

                byte[] datosColor = new byte[frameImagen.PixelDataLength]; //extrae el tamaño del buffer
                //se almacena en un array de tipo byte para usar el RGB
                frameImagen.CopyPixelDataTo(datosColor);

                mostrarVideo.Source = BitmapSource.Create( //mapa de bits
                    frameImagen.Width,                     //ancho de imagen 
                    frameImagen.Height,                    //alto de la imagen
                    96,                                    // dpi horizontales
                    106,                                    // dpi vertical
                    PixelFormats.Bgr32,                    //formato de los pixeles
                    null,                                   // paleta del bitmap
                    datosColor,                             //contenido de la imagen
                    frameImagen.Width * frameImagen.BytesPerPixel   //sprite
                    );
                while (existeFoto)
                {
                    existeFoto = existeFoto;
                }
            }
        }

        private void btnCaptura_Click(object sender, RoutedEventArgs e)
        {
            existeFoto = true;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            existeFoto = false;
        }
    }
}
