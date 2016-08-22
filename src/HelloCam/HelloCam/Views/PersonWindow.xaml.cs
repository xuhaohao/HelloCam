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
    /// PersonWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PersonWindow : Window
    {
        public PersonWindow()
        {
            InitializeComponent();
        }

        private Persons person;


        public static void ShowDialog(Persons person, Action<Persons> action)
        {
            PersonWindow personWindow = new PersonWindow();
            personWindow.person = person;
            if (person.Id == null)
            {
                personWindow.person.Id = Guid.NewGuid().ToString();
            }
            personWindow.txtName.Text = personWindow.person.Name;

            personWindow.btnOk.Click += (s, e) =>
            {
                personWindow.person.Name = personWindow.txtName.Text;
                action(personWindow.person);
                personWindow.Close();
            };
            personWindow.btnCancel.Click += (s, e) =>
            {
                personWindow.Close();
            };
            personWindow.ShowDialog();
        }
    }
}
