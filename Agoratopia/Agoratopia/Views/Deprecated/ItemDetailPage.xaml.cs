using Agoratopia.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Agoratopia.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}