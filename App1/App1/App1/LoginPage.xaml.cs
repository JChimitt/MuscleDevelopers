using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;


namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        static SQLiteConnection myDatabase;
        public LoginPage()
        {
            InitializeComponent();
            myDatabase = DependencyService.Get<IDatabase>().ConnectToDB();
            string userSelect = textUsername.Text;
            string passwordSelect = textPassword.Text;
            string usernameSelect = "";
            var loginSelection = myDatabase.Query<userTable>("");
            bool success = false;

            
            loginButton.Clicked += async (sender, e) =>
            {
                userSelect = textUsername.Text;
                
                

                
                try
                {
                    loginSelection = myDatabase.Query<userTable>("Select * FROM userTable WHERE Username =?", userSelect);
                    passwordSelect = loginSelection.FirstOrDefault().Password;
                    usernameSelect = loginSelection.FirstOrDefault().Username;
                    success = true;
                    }
                catch (NullReferenceException)
                {
                    //await DisplayAlert("Oops...", "Username or Password is incorrect!", "Ok");
                }
                finally
                {
                    if (success)
                    {
                        /*if (textUsername.Text == null || textPassword.Text == null)
                        {
                            await DisplayAlert("Oops...", "Username or Password is incorrect!", "Ok");
                        }
                        else */
                        
                        if (textUsername.Text == "admin" && textPassword.Text == "admin")
                        {
                            //Application.Current.Properties[textUsername.Text] = "admin";
                            await Navigation.PushAsync(new TabbedPage1(textUsername.Text));
                        }
                        else if (textUsername.Text == usernameSelect && textPassword.Text == passwordSelect)
                        {
                            //TabbedPage1 tabbedPage1 = new TabbedPage1();
                            //Application.Current.Properties[usernameSelect] = textUsername.Text;
                            await Navigation.PushAsync(new TabbedPage1(usernameSelect));
                        }
                        else
                        {
                            await DisplayAlert("Oops...", "Username or Password is incorrect!", "Ok");
                        }
                    }
                }
            };
            myRefreshView.Refreshing += async (sender, e) =>
            {
                textUsername.Text = "";
                textPassword.Text = "";
                loginSelection = myDatabase.Query<userTable>("");
                userSelect = null;
                passwordSelect = null;
                success = false;

                await Task.Delay(500);
                myRefreshView.IsRefreshing = false;
            };



        }



        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        
    }
    
}
