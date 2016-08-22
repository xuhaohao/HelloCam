using HelloCam.Models;
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
using System.Windows.Shapes;

namespace HelloCam.Views
{
    /// <summary>
    /// CreateClassWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ClassWindow : Window
    {
        public ClassWindow()
        {
            InitializeComponent();
        }

        private SubGroups subGroup;


        public static void ShowDialog(SubGroups subGroup,Action<SubGroups> action ) {
            ClassWindow createClassWindow = new ClassWindow();
            createClassWindow.subGroup = subGroup;
            if (subGroup.Id == null)
            {
                createClassWindow.subGroup.Id = Guid.NewGuid().ToString();
            }
            createClassWindow.txtName.Text = createClassWindow.subGroup.Name;

            createClassWindow.btnOk.Click += (s, e) =>
            {
                createClassWindow.subGroup.Name = createClassWindow.txtName.Text;
                action(createClassWindow.subGroup);
                createClassWindow.Close();
            };
            createClassWindow.btnCancel.Click += (s, e) =>
            {
                createClassWindow.Close();
            };
            createClassWindow.ShowDialog();
        }
    }
}
