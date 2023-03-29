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
    /// Логика взаимодействия для editVisit.xaml
    /// </summary>
    public partial class editVisit : Window
    {
        // Подключаемся к БД
        beautyDBEntities _db = new beautyDBEntities();

        // Переменная ид визита
        int Id = 0;
        public editVisit(int VisitId)
        {
            InitializeComponent();

            // Получаем значение переменной из таблицы
            Id = VisitId;
        }

        // Функция сохранения новых значений в БД с последующим обновлением таблицы
        private void editVisitBtn_Click(object sender, RoutedEventArgs e)
        {
            // Обновляем значения в БД на значения из формы и сохраняем
            Visit updateVisit = (from v in _db.Visits where v.id == Id select v).Single();
            updateVisit.client = comboboxClient.Text.Trim();
            updateVisit.service = comboboxService.Text.Trim();
            updateVisit.date = fieldDate.Text.Trim();
            _db.SaveChanges();

            // Обновляем таблицу и закрываем форму
            MainWindow.listVisits.ItemsSource = _db.Visits.ToList();
            this.Close();
        }
    }
}
