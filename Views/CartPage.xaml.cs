using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
using TiresShopApplication.DbClasses;

namespace TiresShopApplication.Views
{
    public partial class CartPage : Page
    {
        private static List<InnerTiresWheelClass> CartItems = new List<InnerTiresWheelClass>();
        private static List<InnerWheel> innerWheels = new List<InnerWheel>();

        public enum Table
        {
            AutoTire,
            AutoWheel
        }

        public CartPage()
        {
            InitializeComponent();
            LoadItemsToDataGrid();
            LoadItemsToDataGridWheel();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            CartCollection.RemoveItem(int.Parse(IdForDeleteTextbox.Text));
            LoadItemsToDataGrid();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            CartCollection.Clear();
            LoadItemsToDataGrid();
        }


        private void LoadItemsToDataGrid()
        {

            //MainDataGridView.ItemsSource = allProducts;
            //MainDataGridView.ItemsSource = CartCollectionWheel.GetItems();
            //MainDataGridView.ItemsSource = new List<InnerWheel>();
            MainDataGridView.ItemsSource = new List<InnerTiresWheelClass>();
            MainDataGridView.ItemsSource = CartCollection.GetItems();


            DisplayPrice();

        }
        private void LoadItemsToDataGridWheel()
        {
            //MainDataGridView.ItemsSource = new List<InnerWheel>();
            //MainDataGridView.ItemsSource = CartCollectionWheel.GetItems();
            ////MainDataGridView.ItemsSource = new List<InnerTiresWheelClass>();
            ////MainDataGridView.ItemsSource = CartCollection.GetItems();
            //DisplayPrice();
        }
        private void DisplayPrice()
        {

            PriceTextblock.Text = string.Format("{0:#.##}", CartCollection.GetSum());

        }
        private void tires()
        {
            foreach (var item in CartCollection.GetItems())
            {
                var autowheel = new AutoWheel() { Color = null!, Id = item.Id, Amount = item.Amount - 4 };


                using (var context = new TiresDbContext())
                {
                    context.AutoWheels.Attach(autowheel);

                    context.Entry(autowheel).Property(x => x.Amount).IsModified = true;

                    context.SaveChanges();
                }
            }
        }


        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            using (TiresDbContext db = new())
            {
                DateTime dateOrder = DateTime.Now;
                
                foreach (var item in CartCollection.GetItems())
                {
                    db.SaleGood.Add(new SaleGood { Cost = item.Price, DateOrder = dateOrder, IdAutoTire = item.Id, Amount = 4 });
                    db.SaveChanges();
                }
            }

            List<string> receiptText = new List<string>();
            receiptText.Add("ШИНЫ.RU");
            receiptText.Add("г.Уфа, ул. Кирова, д 65\n\n");
            receiptText.Add($"Дата:{DateTime.Now:dd.MM.yyyy HH:mm:ss}\n");
            foreach (var item in CartCollection.GetItems())
            {
                receiptText.Add($"Товар: {item.TireName} = {item.Price:#.##} руб.");
                receiptText.Add($"x 4");
            }
            receiptText.Add($"\nИТОГО: {CartCollection.GetSum():#.##} руб.");
            receiptText.Add("Гарантийный срок 1 год");
            receiptText.Add("Сохраняйте чек для возврата");

            if (Directory.Exists($"{Environment.CurrentDirectory}\\Receipts") == false)
            {
                Directory.CreateDirectory($"{Environment.CurrentDirectory}\\Receipts");
            }
            string filePath = $"{Environment.CurrentDirectory}\\Receipts\\Receipt{DateTime.Now:dd.MM.yyyy HH.mm.ss}.doc";
            using (FileStream fs = File.Create(filePath))
            {
                fs.Close();
            }
            File.WriteAllLines(filePath, receiptText);

            new Process
            {
                StartInfo = new ProcessStartInfo(filePath)
                {
                    UseShellExecute = true
                }
            }.Start();
            CartCollection.ClearBye();
            MainDataGridView.ItemsSource = null;
        }

        private void Buttom_Amount_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}



