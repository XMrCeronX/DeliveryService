using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace DeliveryService
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string fileName = "data.txt";
        Random random = new Random();

        public static async void SaveOrders(ObservableCollection<Order> orders, string fullPath)
        {
            LogWriter.Log("Сохранение данных.");
            using (StreamWriter writer = new StreamWriter(fullPath, false))
            {
                foreach (Order order in orders)
                {
                    await writer.WriteLineAsync(order.GetSaveFormat());
                }
            }
        }

        public void GenerateData(ObservableCollection<Order> orders, int amountOfDataGenerated = 100)
        {
            LogWriter.Log("Генерация данных.");
            Dictionary<string, string> orderAreaStringNames = new Dictionary<string, string>
            {
                {"Тверской", "51428b23fc2b4435b78d0b5b23fa17c2" },
                {"Арбат", "a0dc83dcd7644b709a79536823974d15" },
                {"Пресненский", "27567746e5d645e8a4e4e82955c6bd34" },
                {"Басманный", "ff5579eb3d4540f8b55068f050a5bc1e" },
                {"Хамовники", "e5836026ac98440abd7f4c85b73d29b2" },
                {"Замоскворечье", "1593a1e957a74520a54d786e90e3eb94" },
                {"Якиманка", "59683fbfa5c04c08a5628db516fd789d" },
                {"Красносельский", "bc54368ffff841c1b219d5521c8eda3c" },
                {"Сокольники", "370ebdd0da7e485ba43411a575608fbb" },
                {"Дорогомилово", "03dbc059fb8e4fceaecba8e0a2ee1107" },
            };
            for (int i = 0; i < amountOfDataGenerated; i++)
            {
                int randIndex = random.Next(0, orderAreaStringNames.Count);
                string orderName = orderAreaStringNames.ElementAt(randIndex).Key;
                string id = orderAreaStringNames[orderName];
                orders.Add(new Order((float)Tools.GetRandomNumber(random, 0, 15), new OrderArea(orderName, id), DateTime.Now.AddMinutes(Tools.GetRandomNumber(random, 10, 90))));
            }
        }

        public MainWindow()
        {
            LogWriter.Log("Запуск программы.");
            InitializeComponent();

            Manager.Instance.orders = new ObservableCollection<Order>();

            GenerateData(Manager.Instance.orders);

            string dir = Directory.GetCurrentDirectory();
            string fullPath = System.IO.Path.Combine(dir, fileName);
            Console.WriteLine(fullPath);

            SaveOrders(Manager.Instance.orders, fullPath);

            Manager.Instance.MainWindow = this;
            Manager.Instance.MainFrame = MainFrame;
            Manager.Instance.MainFrame.Content = new MainPage();
            Closing += new CancelEventHandler(MainWindow_Closing);
        }

        /// <summary>
        /// Обработка зыкрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            LogWriter.Log("Закрытие программы.");
            LogWriter.Log(new string('*', 50));
        }

    }
}