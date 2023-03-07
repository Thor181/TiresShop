using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
using Word = Microsoft.Office.Interop.Word;

namespace TiresShopApplication.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminPanelPage.xaml
    /// </summary>
    public partial class AdminPanelPage : Page
    {
        private string[] _tables = { "Марки", "Модели", "Модификации", "Шины", "Диски", "Привод", "Продажи",/*"Продажа Товара",*/ "Пользователи" };
        private string _selectedTable = "";
        private List<InnerWheel> InnerAutoTiresList1 = new List<InnerWheel>();
        List<AutoTire> listTire;
        List<Sale> listSale;



        public AdminPanelPage()
        {
            InitializeComponent();
            WorkTableCombobox.ItemsSource = _tables;
            //LoadItemsToMainWrapPanel();
        }

        private void WorkTableCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedTable = WorkTableCombobox.SelectedItem.ToString()!;
            LoadItemsToDataGrid();
        }
        //private List<InnerAutoModif> GetItemsSourceInnerAutoModif()
        //{
        //    using (TiresDbContext db = new())
        //    {
        //        return new List<AutoTire>(db.AutoTire.Join(db.SaleGoods, x => x.Id, y => y.Id_Goods, (x, y) => new
        //        {
                   
        //        }).ToList());
        //    }
        //}
        //public void LoadItemsToMainWrapPanel()
        //{
           
        //    using (TiresDbContext db = new())
        //    {
        //        InnerAutoTiresList1 = new List<InnerWheel>(db.SaleGoods.ToList().Join(listTire, x => x.Id_Goods, y => y.Id, (x, y) => new
        //        {
        //           x.Id_SaleGoods,
        //           x.Id_Goods,
        //           x.id_Sale,
        //           x.Amount,
        //        }).Join(listSale, x => x.id_Sale, y => y.IdSale, (x, y) => new InnerWheel(x.Id_SaleGoods, x.Id_Goods,x.id_Sale, x.Amount)).Select(x => x));
        //    }

        //    foreach (var item in InnerAutoTiresList1)
        //    {
        //        CartCollectionWheel.AddItem(item);
        //    }
        //}

        private enum ButtonsWidth
        {
            ButtonDisabled = 0,
            ButtonAmountVisible = 190,
            ButtonUserVisible = 240,
            ButtonStatusVisible = 198
        }

        private void LoadItemsToDataGrid()
        {
            MainDataGridView.ItemsSource = new List<int>();
            using (TiresDbContext db = new())
            {
                if (_selectedTable == "Продажи")
                {
                    ExportSales.Visibility = Visibility.Visible;
                }
                else
                {
                    ExportSales.Visibility = Visibility.Hidden;
                }
                switch (_selectedTable)
                {
                    case "Марки":
                        MainDataGridView.ItemsSource = db.AutoMarks.Select(x => new { x.Id, x.Name }).ToList();
                        Buttom_Amount.Visibility = Visibility.Hidden; Buttom_Amount.Width = (int)ButtonsWidth.ButtonDisabled;
                        Buttom_User.Visibility = Visibility.Hidden; Buttom_User.Width = (int)ButtonsWidth.ButtonDisabled;
                        Buttom_Status.Visibility = Visibility.Hidden; Buttom_Status.Width = (int)ButtonsWidth.ButtonDisabled;
                        MainDataGridView.Columns[0].Header = "Код марки";
                        MainDataGridView.Columns[1].Header = "Название марки";
                       
                        break;
                    case "Модели":
                        MainDataGridView.ItemsSource = db.AutoModels.Include(x => x.IdMarkNavigation)
                                                                    .Select(x => new
                                                                    {
                                                                        x.Id,
                                                                        Mark = x.IdMarkNavigation!.Name,
                                                                        Name = x.Name
                                                                    }).ToList();
                        Buttom_Amount.Visibility = Visibility.Hidden; Buttom_Amount.Width = (int)ButtonsWidth.ButtonDisabled;
                        Buttom_User.Visibility = Visibility.Hidden; Buttom_User.Width = (int)ButtonsWidth.ButtonDisabled;
                        Buttom_Status.Visibility = Visibility.Hidden; Buttom_Status.Width = (int)ButtonsWidth.ButtonDisabled;
                        MainDataGridView.Columns[0].Header = "Код модели";
                        MainDataGridView.Columns[1].Header = "Марка";
                        MainDataGridView.Columns[2].Header = "Название Модели";
                        break;
                    case "Модификации":
                        MainDataGridView.ItemsSource = db.AutoModifs.Include(x => x.IdModelsNavigation)
                                                                    .ThenInclude(x => x!.IdMarkNavigation)
                                                                    .Select(x => new
                                                                    {
                                                                        Id = x.Id,
                                                                        Mark = x.IdModelsNavigation!.IdMarkNavigation!.Name,
                                                                        Model = x.IdModelsNavigation.Name,
                                                                        Modif = x.Name,
                                                                        Year = x.Year,
                                                                        Diameter = x.Diameter
                                                                    }).ToList();
                        Buttom_Amount.Visibility = Visibility.Hidden; Buttom_Amount.Width = (int)ButtonsWidth.ButtonDisabled;
                        Buttom_User.Visibility = Visibility.Hidden; Buttom_User.Width = (int)ButtonsWidth.ButtonDisabled;
                        Buttom_Status.Visibility = Visibility.Hidden; Buttom_Status.Width = (int)ButtonsWidth.ButtonDisabled;
                        MainDataGridView.Columns[0].Header = "Код модификации";
                        MainDataGridView.Columns[1].Header = "Марка";
                        MainDataGridView.Columns[2].Header = "Модель";
                        MainDataGridView.Columns[3].Header = "Название модификации";
                        MainDataGridView.Columns[4].Header = "Год выпуска";
                        MainDataGridView.Columns[5].Header = "Параметры колеса";
                        break;
                    case "Шины":
                        MainDataGridView.ItemsSource = db.AutoTires.Where(a => a.idType == 1).Include(x => x.IdModifNavigation)
                                                                   .ThenInclude(x => x!.IdModelsNavigation)
                                                                   .ThenInclude(x => x!.IdMarkNavigation)
                                                                   .Include(x => x.IdDriveUnitNavigation)
                                                                   .Select(x => new
                                                                   {
                                                                       Id = x.Id,
                                                                       Mark = x.IdModifNavigation!.IdModelsNavigation!.IdMarkNavigation!.Name,
                                                                       Model = x.IdModifNavigation.IdModelsNavigation.Name,
                                                                       Name = x.IdModifNavigation!.Name,
                                                                       Year = x.IdModifNavigation.Year,
                                                                       Diameter = x.IdModifNavigation.Diameter,
                                                                       DriveUnit = x.IdDriveUnitNavigation!.Name,
                                                                       Amount = x.Amount,
                                                                       Lods = x.Load,
                                                                       Season = x.Season,
                                                                       Image = x.Image,
                                                                       Price = x.Price,
                                                                       Status = x.Status,
                                                                   }).ToList();
                        Buttom_Amount.Visibility = Visibility.Visible; Buttom_Amount.Width = (int)ButtonsWidth.ButtonAmountVisible;
                        Buttom_User.Visibility = Visibility.Hidden; Buttom_User.Width = (int)ButtonsWidth.ButtonDisabled;
                        Buttom_Status.Visibility = Visibility.Visible; Buttom_Status.Width = (int)ButtonsWidth.ButtonStatusVisible;
                        MainDataGridView.Columns[0].Header = "Код шины";
                        MainDataGridView.Columns[1].Header = "Марка";
                        MainDataGridView.Columns[2].Header = "Модель";
                        MainDataGridView.Columns[3].Header = "Название";
                        MainDataGridView.Columns[4].Header = "Год выпуска";
                        MainDataGridView.Columns[5].Header = "Параметры колеса";
                        MainDataGridView.Columns[6].Header = "Привод";
                        MainDataGridView.Columns[7].Header = "Количество";
                        MainDataGridView.Columns[8].Header = "Мак. скорость";
                        MainDataGridView.Columns[9].Header = "Сезон";
                        MainDataGridView.Columns[10].Header = "Фото";
                        MainDataGridView.Columns[11].Header = "Цена";
                        MainDataGridView.Columns[12].Header = "Статус";

                        break;
                    case "Продажи":
                        MainDataGridView.ItemsSource = db.SaleGood.Include(x => x.IdAutoTireNavigation)
                                                                  .Select(x => new InnerSaleGood(x.Id, 
                                                                                                 x.DateOrder, 
                                                                                                 x.Cost, 
                                                                                                 x.Amount, 
                                                                                                 x.IdAutoTireNavigation!.Id)).ToList();
                                                                   
                        MainDataGridView.Columns[0].Header = "Id продажи";
                        MainDataGridView.Columns[1].Header = "Дата продажи";
                        MainDataGridView.Columns[2].Header = "Стоимость";
                        MainDataGridView.Columns[3].Header = "Наименование товара";
                        MainDataGridView.Columns[4].Header = "Количество";
                        Buttom_User.Visibility = Visibility.Visible; Buttom_User.Width = (int)ButtonsWidth.ButtonUserVisible;
                        Buttom_Amount.Visibility = Visibility.Visible; Buttom_Amount.Width = (int)ButtonsWidth.ButtonAmountVisible;
                        Buttom_Status.Visibility = Visibility.Hidden; Buttom_Status.Width = (int)ButtonsWidth.ButtonDisabled; 
                      
                        break;
                    case "Диски":
                        MainDataGridView.ItemsSource = db.AutoTires.Where(a => a.idType == 2).Include(x => x.IdModifNavigation)
                                                                    .ThenInclude(x => x!.IdModelsNavigation)
                                                                    .ThenInclude(x => x!.IdMarkNavigation)
                                                                    .Include(x => x.IdDriveUnitNavigation)
                                                                    .Select(x => new
                                                                    {
                                                                        Id = x.Id,
                                                                        Mark = x.IdModifNavigation!.IdModelsNavigation!.IdMarkNavigation!.Name,
                                                                        Model = x.IdModifNavigation.IdModelsNavigation.Name,
                                                                        Name = x.IdModifNavigation!.Name,
                                                                        Year = x.IdModifNavigation.Year,
                                                                        Diameter = x.IdModifNavigation.Diameter,
                                                                        DriveUnit = x.IdDriveUnitNavigation!.Name,
                                                                        Amount = x.Amount,
                                                                        Type = x.Type,
                                                                        Color = x.Color,
                                                                        Image = x.Image,
                                                                        Price = x.Price,
                                                                        Status = x.Status,
                                                                    }).ToList();
                        Buttom_Amount.Visibility = Visibility.Visible; Buttom_Amount.Width = (int)ButtonsWidth.ButtonAmountVisible;
                        Buttom_User.Visibility = Visibility.Hidden; Buttom_User.Width = (int)ButtonsWidth.ButtonDisabled;
                        Buttom_Status.Visibility = Visibility.Visible; Buttom_Status.Width = (int)ButtonsWidth.ButtonStatusVisible;
                        MainDataGridView.Columns[0].Header = "Код диска";
                        MainDataGridView.Columns[1].Header = "Марка";
                        MainDataGridView.Columns[2].Header = "Модель";
                        MainDataGridView.Columns[3].Header = "Название";
                        MainDataGridView.Columns[4].Header = "Год выпуска";
                        MainDataGridView.Columns[5].Header = "Параметры колеса";
                        MainDataGridView.Columns[6].Header = "Привод";
                        MainDataGridView.Columns[7].Header = "Количество";
                        MainDataGridView.Columns[8].Header = "Тип диска";
                        MainDataGridView.Columns[9].Header = "Цвет";
                        MainDataGridView.Columns[10].Header = "Фото";
                        MainDataGridView.Columns[11].Header = "Цена";
                        MainDataGridView.Columns[12].Header = "Статус";
                        break;
                    case "Привод":
                        MainDataGridView.ItemsSource = db.DriveUnits.Select(x => new { x.Id, x.Name }).ToList();
                        Buttom_Amount.Visibility = Visibility.Hidden; Buttom_Amount.Width = (int)ButtonsWidth.ButtonDisabled;
                        MainDataGridView.Columns[0].Header = "Код привода";
                        MainDataGridView.Columns[1].Header = "Название";
                        break;
                    //case "Продажа Товара":
                        
                    //    MainDataGridView.ItemsSource = new List<InnerWheel>();
                    //    MainDataGridView.ItemsSource = CartCollectionWheel.GetItems();
                    //    break;
                    case "Пользователи":
                        using (UserDbContext dbus = new())
                        {
                            MainDataGridView.ItemsSource = dbus.User.Select(x => new
                            {
                                x.login,
                                x.password,
                                x.type_user,
                            }).ToList();
                            Buttom_Amount.Visibility = Visibility.Hidden; Buttom_Amount.Width = (int)ButtonsWidth.ButtonDisabled;
                            Buttom_Status.Visibility = Visibility.Hidden; Buttom_Status.Width = (int)ButtonsWidth.ButtonDisabled;
                            Buttom_User.Visibility = Visibility.Visible; Buttom_User.Width = (int)ButtonsWidth.ButtonUserVisible;
                            MainDataGridView.Columns[0].Header = "Логин";
                            MainDataGridView.Columns[1].Header = "Пароль";
                            MainDataGridView.Columns[2].Header = "Тип пользователя";
                        }
                            break;
                    default:
                        Buttom_Amount.Visibility = Visibility.Hidden; Buttom_Amount.Width = (int)ButtonsWidth.ButtonDisabled;
                        break;
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            switch (_selectedTable)
            {
                case "Марки":
                    new AddDataWindow(new AddAutoMarkView()).ShowDialog();
                    break;
                case "Модели":
                    new AddDataWindow(new AddAutoModelView()).ShowDialog();
                    break;
                case "Модификации":
                    new AddDataWindow(new AddAutoModifView()).ShowDialog();
                    break;
                case "Шины":
                    new AddDataWindow(new AddAutoTireWheelView(AddAutoTireWheelView.Table.AutoTire)).ShowDialog(); 
                    break;
                case "Диски":
                    new AddDataWindow(new AddAutoTireWheelView(AddAutoTireWheelView.Table.AutoWheel)).ShowDialog();
                    break;
                case "Привод":
                    new AddDataWindow(new AddDriveUnitView()).ShowDialog();
                    break;
                default:
                    break;
            }
            LoadItemsToDataGrid();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string id = IdForDeleteTextbox.Text;
            if (string.IsNullOrWhiteSpace(id) == false)
            {
                try
                {
                    using (TiresDbContext db = new())
                    {
                        switch (_selectedTable)
                        {
                            case "Марки":
                                db.Remove(db.AutoMarks.Where(x => x.Id.ToString() == id).First());
                                break;
                            case "Модели":
                                db.Remove(db.AutoModels.Where(x => x.Id.ToString() == id).First());
                                break;
                            case "Модификации":
                                db.Remove(db.AutoModifs.Where(x => x.Id.ToString() == id).First());
                                break;
                            case "Шины":
                                db.Remove(db.AutoTires.Where(x => x.Id.ToString() == id).First());
                                break;
                            case "Диски":
                                db.Remove(db.AutoTires.Where(x => x.Id.ToString() == id).First());
                                break;
                            case "Привод":
                                db.Remove(db.DriveUnits.Where(x => x.Id.ToString() == id).First());
                                break;
                            default:
                                break;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("База данных не содержит указанного id");
                }
                IdForDeleteTextbox.Text = default(string);
                LoadItemsToDataGrid();  
            }    
        }

        private void Buttom_Amount_Click(object sender, RoutedEventArgs e)
        {
            new AddDataWindow(new UpdateAmount()).ShowDialog();
        }

        private void Buttom_User_Click(object sender, RoutedEventArgs e)
        {
            new AddDataWindow(new AddUser()).ShowDialog();
        }
        private void Buttom_Status_Click(object sender, RoutedEventArgs e)
        {
            new AddDataWindow(new UpdateStatus()).ShowDialog();
        }

        private void ExportSales_Click(object sender, RoutedEventArgs e)
        {
            var currentList = MainDataGridView.ItemsSource;
            Word.Table table = CreateWordReport(currentList);
        }

        private Word.Table CreateWordReport(IEnumerable collection)
        {
            int countColumns = 5;
            int countRows = collection.GetCountItems() + 1;
            object oEndOfDoc = "\\endofdoc";
            string[] nameHeaders = { "№ продажи", "Дата продажи", "Стоимость", "Товар", "Количество"};
            decimal sum = 0;
            Word.Application application = new Word.Application() { Visible = false };
            Word.Document document = application.Documents.Add();
            Word.Range range = document.Bookmarks.get_Item(ref oEndOfDoc).Range;
            
            range.Text = $"Дата отчета: {DateTime.Now}";
            range.Start = $"Дата отчета: {DateTime.Now}".Count() + 1;
            Word.Table table = document.Tables.Add(range, countRows, countColumns) ;
            table.Range.ParagraphFormat.SpaceAfter = 7;
            
            for (int i = 1; i < countColumns + 1; i++)
            {
                table.Cell(1, i).Range.Text = nameHeaders[i-1];
            }
            int currentRow = 2;
            foreach (var item in collection)
            {        
                if (item is InnerSaleGood good)
                {
                    table.Cell(currentRow, 1).Range.Text = good.IdSale.ToString();
                    table.Cell(currentRow, 2).Range.Text = good.DataOrder.ToString();
                    table.Cell(currentRow, 3).Range.Text = good.Cost.ToString();
                    table.Cell(currentRow, 4).Range.Text = good.NameGoods;
                    table.Cell(currentRow, 5).Range.Text = good.Amount.ToString();                 
                    sum += good.Cost;
                    currentRow++;
                }
            }
            table.Rows[1].Range.Font.Bold = 1;
            range.Start = document.Tables[1].Range.End + 1;
            range.Text = $"\nИтого: {sum} руб.";
            application.Visible = true;
            try
            {
                table.Borders.Shadow = true;
            }
            catch (Exception)
            {
            }
            return table;
        }
    }
    public static class Counter
    {
        public static int GetCountItems(this IEnumerable collection)
        {
            int counter = 0;
            foreach (var item in collection)
            {
                counter++;
            }
            return counter;
        }
    }
}
