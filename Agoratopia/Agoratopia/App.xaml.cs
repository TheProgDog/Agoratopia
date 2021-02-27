using Agoratopia.Services;
using Agoratopia.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;

namespace Agoratopia
{
    public partial class App : Application
    {

        public static String EntryPath { get; private set; }

        public App()
        {
            InitializeComponent();

            EntryPath = null;

            //DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new TestPage());
        }

        public App(string completePath)
        { 
            InitializeComponent();

            EntryPath = completePath;

            //DependencyService.Register<MockDataStore>();

            using (SQLiteConnection conn = new SQLiteConnection(EntryPath))
            {
                if (conn.GetTableInfo("Settings").Count == 0)
                    MainPage = new NavigationPage(new TierSetting());
                else
                    MainPage = new NavigationPage(new TestPage());

                conn.Close();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
