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
using System.Windows.Shapes;

namespace beauty_salon
{
    /// <summary>
    /// Логика взаимодействия для editClient.xaml
    /// </summary>
    public partial class editClient : Window
    {
        // Подключаемся к БД
        beautyDBEntities _db = new beautyDBEntities();

        // Переменная ид клиента
        int Id = 0;

        public editClient(int ClientId)
        {
            InitializeComponent();

            // Получаем значение переменной из таблицы
            Id = ClientId;
        }

        // Функция сохранения новых значений в БД с последующим обновлением таблицы
        private void editClientBtn_Click(object sender, RoutedEventArgs e)
        {
            // Обновляем значения в БД на значения из формы и сохраняем
            Client updateClient = (from c in _db.Clients where c.id == Id select c).Single();
            updateClient.family = fieldFamily.Text.Trim();
            updateClient.name = fieldName.Text.Trim();
            updateClient.patronymic = fieldPatronymic.Text.Trim();
            updateClient.phone = fieldPhone.Text.Trim();
            _db.SaveChanges();

            // Обновляем таблицу и закрываем форму
            MainWindow.listClients.ItemsSource = _db.Clients.ToList();
            this.Close();
        }
    }
}
