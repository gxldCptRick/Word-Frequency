using IronOcr;
using IronOcr.Languages;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace FrequencyAnalysisTools.Pages
{
    /// <summary>
    /// Interaction logic for WebCamForOcr.xaml
    /// </summary>
    public partial class WebCamForOcr : Page
    {
        public WebCamForOcr()
        {
            InitializeComponent();
        }

        private void WebCameraControl_Loaded(object sender, RoutedEventArgs e)
        {
            var cameras = this.Camera.GetVideoCaptureDevices().ToList();
            foreach (var camera in cameras)
            {
                MessageBox.Show(camera.ToString());
            }
            this.Camera.StartCapture(cameras[0]);
            //var ocr = new IronOcr.AutoOcr();
            //ocr.Read(new Bitmap(100, 100, System.Drawing.Imaging.PixelFormat.Canonical));
        }

        private void CaptureImage(object sender, RoutedEventArgs e)
        {
            var bitmap = this.Camera.GetCurrentImage();
            var ocr = new AdvancedOcr()
            {
                CleanBackgroundNoise = true,
                EnhanceContrast = true,
                EnhanceResolution = true,
                Strategy = AdvancedOcr.OcrStrategy.Fast,
                ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                DetectWhiteTextOnDarkBackgrounds = false,
                InputImageType = AdvancedOcr.InputTypes.AutoDetect,
                RotateAndStraighten = true,
                ColorDepth = 5
            };
            this.CaptureButton.IsEnabled = false;
            Task.Run(() =>
            {
                var yes = ocr.Read(@"C:\Users\Andres Carrera\Pictures\demo.jpg");
                Dispatcher.Invoke(() =>
                {
                    this.CaptureButton.IsEnabled = true;
                    MessageBox.Show(yes.Text);
                });
            });
            //var yes = ocr.Read(bitmap);
            //MessageBox.Show(yes.Text);
        }

        private void Camera_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.Camera.IsCapturing)
            {
                this.Camera.StopCapture();
            }
        }
    }
}
