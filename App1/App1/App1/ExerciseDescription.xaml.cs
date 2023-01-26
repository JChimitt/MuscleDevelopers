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
    public partial class ExerciseDescription : ContentPage
    {
        static SQLiteConnection myDatabase;
        public ExerciseDescription()
        {
            InitializeComponent();
        }
        public ExerciseDescription(string exerciseName)
        {
            InitializeComponent();
            myDatabase = DependencyService.Get<IDatabase>().ConnectToDB();
            exerciseName.Replace(" ", string.Empty);
            var exerciseSelection = myDatabase.Query<ExerciseTable>("SELECT * from ExerciseTable WHERE ExerciseName=?", exerciseName);
            exerciseNameLabel.Text = exerciseNameLabel.Text + exerciseSelection.First().ExerciseName;
            primaryMuscleLabel.Text = primaryMuscleLabel.Text + exerciseSelection.First().PrimaryMuscle;
            secondaryMuscleLabel.Text = secondaryMuscleLabel.Text + exerciseSelection.First().SecondaryMuscle;
            descriptionLabel.Text = descriptionLabel.Text + exerciseSelection.First().Description;



            string exerciseGifName = exerciseName.Replace(" ", string.Empty);
            exerciseGif.Source = exerciseGifName + (".gif");
            
        }
    }
}