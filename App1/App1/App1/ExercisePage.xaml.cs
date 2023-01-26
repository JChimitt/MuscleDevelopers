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
    public partial class ExercisePage : ContentPage
    {
        static SQLiteConnection myDatabase;

        public ExercisePage()
        {
            InitializeComponent();
        }
        public ExercisePage(string user)
        {
            InitializeComponent();
            myDatabase = DependencyService.Get<IDatabase>().ConnectToDB();

            var muscleSelection1 = myDatabase.Query<muscleTable>("SELECT * from muscleTable WHERE ID=1");
            var muscleSelection2 = myDatabase.Query<muscleTable>("SELECT * from muscleTable WHERE ID=2");
            var muscleSelection3 = myDatabase.Query<muscleTable>("SELECT * from muscleTable WHERE ID=3");
            var muscleSelection4 = myDatabase.Query<muscleTable>("SELECT * from muscleTable WHERE ID=4");
            var muscleSelection5 = myDatabase.Query<muscleTable>("SELECT * from muscleTable WHERE ID=5");
            var muscleSelection6 = myDatabase.Query<muscleTable>("SELECT * from muscleTable WHERE ID=6");
            var muscleSelection7 = myDatabase.Query<muscleTable>("SELECT * from muscleTable WHERE ID=7");
            var muscleSelection8 = myDatabase.Query<muscleTable>("SELECT * from muscleTable WHERE ID=8");
            var muscleSelection9 = myDatabase.Query<muscleTable>("SELECT * from muscleTable WHERE ID=9");
            var muscleSelection10 = myDatabase.Query<muscleTable>("SELECT * from muscleTable WHERE ID=10");
            var muscleSelection11 = myDatabase.Query<muscleTable>("SELECT * from muscleTable WHERE ID=11");
            var muscleSelection12 = myDatabase.Query<muscleTable>("SELECT * from muscleTable WHERE ID=12");

            var muscleList = new List<string>();
            muscleList.Add(muscleSelection1.First().PrimaryMuscle);
            muscleList.Add(muscleSelection2.First().PrimaryMuscle);
            muscleList.Add(muscleSelection3.First().PrimaryMuscle);
            muscleList.Add(muscleSelection4.First().PrimaryMuscle);
            muscleList.Add(muscleSelection5.First().PrimaryMuscle);
            muscleList.Add(muscleSelection6.First().PrimaryMuscle);
            muscleList.Add(muscleSelection7.First().PrimaryMuscle);
            muscleList.Add(muscleSelection8.First().PrimaryMuscle);
            muscleList.Add(muscleSelection9.First().PrimaryMuscle);
            muscleList.Add(muscleSelection10.First().PrimaryMuscle);
            muscleList.Add(muscleSelection11.First().PrimaryMuscle);
            muscleList.Add(muscleSelection12.First().PrimaryMuscle);

            musclePicker.ItemsSource = muscleList;

            musclePicker.SelectedIndexChanged += async (sender, e) =>
            {
                
                int selectedIndex = musclePicker.SelectedIndex;
                var musclePicked = new Label { IsVisible = false };
                var muscleSelection = myDatabase.Query<ExerciseTable>("");
                var musclePickedList = new List<string>();



                if (selectedIndex != -1)
                {
                    musclePicked.Text = (string)musclePicker.ItemsSource[selectedIndex];
                    muscleSelection = myDatabase.Query<ExerciseTable>("SELECT * from ExerciseTable WHERE PrimaryMuscle =?", musclePicked.Text);
                };

                int numberOfExercise = muscleSelection.Count();
                int i = 0;
                
                while (numberOfExercise > i)
                {
                    if (musclePickedList.Contains(muscleSelection.ElementAt(i).ExerciseName) == true)
                    {
                        i++;
                    }else
                        musclePickedList.Add(muscleSelection.ElementAt(i).ExerciseName);
                    i++;
                }

                /*var exerciseListingList = new List<string>();
                var lastExercise = exerciseSelection.ToArray();

                foreach (int i in exerciseListingList[])
                {

                }
                exerciseListingList.Add(exerciseSelection.First().PrimaryMuscle);*/

                exerciseList.ItemsSource = musclePickedList;
                
                //exerciseList.ItemsSource = myDatabase.Query<ExerciseTable>("SELECT * from ExerciseTable WHERE PrimaryMuscle =?", exercisePicked.Text);
                await DisplayAlert("You have selected:", musclePicked.Text, "Ok");
            };
            exerciseList.ItemTapped += async (sender, e) =>
            {
                
                bool deleteResult = await DisplayAlert("Exercise Selected", "Do you wish to view the description for this exercise?", "Yes", "No");
                bool deleteResultAns = false;
                deleteResultAns = deleteResult;
                if (deleteResultAns ==true)
                {
                    await Navigation.PushAsync(new ExerciseDescription(exerciseList.SelectedItem.ToString()));
                }

            };

        }
        
    }
}