using MaterialDesignThemes.Wpf;
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
    public partial class AddAutoTireWheelView : UserControl
    {
        private byte[] _imageData = null!;

        public enum Table
        {
            AutoTire,
            AutoWheel
        }
        private Table WorkTable { get; set; }
        public AddAutoTireWheelView(Table workTable)
        {
            InitializeComponent();
            WorkTable = workTable;
         
            using (TiresDbContext db = new())
            {
               
                AutoModifCombobox.ItemsSource = GetItemsSourceInnerAutoModif();
                DriveUnitCombobox.ItemsSource = db.DriveUnits.ToList();
            }
        }

        private List<InnerAutoModif> GetItemsSourceInnerAutoModif()
        {
            if (WorkTable == Table.AutoWheel)
            {
                LoadsText.Visibility = Visibility.Visible;
                Text.Visibility = Visibility.Visible;
                SeasonTextbox.Visibility = Visibility.Hidden;
                LoadsTextbox.Visibility= Visibility.Hidden;
            }
            using (TiresDbContext db = new())
            {
                return new List<InnerAutoModif>(db.AutoModifs.Join(db.AutoModels, x => x.IdModels, y => y.Id, (x, y) => new
                {
                    Id = x.Id,
                    AutoModifName = x.Name,
                    AutoModelName = y.Name,
                    y.IdMark,
                    Year = x.Year,
                    Diameter = x.Diameter
                }).Join(db.AutoMarks, x => x.IdMark, y => y.Id, (x, y) => new InnerAutoModif(x.Id, y.Name, x.AutoModelName, x.AutoModifName!, x.Year ?? 0, x.Diameter!)).ToList());
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PriceTextbox.Text) == true)
            {
                MessageBox.Show("Необходимо ввести стоимость!");
            }
          

            try
            {
                using (TiresDbContext db = new())
                {
                    if (WorkTable == Table.AutoTire)
                    {
                        
                        db.Add(new AutoTire()
                        {
                            IdModif = ((InnerAutoModif)AutoModifCombobox.SelectedItem).Id,
                            IdDriveUnit = ((DriveUnit)DriveUnitCombobox.SelectedItem).Id,
                            idType = 1,
                            Amount = int.Parse(AmountTextbox.Text),
                            Season = SeasonTextbox.Text,
                            Load = LoadsTextbox.Text, 
                            Image = _imageData,
                            Price = decimal.Parse(PriceTextbox.Text)
                        });
                    }
                    else if(WorkTable == Table.AutoWheel)
                    {
                       
                       db.Add(new AutoTire()
                        {
                           
                            IdModif = ((InnerAutoModif)AutoModifCombobox.SelectedItem).Id,
                            IdDriveUnit = ((DriveUnit)DriveUnitCombobox.SelectedItem).Id,
                            idType = 2,
                            Amount = int.Parse(AmountTextbox.Text),
                            Type = LoadsText.Text,
                            Color = Text.Text,
                            Image = _imageData,
                            Price = decimal.Parse(PriceTextbox.Text)
                        }) ;
                    }
                    
                    db.SaveChanges();
                    MessageBox.Show("Вы успешно добавили шину");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Необходимо заполнить все поля!");
                return;
            }

            AddDataWindow.AddDataWindowParent?.Close();
        }

        private void GridPhoto_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PlaceForPhoto.Source = ImageWorker.LoadImage(out _imageData);
        }
    }
}
