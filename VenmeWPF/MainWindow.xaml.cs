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

        private DateTime? _datePicked;

        private CollectionViewSource _venmeContextViewSource;



        public MainWindow()
        {
            InitializeComponent();
            label.Content = "Please select a date";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            _venmeContextViewSource = (CollectionViewSource)FindResource("venmeContextViewSource");
            // Load data by setting the CollectionViewSource.Source property:
            _venmeContext.Transactions.Load();
            _venmeContextViewSource.Filter += yourFilter;
            _venmeContextViewSource.Source = _venmeContext.Transactions.Local;
        }

        private void yourFilter(object sender, FilterEventArgs e)
        {
            var obj = e.Item as Transaction;
            //if (obj != null)
            //    e.Accepted = !obj.FromUserId.Equals(obj.toUserId);

            if (obj == null)
            {
                e.Accepted = false;
                return;
            }

            if (_datePicked == null)
            {
                e.Accepted = false;
                return;
            }

            //e.Accepted = !obj.FromUserId.Equals(_datePicked.Value);
            e.Accepted = true;
        }

        private void DatePicker_SelectedDateChanged(object sender,
            SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            _datePicked = picker.SelectedDate;

            label.Content = _datePicked == null ? "Please select a date" : "";

            _venmeContextViewSource.View.Refresh();
        }
    }
}
