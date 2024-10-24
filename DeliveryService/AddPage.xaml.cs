using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace DeliveryService
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        public AddPage()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LogWriter.Log("Переход на MainPage.");
            Manager.Instance.MainFrame.Content = new MainPage();
        }

        /// <summary>
        /// Проверка ввода
        /// </summary>
        /// <returns>null если все верно, string если ошибка</returns>
        private string ValidateInput(Order order)
        {
            LogWriter.Log("Валидация данных.");
            string result = null;
            float weight = 0;
            if (!float.TryParse(WeightOrder.Text, out weight))
            {
                result = "Поле \"Вес\" должно быть числом!";
            }
            order.Weight = weight;

            OrderArea orderArea = new OrderArea();

            if (IdOrderArea.Text == string.Empty) result = "Поле \"Id района\" не должно быть пустым!";
            if (NameOrderArea.Text == string.Empty) result = "Поле \"Имя района\" не должно быть пустым!";
            orderArea.Id = IdOrderArea.Text;
            orderArea.Name = NameOrderArea.Text;
            order.OrderArea = orderArea;

            if (OrderDeliveryTime.Text == string.Empty) result = "Поле \"Время доставки заказа\" не должно быть пустым!";

            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime dateTime = DateTime.Now;
            if (!DateTime.TryParseExact(OrderDeliveryTime.Text, Order.DATE_TIME_FORMAT, provider, DateTimeStyles.None, out dateTime))
            {
                result = $"Поле \"Время доставки заказа\" должно быть в формате \"{Order.DATE_TIME_FORMAT}\"!";
            }
            order.DeliveryTime = dateTime;
            return result;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Order newOrder = new Order();
            string error = ValidateInput(newOrder);
            if (error == null)
            {
                LogWriter.Log("Добавление заказа.");
                Manager.Instance.orders.Add(newOrder);
                LogWriter.Log("Переход на MainPage.");
                Manager.Instance.MainFrame.Content = new MainPage();
            }
            else
            {
                LogWriter.Log(error);
                LogWriter.Log("Вывод ошибки пользователю.");
                MessageBox.Show(error, "Добавление заказа", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
