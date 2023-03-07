using DocumentFormat.OpenXml.Office.CustomUI;
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
    /// Логика взаимодействия для UpdateAmount.xaml
    /// </summary>
    public partial class UpdateAmount : UserControl
    {
        public UpdateAmount()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string id = IdTextBox.Text;
            int amounts = Int32.Parse(AmountTextbox.Text);

            if (IdTextBox.Text == "" || AmountTextbox.Text == "")
            {
                MessageBox.Show("Заполните поля");
            }
            else
            {
               
                {
                    TiresDbContext context = new TiresDbContext();

                    IEnumerable<AutoTire> customers = context.AutoTires
                        // Загрузить всех покупателей с фамилией "Иванов"
                        .Where(c => c.Id.ToString() == id)
                        .Select(c => c.Id)
                        .AsEnumerable()
                        // Поменять им всем фамилию
                        .Select(id => new AutoTire
                        {
                            Id = id,
                            Amount = amounts,
                        });

                    foreach (AutoTire customer in customers)
                    {
                        // Указать, что запись изменилась
                        context.AutoTires.Attach(customer);
                        context.Entry(customer)
                            .Property(c => c.Amount).IsModified = true;
                    }

                    // Сохранить изменения
                    context.SaveChanges();
                    MessageBox.Show("Сохранено");
                    
                }
            }
        }
    } 
}
