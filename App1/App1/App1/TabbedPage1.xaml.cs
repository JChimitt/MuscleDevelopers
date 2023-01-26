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
    public partial class TabbedPage1 : TabbedPage
    {
        public TabbedPage1()
        {
            this.Title = "Tabbed Navigation";
            this.Children.Add(new WorkoutRoutines());
            this.Children.Add(new ExercisePage());
            this.Children.Add(new UserProfile());
        }

        public TabbedPage1(string User)
        {
            InitializeComponent();
            this.Title = "";
            this.Children.Add(new WorkoutRoutines(User));
            this.Children.Add(new ExercisePage(User));
            this.Children.Add(new UserProfile(User));
        }

    }
}