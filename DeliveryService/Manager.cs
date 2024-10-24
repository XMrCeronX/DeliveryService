using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DeliveryService
{
    public class Manager
    {
        private static Manager _INSTANCE;

        public static Manager Instance
        {
            get
            {
                if (_INSTANCE == null)
                {
                    _INSTANCE = new Manager();
                }
                return _INSTANCE;
            }
        }

        public ObservableCollection<Order> orders { get; set; }

        public Frame MainFrame { get; set; }
        public MainWindow MainWindow { get; set; }
    }
}