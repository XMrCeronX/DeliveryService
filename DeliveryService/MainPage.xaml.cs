using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace DeliveryService
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            LogWriter.Log("Привязка дынных через ItemsSource.");
            phonesList.ItemsSource = Manager.Instance.orders;
            LogWriter.Log("Привязка ComboBox.ItemsSource.");
            orderComboBox.ItemsSource = Manager.Instance.orders.Select(i => i.OrderArea.Name).Distinct();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            LogWriter.Log("Переход на AddPage.");
            Manager.Instance.MainFrame.Content = new AddPage();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            LogWriter.Log("Сортировка данных.");
            ObservableCollection<Order> ordered = new ObservableCollection<Order>(Manager.Instance.orders
                .OrderBy(i => i.OrderArea.Id)
                .ThenBy(i => i.DeliveryTime)
                .ToList());
            Manager.Instance.orders = ordered;
            LogWriter.Log("Обновление дынных через ItemsSource.");
            phonesList.ItemsSource = Manager.Instance.orders;
        }
        /// <summary>
        /// В результирующий файл либо БД необходимо вывести результат фильтрации
        /// заказов для доставки в конкретный район города в ближайшие полчаса после времени первого заказа.
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="fullPath"></param>
        public static async void SaveAreaOrders(string areaName, ObservableCollection<Order> orders, string fullPath)
        {
            LogWriter.Log("Сохранение заказов по выбранному району.");
            using (StreamWriter writer = new StreamWriter(fullPath, false))
            {
                // get by areaName
                var ordered = orders.Where(i => i.OrderArea.Name == areaName).ToList();
                LogWriter.Log($"Получение данных по району города ({areaName}):");
                string orderedString = "";
                foreach (var item in ordered)
                {
                    orderedString += $"\n{item}";
                }
                LogWriter.Log(orderedString);
                // get first by DeliveryTime
                var firstOrder = ordered.OrderBy(i => i.DeliveryTime).FirstOrDefault();
                LogWriter.Log($"Первый заказ по времени: {firstOrder}");
                // LINQ
                var nextHalfHourAfterFirstOrder = (from p in ordered
                                                   where p.DeliveryTime >= firstOrder.DeliveryTime && p.DeliveryTime <= firstOrder.DeliveryTime.AddMinutes(30)
                                                   orderby p.DeliveryTime
                                                   select p).ToList();
                LogWriter.Log($"Получение конкретного района города в ближайшие полчаса после времени первого заказа:");
                string nextHalfHourAfterFirstOrderString = "";
                foreach (var item in nextHalfHourAfterFirstOrder)
                {
                    nextHalfHourAfterFirstOrderString += $"\n{item}";
                }
                LogWriter.Log(nextHalfHourAfterFirstOrderString);
                foreach (Order order in nextHalfHourAfterFirstOrder)
                {
                    await writer.WriteLineAsync(order.GetSaveFormat());
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveAreaOrders(orderComboBox.SelectedItem.ToString(), Manager.Instance.orders, "save.txt");
        }

        private void orderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSave.IsEnabled = orderComboBox.SelectedItem.ToString() != string.Empty;
        }
    }
}
