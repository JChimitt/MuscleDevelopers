using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;


namespace App1
{
    public partial class MainPage : ContentPage
    {
        static SQLiteConnection myDatabase;
        public MainPage()
        {
            InitializeComponent();

            //DATABASE CREATION SECTION
            myDatabase = DependencyService.Get<IDatabase>().ConnectToDB();

            // Creating the User Table
            myDatabase.CreateTable<userTable>();

            var user1 = new userTable { Username = "jchimitt", Password = "admin", FirstName = "Jacob", LastName = "Chimitt", Email = "chimitt@pnw.edu" };
            var user2 = new userTable { Username = "gstefanek", Password = "password", FirstName = "George", LastName = "Stefanek", Email = "stefanek@pnw.edu" };
            var user3 = new userTable { Username = "mcrowder", Password = "password", FirstName = "Madalynn", LastName = "Crowder", Email = "crowder@pnw.edu" };

            myDatabase.Insert(user1);
            myDatabase.Insert(user2);
            myDatabase.Insert(user3);

            Button tabbedPageNav;

            tabbedPageNav = new Button
            {
                Text = "Tabbed Page Navigation"

            };
            tabbedPageNav.Clicked += async (sender, args) =>
                await Navigation.PushAsync(new TabbedPage1());

            StackLayout stackLayout = new StackLayout
            {
                Spacing = 10,
                Children =
                {
                    tabbedPageNav
                },
            };

            ScrollView scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = stackLayout
            };

            // Accomodate iPhone status bar.                
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.Content = scrollView;

        }
    }



    
}
