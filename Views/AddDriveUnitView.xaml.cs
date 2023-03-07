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
    /// <summary>
    /// Логика взаимодействия для AddDriveUnitView.xaml
    /// </summary>
    public partial class AddDriveUnitView : UserControl
    {
        public AddDriveUnitView()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextbox.Text) == false)
            {
                using (TiresDbContext db = new())
                {
                    db.Add(new DriveUnit()
                    {
                        Id = (byte)(db.DriveUnits.OrderBy(x => x.Id).Last().Id + 1),
                        Name = NameTextbox.Text
                    });
                    db.SaveChanges();
                }
            }
            AddDataWindow.AddDataWindowParent?.Close();
        }
    }
}
