using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace beauty_salon
{
    /// <summary>
    /// Логика взаимодействия для addVisit.xaml
    /// </summary>
    public partial class addVisit : Window
    {
        // Подключаемся к БД
        beautyDBEntities _db = new beautyDBEntities();
        public addVisit()
        {
            InitializeComponent();

            // Добавляем в ComboBox список клиентов
            var clientsList = _db.Clients.ToList();

            // Добавляем элементы
            foreach (var client in clientsList)
            {
                comboboxClient.Items.Add(new ComboBoxItem
                {
                    Content = client.family + " " + client.name + " " + client.patronymic
                });
            }

            // Добавляем в ComboBox список услуг
            var servicesList = _db.Services.ToList();

            // Добавляем элементы
            foreach (var service in servicesList)
            {
                comboboxService.Items.Add(new ComboBoxItem
                {
                    Content = service.name + " " + service.price + " руб."
                });
            }

        }

        private void addVisitBtn_Click(object sender, RoutedEventArgs e)
        {

            // Получаем данные из формы
            Visit newVisit = new Visit()
            {
                client = comboboxClient.Text.Trim(),
                service = comboboxService.Text.Trim(),
                date = fieldDate.Text
            };

            // Добавлям в БД и обновляем список услуг
            _db.Visits.Add(newVisit);
            _db.SaveChanges();
            MainWindow.listVisits.ItemsSource = _db.Visits.ToList();
            this.Close();
        }
    }
}
