using HelloCam.Commons;
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
        }
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(MainWindow));

        public FaceService faceService = new FaceService(Constants.FACE_KEY, Constants.FACE_SECRET);


    }
}
