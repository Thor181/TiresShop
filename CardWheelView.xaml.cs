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
using TiresShopApplication.Views;

namespace TiresShopApplication
{
    /// <summary>
    /// Логика взаимодействия для CardWheelView.xaml
    /// </summary>
    public partial class CardWheelView : UserControl, ICardType
    {
        private SearchWheelPage page;
        private IDbClass _item;
        public CardWheelView(IDbClass sourceObject, SearchWheelPage page)
        {
            InitializeComponent();
            _item = sourceObject;
            CardMainGridWheel.DataContext = sourceObject;
            this.page = page;
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            CartCollection.AddItem((InnerTiresWheelClass)_item);
            page.LoadItemsToMainWrapPanel();

        }
    }
}

