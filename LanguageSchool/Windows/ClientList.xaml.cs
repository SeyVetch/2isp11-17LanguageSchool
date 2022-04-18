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
using LanguageSchool.ClassHelper;

namespace LanguageSchool.Windows
{
    /// <summary>
    /// Логика взаимодействия для ClientList.xaml
    /// </summary>
    public partial class ClientList : Window
    {
        List<EF.Client> clients;
        int genderID = -1;
        int elementsPerPage = -1;
        int curPage = 0;
        private Comparison<EF.Client> sortBy;
        string txtFIO
        {
            get
            {
                return tbFIO.Text.ToLower();
            }
        }
        string txtEMail
        {
            get
            {
                return tbMail.Text.ToLower();
            }
        }
        string txtPhone
        {
            get
            {
                return tbPhone.Text.ToLower();
            }
        }
        public ClientList()
        {
            InitializeComponent();

            foreach (EF.Client C in AppData.Context.Client.ToList())
            {
                C.IsHidden = false;
            }
            AppData.Context.SaveChanges();
            clients = AppData.Context.Client.ToList();
            cmbGender.ItemsSource = GendersList.Genders();
            sortBy = SortConditions.sortByID;
            cmbElements.SelectedIndex = 0;
            cmbSort.SelectedIndex = 0;
            cmbGender.SelectedIndex = 0;
            ApplyFilter();
        }
        private void cmbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string gen = cmbGender.SelectedValue.ToString();
            if (gen != "Нет огран.")
            {
                genderID = AppData.Context.Gender.FirstOrDefault(i => i.GenderName == gen).IdGender;
            }
            else
            {
                genderID = -1;
            }
            ApplyFilter();
        }
        private void ApplyFilter()
        {
            List<EF.Client> filtered = AppData.Context.Client.ToList();
            foreach (EF.Client c in AppData.Context.Client.ToList())
            {
                if (c.IsHidden)
                {
                    filtered.Remove(c);
                }
            }
            if (genderID != -1)
            {
                filtered = filtered.Where(i => i.IdGender == genderID).ToList();
            }
            if ((bool)checkBirthDay.IsChecked)
            {
                filtered = filtered.Where(i => i.BirthDate.Month == DateTime.Today.Month).ToList();
            }
            clients = filtered;
            ApplySearch();
            ApplyPages(0);
        }
        private List<EF.Client> SearchByFIO(List<EF.Client> c)
        {
            return c.Where(i => i.Fio.ToLower().Contains(txtFIO)).ToList();
        }
        private List<EF.Client> SearchByMail(List<EF.Client> c)
        {
            return c.Where(i => i.Email.ToLower().Contains(txtEMail)).ToList();
        }
        private List<EF.Client> SearchByPhone(List<EF.Client> c)
        {
            return c.Where(i => i.Phone.ToLower().Contains(txtPhone)).ToList();
        }
        private void ApplySearch()
        {
            if (txtFIO != "")
            {
                clients = SearchByFIO(clients);
            }
            if (txtEMail != "")
            {
                clients = SearchByMail(clients);
            }
            if (txtPhone != "")
            {
                clients = SearchByPhone(clients);
            }
        }
        private void ApplyPages(int page)
        {
            List<EF.Client> c = clients;
            c.Sort(sortBy);
            List<EF.Client> pageClients = c;
            if (elementsPerPage != -1)
            {
                int firstIndex = page * elementsPerPage;
                if (firstIndex + 1 > clients.Count())
                {
                    curPage = clients.Count / elementsPerPage - 1;
                    if (curPage * elementsPerPage + elementsPerPage < clients.Count() - 1)
                    {
                        curPage += 1;
                    }
                    firstIndex = curPage * elementsPerPage;
                }
                if (firstIndex < 0)
                {
                    curPage = 0;
                    firstIndex = 0;
                }
                int len = elementsPerPage;
                if (firstIndex + len + 1 > clients.Count())
                {
                    len = clients.Count() - firstIndex;
                }
                pageClients = c.GetRange(firstIndex, len).ToList();
            }
            
            lvClient.ItemsSource = pageClients;
            TxtPage.Text = pageClients.Count() + "/" + clients.Count();
        }
        private void cmbElements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int el = cmbElements.SelectedIndex;
            switch (el)
            {
                default:
                    elementsPerPage = -1;
                    break;
                case 1:
                    elementsPerPage = 10;
                    break;
                case 2:
                    elementsPerPage = 50;
                    break;
                case 3:
                    elementsPerPage = 200;
                    break;
            }
            ApplyPages(0);
        }

        private void PageUp_Click(object sender, RoutedEventArgs e)
        {
            curPage += 1;
            ApplyPages(curPage);
        }
        private void PageDown_Click(object sender, RoutedEventArgs e)
        {
            curPage -= 1;
            ApplyPages(curPage);
        }

        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cmbSort.SelectedIndex)
            {
                default:
                    sortBy = SortConditions.sortByID;
                    break;
                case 1:
                    sortBy = SortConditions.sortByFIO;
                    break;
                case 2:
                    sortBy = SortConditions.sortByLastVisit;
                    break;
                case 3:
                    sortBy = SortConditions.sortByTotalVisits;
                    break;
            }
            ApplyFilter();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cmbElements.SelectedIndex = 0;
            cmbSort.SelectedIndex = 0;
            cmbGender.SelectedIndex = 0;
            tbFIO.Text = "";
            tbMail.Text = "";
            tbPhone.Text = "";
            checkBirthDay.IsChecked = false;
            ApplyFilter();
        }

        private void lvClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lvClient.SelectedIndex != -1)
            {
                MessageBoxResult res = MessageBox.Show("Удалить пользователя?", "Delete user", MessageBoxButton.OKCancel);
                if (res == MessageBoxResult.OK)
                {
                    EF.Client c = AppData.Context.Client.FirstOrDefault(i => i.IdClient == ((EF.Client)lvClient.SelectedItem).IdClient);
                    if(c.TotalVisits > 0)
                    {
                        MessageBox.Show("Нельзя было удалить пользоваетля");
                    }
                    else
                    {
                        AppData.Context.Client.FirstOrDefault(i => i.IdClient == ((EF.Client)lvClient.SelectedItem).IdClient).IsHidden = true;
                        AppData.Context.SaveChanges();
                        ApplyFilter();
                    }
                }
            }
        }

        private void checkBirthDay_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFilter();
        }

        private void lvClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvClient.SelectedIndex != -1)
            {
                VisitsWindow vw = new VisitsWindow((EF.Client)lvClient.SelectedItem);
                vw.Show();
            }
        }
    }
}
