using System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Laba3
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<int> ButtonNumbers { get; set; } = new ObservableCollection<int>();
        public ICommand ButtonClickCommand { get; }

        private int? _lastClickedNumber = null;
        private int _clickCount = 0;

        public MainViewModel()
        {
            ButtonClickCommand = new RelayCommand(param =>
            {
                if (int.TryParse(param?.ToString(), out int number))
                {
                    if (_lastClickedNumber == number)
                    {
                        _clickCount++;

                        if (_clickCount >= 3)
                        {
                            MessageBox.Show("Та скільки вже можна клацати!!!", "Не клацай!!!");
                            return; 
                        }
                    }
                    else
                    {
                        _lastClickedNumber = number;
                        _clickCount = 1;
                    }

                    string result = GetSmallestDivisorInfo(number);
                    MessageBox.Show(result, $"Число {number}");
                }
            });
        }

        private string GetSmallestDivisorInfo(int number)
        {
            int absNumber = Math.Abs(number);

            if (absNumber <= 1)
                return "Число повинно бути більше 1 (за абсолютним значенням).";

            for (int i = 2; i <= Math.Sqrt(absNumber); i++)
            {
                if (absNumber % i == 0)
                    return $"Найменший дільник (за модулем): {i}";
            }

            return number > 0
                ? "Число є простим!"
                : "Число є простим за модулем!";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
