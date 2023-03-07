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
    /// Логика взаимодействия для AddAutoMarkView.xaml
    /// </summary>
    public partial class AddAutoMarkView : UserControl
    {
        public AddAutoMarkView()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextbox.Text) == false)
            {
                using (TiresDbContext db = new())
                {
                    db.Add(new AutoMark()
                    {
                        Name = NameTextbox.Text
                    });
                    db.SaveChanges();
                }
            }
            AddDataWindow.AddDataWindowParent?.Close();
        }
    }
}
