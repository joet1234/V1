using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PleaseWorkV1.Datasets;
using MySql.Data.MySqlClient;
using PleaseWorkV1.Database;
using System.Globalization;
using PleaseWorkV1.DataSets;
using Newtonsoft.Json;

namespace PleaseWorkV1
{
    [Activity(Label = "TimeTable")]
    public class TimeTable : Activity
    {
        ClassInstance populateClass = new ClassInstance();
        UserInstance User = new UserInstance(); 
        GetConnectionClass Connect = new GetConnectionClass();

        //Create Instance of the Class Display Object 
        ListView MondaylistOfClass;
        ListView NotesListOfClass;
        TextView dateDisplay;
        Button selectDate;
        List<ClassInstance> MondaysList = new List<ClassInstance>();
        List<ClassInstance> NotesList = new List<ClassInstance>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TimeTable);

            MondaylistOfClass = FindViewById<ListView>(Resource.Id.MondayList);
            NotesListOfClass = FindViewById<ListView>(Resource.Id.NotesList);
            dateDisplay = FindViewById<TextView>(Resource.Id.date);
            selectDate = FindViewById<Button>(Resource.Id.SelectDate);

            // Populate User 
            User = JsonConvert.DeserializeObject<UserInstance>(Intent.GetStringExtra("User"));

            //Find Monday 
            DateTime TodaysDate = DateTime.Now;

            while (TodaysDate.DayOfWeek != DayOfWeek.Monday) TodaysDate = TodaysDate.AddDays(-1);
            dateDisplay.Text = TodaysDate.ToLongDateString();

            //Find Current
            MondaysList = ListsPopulation(TodaysDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));

            ListOfNotes(TodaysDate);

            PopulateListsView(MondaysList);

            //If the button clicked then a popup fragment appears allowing the user to select date
            selectDate.Click += delegate
            {
                DateSelect_OnClick();
            };


        }
        
        private void PopulateListsView(List<ClassInstance> Temp)
        {
            ListViewTimeTableAdapter myAdapter = new ListViewTimeTableAdapter(this, Temp);

            MondaylistOfClass.Adapter = myAdapter;
        }

        private void PopulateListViewNotes(List<ClassInstance> Temp)
        {
            NotesListViewAdapter myAdapter = new NotesListViewAdapter(this, Temp);

            NotesListOfClass.Adapter = myAdapter;
        }

        private void ListOfNotes(DateTime Date)
        {
            for (int i = 0; i <= 6; i++)
            {
                NotesList.AddRange(FindNotesForDay(Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
                Date = Date.AddDays(+1);
            }

            PopulateListViewNotes(NotesList);
        }

        private List<ClassInstance> FindNotesForDay(String date)
        {
            string sql = "SELECT *  FROM  lectures join groupConvert using (Groups) where StartDate = '" + date + "' AND Year = '" + User.IntakeYear.ToString() + "' AND Cohort = '" + User.Cohort + "' AND Notes IS NOT NULL";

            List<ClassInstance> Temp = new List<ClassInstance>();

            using (MySqlCommand cmd = new MySqlCommand(sql, Connect.GetConnection()))
            {

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClassInstance c = new ClassInstance(reader["StartDate"].ToString(), reader["StartTime"].ToString(), reader["Subject"].ToString(), reader["Notes"].ToString());
                    Temp.Add(c);
                }
            }
            return Temp;
        }

            private List<ClassInstance> ListsPopulation(String date)
        {
            string sql = "SELECT *  FROM  lectures join groupConvert using (Groups) where StartDate = '" + date + "' AND Year = '" + User.IntakeYear.ToString() + "' AND Cohort = '" + User.Cohort + "' ";

            List<ClassInstance> Temp = new List<ClassInstance>();

            using (MySqlCommand cmd = new MySqlCommand(sql, Connect.GetConnection()))
            {

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClassInstance c = new ClassInstance(reader["StartTime"].ToString(), reader["EndTime"].ToString(), reader["Subject"].ToString(), reader["Location"].ToString(), reader["Essential"].ToString());
                    Temp.Add(c);
                }
            }
            return Temp;
        }
       
        private void DateSelect_OnClick()
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime SelectedDate)
            {
                while (SelectedDate.DayOfWeek != DayOfWeek.Monday) SelectedDate = SelectedDate.AddDays(-1);
                dateDisplay.Text = SelectedDate.ToLongDateString();

                MondaysList = ListsPopulation(SelectedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                
                PopulateListsView(MondaysList);
                

            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
    }
}
