using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutPage : ContentPage
    {
        static SQLiteConnection myDatabase;
        Stopwatch stopwatch;
        public WorkoutPage()
        {
            InitializeComponent();
        }
        public WorkoutPage(string user, string routine)
        {
            
            InitializeComponent();
            stopwatch = new Stopwatch();
            stopwatchLabel.Text = "00:00:00.00000";
            myDatabase = DependencyService.Get<IDatabase>().ConnectToDB();
            var routineSelection = myDatabase.Query<routineTable>("");
            if (myDatabase.Query<routineTable>("SELECT * from routineTable WHERE username=? AND routine=?", user, routine) == null)
            {
                DisplayAlert("Notification!", "This routine has no exercises currently inside of it.", "Ok");
            }
            else
            {
                routineSelection = myDatabase.Query<routineTable>("SELECT * from routineTable WHERE username=? AND routine=?", user, routine);
                //TestLabel.Text = routineSelection.First().exercise;
                var numberOfExerciseList = new List<string>();
                int numberOfExercise = routineSelection.Count();
                int i = 0;

                while (numberOfExercise > i)
                {
                    if (numberOfExerciseList.Contains(routineSelection.ElementAt(i).exercise) == true)
                    {
                        i++;
                    }
                    else
                        numberOfExerciseList.Add(routineSelection.ElementAt(i).exercise);
                    i++;
                }

                exerciseList.ItemsSource = numberOfExerciseList;

                //workoutList.exerciseLabel.Text = routineSelection.First().exercise;

            }
            /*if (routine != null)
            {
                routineSelection = myDatabase.Query<routineTable>("SELECT * from routineTable WHERE username=? AND routine=?", user, routine);
            }*/
            

        }

        private void resetButton_Clicked(object sender, EventArgs e)
        {
            startButton.Text = "Start";
            stopwatchLabel.Text = "00:00:00.00000";
            stopwatch.Reset();
        }

        private void stopButton_Clicked(object sender, EventArgs e)
        {
            startButton.Text = "Resume";
            
            stopwatch.Stop();
        }

        private void startButton_Clicked(object sender, EventArgs e)
        {
            startButton.Text = "Start";
            if (!stopwatch.IsRunning)
            {
                stopwatch.Start();
                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    stopwatchLabel.Text = stopwatch.Elapsed.ToString();
                    
                    if (!stopwatch.IsRunning)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                );
            }
        }
    }
}