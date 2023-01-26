using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;

namespace App1
{
    public partial class App : Application
    {
        static SQLiteConnection myDatabase;
        public App()
        {
            InitializeComponent();

            //DATABASE CREATION SECTION
            myDatabase = DependencyService.Get<IDatabase>().ConnectToDB();

            // Creating the User Table
            myDatabase.CreateTable<userTable>();
            

            var user1 = new userTable { Username = "jchimitt", Password = "admin", FirstName = "Jacob", LastName = "Chimitt", Email = "chimitt@pnw.edu", CurrentWeight="160", GoalWeight="185", 
                Notes="Jacob likes to weightlift.", Age="24", Height="73", Experience="Intermediate", Sex="Male" };
            var user2 = new userTable { Username = "gstefanek", Password = "password", FirstName = "George", LastName = "Stefanek", Email = "stefanek@pnw.edu", Experience="Master", Notes="Can deadlift at least 1000lbs easy peasy." };
            var user3 = new userTable { Username = "mcrowder", Password = "password", FirstName = "Madalynn", LastName = "Crowder", Email = "crowder@pnw.edu" };

            myDatabase.Insert(user1);
            myDatabase.Insert(user2);
            myDatabase.Insert(user3);

            //Creating muscle groups
            myDatabase.CreateTable<muscleTable>();

            var muscleCategory1 = new muscleTable { PrimaryMuscle = "Triceps" };
            var muscleCategory2 = new muscleTable { PrimaryMuscle = "Chest" };
            var muscleCategory3 = new muscleTable { PrimaryMuscle = "Shoulder" };
            var muscleCategory4 = new muscleTable { PrimaryMuscle = "Core" };
            var muscleCategory5 = new muscleTable { PrimaryMuscle = "Back" };
            var muscleCategory6 = new muscleTable { PrimaryMuscle = "Biceps" };
            var muscleCategory7 = new muscleTable { PrimaryMuscle = "Forearms" };
            var muscleCategory8 = new muscleTable { PrimaryMuscle = "Upper Legs" };
            var muscleCategory9 = new muscleTable { PrimaryMuscle = "Glutes" };
            var muscleCategory10 = new muscleTable { PrimaryMuscle = "Lower Legs" };
            var muscleCategory11 = new muscleTable { PrimaryMuscle = "Cardio" };
            var muscleCategory12 = new muscleTable { PrimaryMuscle = "All" };

            myDatabase.Insert(muscleCategory1);
            myDatabase.Insert(muscleCategory2);
            myDatabase.Insert(muscleCategory3);
            myDatabase.Insert(muscleCategory4);
            myDatabase.Insert(muscleCategory5);
            myDatabase.Insert(muscleCategory6);
            myDatabase.Insert(muscleCategory7);
            myDatabase.Insert(muscleCategory8);
            myDatabase.Insert(muscleCategory9);
            myDatabase.Insert(muscleCategory10);
            myDatabase.Insert(muscleCategory11);
            myDatabase.Insert(muscleCategory12);

            //Create Exercise table
            
            myDatabase.CreateTable<ExerciseTable>();

            //Tricep exercises
            var exerciseTricep1 = new ExerciseTable { ExerciseName = "Dip", PrimaryMuscle = "Triceps", SecondaryMuscle = "Chest", Description = "This exercise is focused on working your triceps and chest muscles" +
                " as you lift yourself up and down." };
            var exerciseTricep2 = new ExerciseTable
            {
                ExerciseName = "Tricep Pushup",
                PrimaryMuscle = "Triceps",
                SecondaryMuscle = "Chest",
                Description = "This pushup involves using your hands close together to isolate the tricep muscles."
            };
            var exerciseTricep3 = new ExerciseTable
            {
                ExerciseName = "Cable Tricep Extension",
                PrimaryMuscle = "Triceps",
                SecondaryMuscle = "",
                Description = "This exercise works the tricep muscles by extending your arms behind your neck."
            };

            //Glute Exercises
            var exerciseGlute1 = new ExerciseTable
            {
                ExerciseName = "Flutter Kick",
                PrimaryMuscle = "Glutes",
                SecondaryMuscle = "Upper Leg",
                Description = "Flutter Kicks strenghten the hamstrings and muscles on the back of the thigh."
            };
            var exerciseGlute2 = new ExerciseTable
            {
                ExerciseName = "Kneeling Squat",
                PrimaryMuscle = "Glutes",
                SecondaryMuscle = "Lower Leg",
                Description = "This squat variation helps shape and tone the glute muscles."
            };
            var exerciseGlute3 = new ExerciseTable
            {
                ExerciseName = "Lateral Lunge",
                PrimaryMuscle = "Glutes",
                SecondaryMuscle = "Upper Leg",
                Description = "This lunge variation helps shape and tone the glute muscles."
            };


            myDatabase.Insert(exerciseTricep1);
            myDatabase.Insert(exerciseTricep2);
            myDatabase.Insert(exerciseTricep3);
            myDatabase.Insert(exerciseGlute1);
            myDatabase.Insert(exerciseGlute2);
            myDatabase.Insert(exerciseGlute3);

            //Create Routines
            myDatabase.CreateTable<routineTable>();

            var jacobRoutine1 = new routineTable { username = "jchimitt", routine = "Leg Day", exercise = "Lateral Lunge", weight = 20, reps="3x25" };
            var jacobRoutine2 = new routineTable { username = "jchimitt", routine = "Leg Day", exercise = "Kneeling Squat", weight = 100, reps = "3x12" };
            var jacobRoutine3 = new routineTable { username = "jchimitt", routine = "Chest, Shoulders, and Triceps", exercise = "Tricep Pushup", weight = 0, reps = "4x20" };
            var jacobRoutine4 = new routineTable { username = "jchimitt", routine = "Chest, Shoulders, and Triceps", exercise = "Dip", weight = 0, reps = "4x8" };
            var jacobRoutine5 = new routineTable { username = "jchimitt", routine = "Back, Biceps, and Abs", exercise = "Crunches", weight = 0, reps = "4x25" };

            myDatabase.Insert(jacobRoutine1);
            myDatabase.Insert(jacobRoutine2);
            myDatabase.Insert(jacobRoutine3);
            myDatabase.Insert(jacobRoutine4);
            myDatabase.Insert(jacobRoutine5);

            myDatabase.CreateTable<UserRoutineTable>();

            var jacobRoutineName1 = new UserRoutineTable { username = "jchimitt", routine = "Back, Biceps, and Abs" };
            var jacobRoutineName2 = new UserRoutineTable { username = "jchimitt", routine = "Chest, Shoulders, and Triceps" };
            var jacobRoutineName3 = new UserRoutineTable { username = "jchimitt", routine = "Leg Day" };

            myDatabase.Insert(jacobRoutineName1);
            myDatabase.Insert(jacobRoutineName2);
            myDatabase.Insert(jacobRoutineName3);



            //MainPage = new NavigationPage(new MainPage());
            MainPage = new NavigationPage(new LoginPage());
            //this is a test below me
            //MainPage = new NavigationPage(new WorkoutRoutines());
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

    public class userTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(15)]
        public string Username { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string CurrentWeight { get; set; }
        public string GoalWeight { get; set; }
        public string Height { get; set; }
        public string Age { get; set; }
        public string Experience { get; set; }
        public string Sex { get; set; }
        public string Notes { get; set; }


    }

    public class muscleTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(15)]
        public string PrimaryMuscle { get; set; }

        

    }

    public class routineTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(15)]
        public string username { get; set; }
        public string routine { get; set; }
        public string exercise { get; set; }
        public int weight { get; set; }
        public string reps { get; set; }

    }
    public class UserRoutineTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(15)]
        public string username { get; set; }
        public string routine { get; set; }
    }
    

    public class ExerciseTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(15)]
        public string ExerciseName { get; set; }
        
        public string PrimaryMuscle { get; set; }

        public string SecondaryMuscle { get; set; }

        public string Description { get; set; }

    }

    public class ListItem
    {
        public string Title { get; set; }
    }

    
    public class ListItemCell : ViewCell
    {
        public ListItemCell()
        {

            Label titleLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 20,
                FontAttributes = Xamarin.Forms.FontAttributes.Bold,
                TextColor = Color.White
            };
            titleLabel.SetBinding(Label.TextProperty, "Title");




            StackLayout viewLayoutItem = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                WidthRequest = 120,
                Children = { titleLabel, }
            };


            StackLayout viewLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(5, 10, 5, 15),
                Children = { viewLayoutItem }
            };

            View = viewLayout;
        }

    }
}
