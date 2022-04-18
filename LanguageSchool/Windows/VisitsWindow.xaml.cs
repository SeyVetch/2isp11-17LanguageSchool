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
    /// Логика взаимодействия для VisitsWindow.xaml
    /// </summary>
    public partial class VisitsWindow : Window
    {
        EF.Client ThisClient = AppData.Context.Client.FirstOrDefault();
        List<EF.ServiceHistory> Visits = AppData.Context.ServiceHistory.ToList();
        public VisitsWindow()
        {
            InitializeComponent();
            SetVisits();
        }
        public VisitsWindow(EF.Client c)
        {
            ThisClient = c;
            InitializeComponent();
            SetVisits();
        }
        void SetVisits()
        {
            Visits = AppData.Context.ServiceHistory.Where(i => i.IdClient == ThisClient.IdClient).ToList();
            lvVisits.ItemsSource = Visits;
        }
    }
}
