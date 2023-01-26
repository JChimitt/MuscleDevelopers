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
    //[XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class WorkoutRoutines : ContentPage
    {
        static SQLiteConnection myDatabase;
        public WorkoutRoutines()
        {
            InitializeComponent();
            //textUsername.Text = Application.Current.Properties[].ToString();


        }
        public WorkoutRoutines(string user)
        {
            InitializeComponent();
            //textUsername.Text = Application.Current.Properties[].ToString();
            //textUsername.Text = user;
            myDatabase = DependencyService.Get<IDatabase>().ConnectToDB();

            var routineSelection = myDatabase.Query<UserRoutineTable>("SELECT * from UserRoutineTable WHERE username=?",user);

            int numberOfRoutines = routineSelection.Count();
            int i = 0;
            var totalRoutineList = new List<string>();

            while (numberOfRoutines > i)
            {
                if (totalRoutineList.Contains(routineSelection.ElementAt(i).routine) == true)
                {
                    i++;
                }
                else
                    totalRoutineList.Add(routineSelection.ElementAt(i).routine);
                i++;
            }

            //routineList.ItemsSource = new string[] { "Routine 1", "Routine 2", "Routine 3", "Routine 4", "Routine 5" };
            routineList.ItemsSource = totalRoutineList;

            routineList.ItemTapped += async (sender, e) =>
             {
                 
                 
                 newRoutineSelection.routineSelection = routineList.SelectedItem.ToString();
                 await DisplayAlert("You have selected:", newRoutineSelection.routineSelection, "Ok");
                
             };

            EditButton.Clicked += async (sender, e) =>
            {
                if (routineList.SelectedItem == null)
                {
                    await DisplayAlert("Error", "Please select the routine you wish to edit!", "Ok");
                    return;
                }
                

                string editResult = await DisplayPromptAsync("Edit Routine", "What would you like to call this routine?");
                string editResultAns = "";
                
                
                    editResultAns = editResult;
                //testLabel.Text = editResultAns;
                    myDatabase.Query<UserRoutineTable>("UPDATE UserRoutineTable SET routine = ? WHERE routine = ?", editResultAns, routineList.SelectedItem );
                    routineSelection = myDatabase.Query<UserRoutineTable>("SELECT * from UserRoutineTable WHERE username=?", user);

                    numberOfRoutines = routineSelection.Count();
                    i = 0;
                    totalRoutineList = new List<string>();
                    i = 0;

                while (numberOfRoutines > i)
                {
                    if (totalRoutineList.Contains(routineSelection.ElementAt(i).routine) == true)
                    {
                        i++;
                    }
                    else
                        totalRoutineList.Add(routineSelection.ElementAt(i).routine);
                    i++;
                }

                //routineList.ItemsSource = new string[] { "Routine 1", "Routine 2", "Routine 3", "Routine 4", "Routine 5" };
                routineList.ItemsSource = totalRoutineList;
                editResultAns = "";
                routineList.SelectedItem = null;
            };

            AddButton.Clicked += async (sender, e) =>
            {
                string addResult = await DisplayPromptAsync("Add Routine", "What would you like to call this routine?");
                string addResultAns = "";
                
                    addResultAns = addResult;
                if (addResultAns == null)
                {
                    await DisplayAlert("Error", "You have to enter a name for the routine you wish to add.", "Ok");
                }
                else
                    //testLabel.Text = editResultAns;
                    myDatabase.Query<UserRoutineTable>("INSERT INTO UserRoutineTable (username, routine) VALUES (?, ?)", user, addResultAns);
                    routineSelection = myDatabase.Query<UserRoutineTable>("SELECT * from UserRoutineTable WHERE username=?", user);

                    numberOfRoutines = routineSelection.Count();
                
                    totalRoutineList = new List<string>();
                    i = 0;

                while (numberOfRoutines > i)
                {
                    if (totalRoutineList.Contains(routineSelection.ElementAt(i).routine) == true)
                    {
                        i++;
                    }
                    else
                        totalRoutineList.Add(routineSelection.ElementAt(i).routine);
                    i++;
                }

                //routineList.ItemsSource = new string[] { "Routine 1", "Routine 2", "Routine 3", "Routine 4", "Routine 5" };
                routineList.ItemsSource = totalRoutineList;
                addResultAns = "";
                routineList.SelectedItem = null;
            };

            DeleteButton.Clicked += async (sender, e) =>
            {
                if (routineList.SelectedItem == null)
                {
                    await DisplayAlert("Error", "You have to select a routine to delete it.", "Ok");
                    return;
                }
                
                bool deleteResult = await DisplayAlert("Delete Routine", "Are you sure you wish to delete this?", "Yes", "No");
                bool deleteResultAns = false;
                string deleteChecker = deleteResult.ToString();

                deleteResultAns = deleteResult;
                
                if (deleteResultAns == true)
                    //testLabel.Text = editResultAns;
                    myDatabase.Query<UserRoutineTable>("DELETE FROM UserRoutineTable WHERE routine = ? ", routineList.SelectedItem );
                routineSelection = myDatabase.Query<UserRoutineTable>("SELECT * from UserRoutineTable WHERE username=?", user);

                numberOfRoutines = routineSelection.Count();

                totalRoutineList = new List<string>();
                i = 0;

                while (numberOfRoutines > i)
                {
                    if (totalRoutineList.Contains(routineSelection.ElementAt(i).routine) == true)
                    {
                        i++;
                    }
                    else
                        totalRoutineList.Add(routineSelection.ElementAt(i).routine);
                    i++;
                }

                //routineList.ItemsSource = new string[] { "Routine 1", "Routine 2", "Routine 3", "Routine 4", "Routine 5" };
                routineList.ItemsSource = totalRoutineList;
                deleteResultAns = false;
                routineList.SelectedItem = null;
            };
            //var workoutSelection = myDatabase.Query<routineTable>("");
            WorkoutButton.Clicked += async (sender, e) =>
            {
                //var workoutList = routineList;
                //int selectedIndex = workoutList[i];
                
                if (routineList.SelectedItem == null)
                {
                    await DisplayAlert("Error", "You have to select a routine to workout.", "Ok");
                    return;
                }
                
                //var routineSelection = myDatabase.Query<routineTable>("SELECT * from routineTable WHERE username=? AND routine=?", user, routineList.SelectedItem);
                await Navigation.PushAsync(new WorkoutPage(user, routineList.SelectedItem.ToString()));
            };

        }


        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void routineList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            

        }
        static class newRoutineSelection
        {
            public static string routineSelection;
        }

        private void WorkoutButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("This is the routine selected!", newRoutineSelection.routineSelection, "ok");
        }

        

        private void AddButton_Clicked(object sender, EventArgs e)
        {
           
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
