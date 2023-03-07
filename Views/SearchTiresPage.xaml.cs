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

namespace TiresShopApplication.Views
{
    public partial class SearchTiresPage : Page
    {
        private List<InnerTiresWheelClass> InnerAutoTiresList = new List<InnerTiresWheelClass>();
        List<InnerAutoModif> listInnerAutoModifs;
        List<DriveUnit> listInnerDriveUnit;

        public SearchTiresPage()
        {
            InitializeComponent();

            AutoModifCombobox.ItemsSource = listInnerAutoModifs = GetItemsSourceInnerAutoModif();
            DriveUnitCombobox.ItemsSource = listInnerDriveUnit = GetItemsSourceDriveUnit();

            LoadItemsToMainWrapPanel();
        }

        

        private List<InnerAutoModif> GetItemsSourceInnerAutoModif()
        {
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

        private List<DriveUnit> GetItemsSourceDriveUnit()
        {
            using (TiresDbContext db = new())
            {
                return new List<DriveUnit>(db.DriveUnits);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

            MainWrapPanel.Children.Clear();
            string model = "";
            string driveUnit = "";
            try
            {
                model = $"{((InnerAutoModif)AutoModifCombobox.SelectedItem).Model ?? ""} {((InnerAutoModif)AutoModifCombobox.SelectedItem).Year.ToString() ?? ""}";
            }
            catch (Exception)
            {
                model = "";
            }
            try
            {
                driveUnit = ((DriveUnit)DriveUnitCombobox.SelectedItem).Name ?? "";
            }
            catch (Exception)
            {
                driveUnit = "";  
            }
            
            decimal.TryParse(PriceFromTextbox.Text, out decimal priceFrom);
            decimal.TryParse(PriceToTextbox.Text, out decimal priceTo);
            if (priceTo == 0)
            {
                priceTo = int.MaxValue;
            }
            List<InnerTiresWheelClass> innerAutoTires = InnerAutoTiresList.Where(x => x.TireName.Contains(model) && x.DriveUnit.Contains(driveUnit) && x.Price >= priceFrom && x.Price <= priceTo).ToList();
            foreach (var item in innerAutoTires)
            {
                MainWrapPanel.Children.Add(new CardTireView(item, this));
            }

        }

        public void LoadItemsToMainWrapPanel()
        {
            MainWrapPanel.Children.Clear();
            using (TiresDbContext db = new())
            {
                InnerAutoTiresList = new List<InnerTiresWheelClass>(db.AutoTires.Where(a => a.idType == 1 && a.Status == "Активен").ToList().Join(listInnerAutoModifs, x => x.IdModif, y => y.Id, (x, y) => new
                {
                    x.Id,
                    y.Model,
                    y.Year,
                    x.IdDriveUnit,
                    y.Diameter,
                    x.Season,
                    x.Load,
                    x.Image,
                    x.Price,
                    x.Amount,
                    x.Type,
                    x.Color,
                }).Join(listInnerDriveUnit, x => x.IdDriveUnit, y => y.Id, (x, y) => new InnerTiresWheelClass(x.Id, x.Model, x.Year, y.Name, x.Diameter, x.Price, x.Season, x.Load, x.Image!,x.Amount, x.Type, x.Color)).Select(x => x));
            }

            foreach (var item in InnerAutoTiresList)
            {
                MainWrapPanel.Children.Add(new CardTireView(item, this));
            }
        }


        private void ClearFilterFieldsButton_Click(object sender, RoutedEventArgs e)
        {
            AutoModifCombobox.SelectedIndex = -1;
            DriveUnitCombobox.SelectedIndex = -1;
            PriceFromTextbox.Text = default(string);
            PriceToTextbox.Text = default(string);
            
            LoadItemsToMainWrapPanel();
        }
    }
}
