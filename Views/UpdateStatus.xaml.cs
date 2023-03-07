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

namespace TiresShopApplication.Views
{
    /// <summary>
    /// Логика взаимодействия для UpdateStatus.xaml
    /// </summary>
    public partial class UpdateStatus : UserControl
    {
        public UpdateStatus()
        {
            InitializeComponent();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string id = IdTextBox.Text;
            string status = status_update.Text;

            if (IdTextBox.Text == "" || status_update.Text == "")
            {
                MessageBox.Show("Заполните поля");
            }
            else
            {

                {
                    TiresDbContext context = new TiresDbContext();

                    IEnumerable<AutoTire> customers = context.AutoTires

                        .Where(c => c.Id.ToString() == id)
                        .Select(c => c.Id)
                        .AsEnumerable()

                        .Select(id => new AutoTire
                        {
                            Id = id,
                            Status = status,
                        });

                    foreach (AutoTire customer in customers)
                    {

                        context.AutoTires.Attach(customer);
                        context.Entry(customer)
                            .Property(c => c.Status).IsModified = true;
                    }


                    context.SaveChanges();
                    MessageBox.Show("Сохранено");
                }

            }
        }
    }
}
