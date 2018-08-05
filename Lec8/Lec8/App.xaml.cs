using Lec8.Persistence;
using Lec8.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Lec8
{
	public partial class App : Application
	{

        private const string TitleKey = "Name";
        private const string NotificationsKey = "NotificationsEnabled";
        static ItemDatabase database;

        public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new ContPag());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        /**
        public string Title
        {
            get
            {
                if (Properties.ContainsKey(TitleKey))
                {
                    return Properties[TitleKey].ToString();
                }

                return "";
            }
            set
            {
                if (value != null)
                {
                    Properties[TitleKey] = value;
                }
            }
        }

        public bool NotificationsEnabled
        {
            get
            {
                if (Properties.ContainsKey(NotificationsKey))
                {
                    return (bool)Properties[NotificationsKey];
                }
                return false;
            }

            set
            {
                    Properties[NotificationsKey] = value;
            }
        } **/

    }
}
