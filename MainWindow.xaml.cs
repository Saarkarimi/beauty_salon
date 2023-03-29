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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace beauty_salon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        // Подключаемся к БД
        beautyDBEntities _db = new beautyDBEntities();

        // Создаем переменые таблиц клиентов, услуг
        public static DataGrid listClients;
        public static DataGrid listServices;
        public static DataGrid listVisits;

        public MainWindow()
        {
            InitializeComponent();

            // Заполняем таблицы данными из БД
            Load();
        }

        // Функция формирует DataGrid данными на основе значений из БД
        public void Load()
        {
            myClientsGrid.ItemsSource = _db.Clients.ToList();
            listClients = myClientsGrid;

            myServicesGrid.ItemsSource = _db.Services.ToList();
            listServices = myServicesGrid;

            myVisitsGrid.ItemsSource = _db.Visits.ToList();
            listVisits = myVisitsGrid;
        }

        // По клику показываем диалог Новый клиент
        private void addClientBtn_Click(object sender, RoutedEventArgs e)
        {
            addClient addClientPage = new addClient();
            addClientPage.ShowDialog();
        }

        // По клику показываем диалог Редактирование клиента
        private void editClientBtn_Click(Object sender, RoutedEventArgs e)
        {
            // Получаем ид выбранной строки
            int Id = (myClientsGrid.SelectedItem as Client).id;

            // Заполняем окно редактора значениями из БД
            editClient editClientPage = new editClient(Id);
            editClientPage.fieldFamily.Text = (myClientsGrid.SelectedItem as Client).family;
            editClientPage.fieldName.Text = (myClientsGrid.SelectedItem as Client).name;
            editClientPage.fieldPatronymic.Text = (myClientsGrid.SelectedItem as Client).patronymic;
            editClientPage.fieldPhone.Text = (myClientsGrid.SelectedItem as Client).phone;

            editClientPage.ShowDialog();
        }

        // По клику удаляем клиента
        private void deleteClientBtn_Click(System.Object sender, RoutedEventArgs e)
        {
            int Id = (listClients.SelectedItem as Client).id;
            var deleteClient = _db.Clients.Where(c => c.id == Id).Single();
            _db.Clients.Remove(deleteClient);
            _db.SaveChanges();
            listClients.ItemsSource = _db.Clients.ToList();
        }

        // По клику показываем диалог Новая услуга
        private void addServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            addService addServicePage = new addService();
            addServicePage.ShowDialog();
        }

        // По клику показываем диалог Редактирование услуги
        private void editServiceBtn_Click(Object sender, RoutedEventArgs e)
        {
            // Получаем ид выбранной строки
            int Id = (myServicesGrid.SelectedItem as Service).id;

            // Заполняем окно редактора значениями из БД
            editService editServicePage = new editService(Id);
            editServicePage.fieldName.Text = (myServicesGrid.SelectedItem as Service).name;
            editServicePage.fieldPrice.Text = Convert.ToString((myServicesGrid.SelectedItem as Service).price);

            editServicePage.ShowDialog();
        }

        // По клику удаляем услугу
        private void deleteServiceBtn_Click(System.Object sender, RoutedEventArgs e)
        {
            int Id = (listServices.SelectedItem as Service).id;
            var deleteService = _db.Services.Where(s => s.id == Id).Single();
            _db.Services.Remove(deleteService);
            _db.SaveChanges();
            listServices.ItemsSource = _db.Services.ToList();
        }

        // По клику показываем диалог Новый визит
        private void addVisitBtn_Click(object sender, RoutedEventArgs e)
        {
            addVisit addVisitPage = new addVisit();
            addVisitPage.ShowDialog();
        }

        // По клику показываем диалог Редактирование визита
        private void editVisitBtn_Click(Object sender, RoutedEventArgs e)
        {
            // Получаем ид выбранной строки
            int Id = (myVisitsGrid.SelectedItem as Visit).id;

            // Заполняем окно редактора значениями из БД
            editVisit editVisitPage = new editVisit(Id);


            // Добавляем в ComboBox список клиентов
            var clientsList = _db.Clients.ToList();

            // Добавляем элементы
            foreach (var client in clientsList)
            {
                editVisitPage.comboboxClient.Items.Add(new ComboBoxItem
                {
                    Content = client.family + " " + client.name + " " + client.patronymic
                });
            }

            // Добавляем в ComboBox список услуг
            var servicesList = _db.Services.ToList();

            // Добавляем элементы
            foreach (var service in servicesList)
            {
                editVisitPage.comboboxService.Items.Add(new ComboBoxItem
                {
                    Content = service.name + " " + service.price + " руб."
                });
            }

            editVisitPage.ShowDialog();
        }

        // По клику удаляем заказ
        private void deleteVisitBtn_Click(System.Object sender, RoutedEventArgs e)
        {
            int Id = (listVisits.SelectedItem as Visit).id;
            var deleteVisit = _db.Visits.Where(o => o.id == Id).Single();
            _db.Visits.Remove(deleteVisit);
            _db.SaveChanges();
            listVisits.ItemsSource = _db.Visits.ToList();
        }
    }
}
