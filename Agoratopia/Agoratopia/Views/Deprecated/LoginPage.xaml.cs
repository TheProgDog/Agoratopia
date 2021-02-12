using Agoratopia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agoratopia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        void OnSaveButtonClicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        void OnDeleteButtonClicked(Object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }
}