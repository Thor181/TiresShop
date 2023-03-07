using Microsoft.EntityFrameworkCore;
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

namespace TiresShopApplication
{

    public partial class AddAutoModifView : UserControl
    {
        public AddAutoModifView()
        {
            InitializeComponent();
            using (TiresDbContext db = new())
            {
                AutoModelCombobox.ItemsSource = db.AutoModels.Include(x => x.IdMarkNavigation).Select(x => new InnerAutoModel(x.Id, $"{x.IdMarkNavigation!.Name} {x.Name}")).ToList();
            }
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int.Parse(YearTextbox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Год выпуска введен неверно\nНеобходимо ввести в формате: YYYY ");
                return;
            }

            if (string.IsNullOrWhiteSpace(NameTextbox.Text) == false)
            {
                using (TiresDbContext db = new())
                {
                    db.Add(new AutoModif()
                    {
                        Name = NameTextbox.Text,
                        IdModels = ((InnerAutoModel)AutoModelCombobox.SelectedItem).GetId(),
                        Year = int.Parse(YearTextbox.Text),
                        Diameter = DiameterTextbox.Text
                    });
                    db.SaveChanges();
                }
            }
            AddDataWindow.AddDataWindowParent?.Close();
        }


    }
}
    




