using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laba3
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(FromTextBox.Text, out int from) ||
                !int.TryParse(ToTextBox.Text, out int to) ||
                !int.TryParse(StepTextBox.Text, out int step) || step <= 0)
            {
                MessageBox.Show("Будь ласка, введіть коректні цілі числа.");
                return;
            }

            if (from > to)
            {
                MessageBox.Show("Початкове значення не може бути більшим за кінцеве.");
                return;
            }

            int count = ((to - from) / step) + 1;

            if (count > 10000)
            {
                MessageBox.Show($"Надто багато кнопок ({count}). Максимум — 10 000.");
                return;
            }

            for (int i = from; i <= to; i += step)
            {
                _viewModel.ButtonNumbers.Add(i);
            }
        }

        private void RemoveMultiplesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(RemoveMultiplesTextBox.Text, out int multiple) || multiple == 0)
            {
                MessageBox.Show("Введіть коректне ціле число, яке не дорівнює 0.");
                return;
            }

            var itemsToRemove = _viewModel.ButtonNumbers
                .Where(x => x % multiple == 0)
                .ToList();

            foreach (var item in itemsToRemove)
                _viewModel.ButtonNumbers.Remove(item);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ButtonNumbers.Clear();
        }
    }
}