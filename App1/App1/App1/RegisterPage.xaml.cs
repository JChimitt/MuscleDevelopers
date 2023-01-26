using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        static SQLiteConnection myDatabase;
        public RegisterPage()
        {
            InitializeComponent();
           
            myDatabase = DependencyService.Get<IDatabase>().ConnectToDB();
            string userSelect = textUsername.Text;
            string passwordSelect = textPassword.Text;
            string usernameSelect = "";
            var loginSelection = myDatabase.Query<userTable>("");
            bool success = false;


            registerButton.Clicked += async (sender, e) =>
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

                        if (textUsername.Text == "admin" & textPassword.Text == "admin")
                        {
                            //Application.Current.Properties[textUsername.Text] = "admin";
                            await DisplayAlert("Error", "This username already exists.", "Ok");
                            
                        }
                        else if (textUsername.Text == usernameSelect & textPassword.Text == passwordSelect)
                        {
                            //TabbedPage1 tabbedPage1 = new TabbedPage1();
                            //Application.Current.Properties[usernameSelect] = textUsername.Text;
                            await DisplayAlert("Error", "That username is already taken.", "Ok");
                            
                        }
                        else if(textUsername.Text == "" ^ textPassword.Text == "")
                        {
                            await DisplayAlert("Oops...", "Either the username or password is empty.", "Ok");
                        }
                        else if (textUsername.Text == "Username" & textPassword.Text == "Password")
                        {
                            //Application.Current.Properties[textUsername.Text] = "admin";
                            await DisplayAlert("Error", "You can't use either that username or password.", "Ok");
                        }
                        else
                        {
                            myDatabase.Query<userTable>("INSERT INTO UserTable (Username, Password) VALUES (?, ?)", textUsername.Text, textPassword.Text );
                            await DisplayAlert("Congrats!", "Your account has been created!", "Ok");
                        }
                    }
                }
            };
        }
    }
}