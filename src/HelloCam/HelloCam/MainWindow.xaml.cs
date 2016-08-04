using HelloCam.Commons;
using System;
using System.Collections.Generic;
using System.IO;
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
using WPFMediaKit.DirectShow.Controls;

namespace HelloCam
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var a = Dbs.ParamsCache;

            string[] inputNames = MultimediaUtil.VideoInputNames;
            m_VideoCaptureElement.VideoCaptureSource = inputNames[0];
        }
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(MainWindow));

        public FaceService faceService = new FaceService(Constants.FACE_KEY, Constants.FACE_SECRET);

        private string fileName = "";

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //m_VideoCaptureElement.
            //RenderTargetBitmap bmp = new RenderTargetBitmap((int)m_VideoCaptureElement.ActualWidth, (int)m_VideoCaptureElement.ActualHeight, 96, 96, PixelFormats.Rgb24);
            //bmp.Render(m_VideoCaptureElement);

            //BitmapEncoder encoder = new JpegBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(bmp));
            //string now = DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "" + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second;
            //string filename = "E:\\" + now + "pic.jpg";
            //FileStream fstream = new FileStream(filename, FileMode.Create);
            //encoder.Save(fstream);
            //fstream.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)m_VideoCaptureElement.ActualWidth, (int)m_VideoCaptureElement.ActualHeight, 96, 96, PixelFormats.Default);
            //m_VideoCaptureElement.Measure(m_VideoCaptureElement.RenderSize);
            //m_VideoCaptureElement.Arrange(new Rect(m_VideoCaptureElement.RenderSize));
            bmp.Render(m_VideoCaptureElement);
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));           using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                byte[] captureData = ms.ToArray();
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                File.WriteAllBytes(fileName, captureData);
            }
            string path = System.AppDomain.CurrentDomain.BaseDirectory + fileName;
            if (System.IO.File.Exists(path))
            {
                img.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
                MessageBox.Show("照片保存在Debug目录下photos文件夹内。");
                img.Source = null;
            }


        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var filePath = @"C:\Users\hh\Desktop\20160731233904.jpg";
            //var result = faceService.Detection_DetectImg(@"C:\Users\hh\Desktop\20160731233904.jpg");
            //var a = result.face;

            //var result = faceService.Recognition_IdentifyImgById("group_id", filePath);
            var id = Guid.NewGuid().ToString();
            var result = faceService.Group_CreateByIdList(id);


            var a = result;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Dbs.Init();
        }
    }
}
