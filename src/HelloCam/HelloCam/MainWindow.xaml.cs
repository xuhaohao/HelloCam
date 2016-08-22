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

        private string fileName = "";

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)m_VideoCaptureElement.ActualWidth, (int)m_VideoCaptureElement.ActualHeight, 96, 96, PixelFormats.Default);
            bmp.Render(m_VideoCaptureElement);
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));           using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                byte[] captureData = ms.ToArray();
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                File.WriteAllBytes(fileName, captureData);
            }
            string path = AppDomain.CurrentDomain.BaseDirectory + fileName;
            if (File.Exists(path))
            {
                img.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
                MessageBox.Show("照片保存在Debug目录下photos文件夹内。");
                img.Source = null;
            }


        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            FaceService faceService = new FaceService(Constants.FACE_KEY, Constants.FACE_SECRET);

            var filePath = @"C:\Users\hh\Desktop\20160731233904.jpg";
            //dynamic result = faceService.Detection_DetectImg(@"C:\Users\hh\Desktop\20160731233904.jpg");
            //var a = result.face;

            //if (result.face != null && result.face.Count > 0) {
            //    var id = Guid.NewGuid().ToString();
            //    result = faceService.Person_Create(id, id, id, Constants.Group_Id, Constants.Group_Name);
            //}
            //var result = faceService.Recognition_IdentifyImgById("group_id", filePath);




            //var a = result;
            var id = Guid.NewGuid().ToString();
            //var result = faceService.Group_CreateByIdList(id);
            //var result = faceService.Group_GetInfoById(Constants.Group_Id);
            //var result = faceService.Group_AddPersonByGroupIdPersonId(Constants.Group_Id, id);
            //var result = faceService.Group_AddPersonByGroupIdPersonId(Constants.Group_Id,)
            //var result = faceService.Person_Create(id, "c34e695176d5956a43443321170dec9e", id, Constants.Group_Id, Constants.Group_Name);
            dynamic r = result;
            
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Dbs.Init();
        }

        private void btnCheckin_Click(object sender, RoutedEventArgs e)
        {
            Dbs.CheckInLog("13131", "131313", 0);
            MessageBox.Show("提交考勤");
        }
    }
}
