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
    public partial class CardTireView : UserControl, ICardType
    {
        private SearchTiresPage page;
        private IDbClass _item;
        public CardTireView(IDbClass sourceObject, SearchTiresPage page)
        {
            InitializeComponent();
            _item = sourceObject;
            CardMainGrid.DataContext = sourceObject;
            this.page = page;


        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            CartCollection.AddItem((InnerTiresWheelClass)_item);
            page.LoadItemsToMainWrapPanel();






        }
    }
}
