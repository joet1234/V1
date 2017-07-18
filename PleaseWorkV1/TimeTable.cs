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
using PleaseWorkV1.ListViewAdapters;

namespace PleaseWorkV1
{
    [Activity(Label = "TimeTable")]
    public class TimeTable : Activity
    {
        ClassInstance populateClass = new ClassInstance();
        UserInstance User = new UserInstance(); 
        GetConnectionClass Connect = new GetConnectionClass();

        ExpandableListViewAdapter myAdapater;
        ExpandableListView expandableListView;
        TextView StartMonday;

        List<string> group = new List<string>();
        Dictionary<string, List<ClassInstance>> dicMyMap = new Dictionary<string, List<ClassInstance>>();

  

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TimeTable);

            expandableListView = FindViewById<ExpandableListView>(Resource.Id.ListViewExpanded);
            StartMonday = FindViewById<TextView>(Resource.Id.TextViewStartDateMonday);

            // Populate User 
            User = JsonConvert.DeserializeObject<UserInstance>(Intent.GetStringExtra("User"));

            //Find Monday 
            DateTime TodaysDate = DateTime.Now;
            while (TodaysDate.DayOfWeek != DayOfWeek.Monday) TodaysDate = TodaysDate.AddDays(-1);

            StartMonday.Text = TodaysDate.ToLongDateString();

             //Set Data
            SetData(TodaysDate, out myAdapater);

            expandableListView.SetAdapter(myAdapater);

            expandableListView.ChildClick += (s, e) => {
                Toast.MakeText(this, "Clicked : " + myAdapater.GetChild(e.GroupPosition, e.ChildPosition), ToastLength.Short).Show();
            };
        }

        private void DateSelect_OnClick()
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime SelectedDate)
            {
                while (SelectedDate.DayOfWeek != DayOfWeek.Monday) SelectedDate = SelectedDate.AddDays(-1);
                StartMonday.Text = SelectedDate.ToLongDateString();

                //Set Data
                SetData(SelectedDate, out myAdapater);

            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void SetData(DateTime date, out ExpandableListViewAdapter mAdapter)
        {
            group.Add("Monday");
            group.Add("Tuesday");
            group.Add("Wednesday");
            group.Add("Thursday");
            group.Add("Friday");

            
            dicMyMap.Add(group[0], ListsPopulation(date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));

            date = date.AddDays(+1);
                    
            dicMyMap.Add(group[1], ListsPopulation(date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));

            date = date.AddDays(+1);
                        
            dicMyMap.Add(group[2], ListsPopulation(date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));

            date = date.AddDays(+1);

            dicMyMap.Add(group[3], ListsPopulation(date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));

            date = date.AddDays(+1);

            dicMyMap.Add(group[4], ListsPopulation(date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
            
            mAdapter = new ExpandableListViewAdapter(this, group, dicMyMap);

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
     
    }
}
