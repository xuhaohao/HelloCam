using HelloCam.Commons;
using HelloCam.Models;
using System.Collections.ObjectModel;
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

namespace HelloCam.Views
{
    /// <summary>
    /// ConfigTabView.xaml 的交互逻辑
    /// </summary>
    public partial class SubGroupsListView : UserControl
    {
        public SubGroupsListView()
        {
            InitializeComponent();

            var subGroupList = Dbs.GetAll<SubGroups>(null);
            foreach (var item in subGroupList)
            {
                subGroups.Add(item);
            }
            lbClasses.ItemsSource = subGroups;
        }

        private ObservableCollection<SubGroups> subGroups = new ObservableCollection<SubGroups>();

        private ObservableCollection<Persons> persons = new ObservableCollection<Persons>();

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (lbPersons.Visibility == Visibility.Visible)
            {
                //创建人
                var subgroup = btnCreate.Tag as SubGroups;
                if (subgroup != null) {
                    var person = new Persons() { SubGroupId = subgroup.Id };
                    PersonWindow.ShowDialog(person, r => {
                        FaceService faceService = new FaceService(Constants.FACE_KEY, Constants.FACE_SECRET);
                        var result = faceService.Group_AddPersonByGroupIdPersonId(Constants.Group_Id, r.Id);
                        if (result.success) {
                            Dbs.Insert(r);
                            persons.Add(r);
                        }
                    });
                }
            }
            else
            {
                //创建班级
                ClassWindow.ShowDialog(new SubGroups(),subGroup => {
                    Dbs.Insert(subGroup);
                    subGroups.Add(subGroup);
                });
            }
        }

        private void btnClassDelete_Click(object sender, RoutedEventArgs e)
        {
            var subGroup = (sender as Button).DataContext as SubGroups;
            if (subGroup != null) {
                Dbs.Delete(subGroup);
                subGroups.Remove(subGroup);
            }
        }

        private void btnClassManagement_Click(object sender, RoutedEventArgs e)
        {
            var subGroup = (sender as Button).DataContext as SubGroups;
            if (subGroup != null)
            {
                btnCreate.Tag = subGroup;
                var personList = Dbs.GetAll<Persons>(string.Format("SubGroupId = '{0}'", subGroup.Id));
                persons.Clear();
                foreach (var item in personList)
                {
                    persons.Add(item);
                }
                lbPersons.ItemsSource = persons;

                lbClasses.Visibility = Visibility.Collapsed;
                lbPersons.Visibility = Visibility.Visible;
            }
            
        }

        private void btnPersonDelete_Click(object sender, RoutedEventArgs e)
        {
            var person = (sender as Button).DataContext as Persons;
            if (person != null)
            {
                Dbs.Delete(person);
                persons.Remove(person);
            }
        }

        private void btnPersonUpdate_Click(object sender, RoutedEventArgs e)
        {
            var person = (sender as Button).DataContext as Persons; if (person != null)
            {
                PersonWindow.ShowDialog(person, r => {
                    Dbs.Update(r);
                });
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (lbPersons.Visibility == Visibility.Visible)
            {
                lbClasses.Visibility = Visibility.Visible;
                lbPersons.Visibility = Visibility.Collapsed;
            }
            else {
                //退出管理界面
            }
        }

        private void btnClassUpdate_Click(object sender, RoutedEventArgs e)
        {
            var subGroup = (sender as Button).DataContext as SubGroups;
            if (subGroup != null)
            {
                ClassWindow.ShowDialog(subGroup, r => {
                    Dbs.Update(r);
                });
            }
        }
    }
}
