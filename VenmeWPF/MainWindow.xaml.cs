using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace VenmeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VenmeContext _venmeContext = new VenmeContext();

        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            CollectionViewSource venmeContextViewSource = (CollectionViewSource)FindResource("venmeContextViewSource");
            // Load data by setting the CollectionViewSource.Source property:
            _venmeContext.Transactions.Load();
            venmeContextViewSource.Filter += yourFilter;
            venmeContextViewSource.Source = _venmeContext.Transactions.Local;
        }

        private void yourFilter(object sender, FilterEventArgs e)
        {
            var obj = e.Item as Transaction;
            if (obj != null)
                e.Accepted = obj.FromUserId.Equals(1);
        }

        private void DatePicker_SelectedDateChanged(object sender,
            SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                this.Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                this.Title = date.Value.ToShortDateString();
            }
        }
    }
}
