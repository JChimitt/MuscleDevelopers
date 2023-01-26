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
    public partial class UserProfile : ContentPage
    {
        static SQLiteConnection myDatabase;

        public UserProfile()
        {
            InitializeComponent();
        }
        public UserProfile(string user)
        {
            InitializeComponent();
            myDatabase = DependencyService.Get<IDatabase>().ConnectToDB();
            var userSelection = myDatabase.Query<userTable>("Select * FROM userTable WHERE Username =?", user);
            usernameLabel.Text = userSelection.FirstOrDefault().Username;
            firstNameLabel.Text = userSelection.FirstOrDefault().FirstName;
            lastNameLabel.Text = userSelection.FirstOrDefault().LastName;
            emailLabel.Text = userSelection.FirstOrDefault().Email;
            sexLabel.Text = userSelection.FirstOrDefault().Sex;
            experienceLabel.Text = userSelection.FirstOrDefault().Experience;
            weightLabel.Text = userSelection.FirstOrDefault().CurrentWeight;
            goalWeightLabel.Text = userSelection.FirstOrDefault().GoalWeight;
            heightLabel.Text = userSelection.FirstOrDefault().Height;
            ageLabel.Text = userSelection.FirstOrDefault().Age;
            noteLabel.Text = userSelection.FirstOrDefault().Notes;

        }

        private void EditButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Edit Button", "You may now insert your updated information", "Ok");
            usernameLabel.IsReadOnly = false;
            firstNameLabel.IsReadOnly = false;
            lastNameLabel.IsReadOnly = false;
            emailLabel.IsReadOnly = false;
            sexLabel.IsReadOnly = false;
            experienceLabel.IsReadOnly = false;
            weightLabel.IsReadOnly = false;
            goalWeightLabel.IsReadOnly = false;
            heightLabel.IsReadOnly = false;
            ageLabel.IsReadOnly = false;
            noteLabel.IsReadOnly = false;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Save Button", "Your profile has now been updated.", "Ok");
            usernameLabel.IsReadOnly = true;
            firstNameLabel.IsReadOnly = true;
            lastNameLabel.IsReadOnly = true;
            emailLabel.IsReadOnly = true;
            sexLabel.IsReadOnly = true;
            experienceLabel.IsReadOnly = true;
            weightLabel.IsReadOnly = true;
            goalWeightLabel.IsReadOnly = true;
            heightLabel.IsReadOnly = true;
            ageLabel.IsReadOnly = true;
            noteLabel.IsReadOnly = true;

            myDatabase.Query<userTable>("UPDATE userTable SET Username = ?, FirstName = ?, LastName = ?, Email = ?, Sex = ?, Experience = ?, CurrentWeight = ?, GoalWeight = ?, Height = ?, Age = ?, Notes = ?",
                usernameLabel.Text, firstNameLabel.Text, lastNameLabel.Text, emailLabel.Text, sexLabel.Text, experienceLabel.Text, weightLabel.Text, goalWeightLabel.Text, heightLabel.Text, ageLabel.Text, noteLabel.Text);
        }
    }
}