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

        List<string> group = new List<string>();
        Dictionary<string, List<string>> dicMyMap = new Dictionary<string, List<string>>();

  

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TimeTable);

            expandableListView = FindViewById<ExpandableListView>(Resource.Id.ListViewExpanded);
            

            // Populate User 
            User = JsonConvert.DeserializeObject<UserInstance>(Intent.GetStringExtra("User"));

            //Find Monday 
            DateTime TodaysDate = DateTime.Now;

            while (TodaysDate.DayOfWeek != DayOfWeek.Monday) TodaysDate = TodaysDate.AddDays(-1);

            // Find Current
            // MondaysList = ListsPopulation(TodaysDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            //Set Data
            SetData(out myAdapater);
            expandableListView.SetAdapter(myAdapater);

            expandableListView.ChildClick += (s, e) => {
                Toast.MakeText(this, "Clicked : " + myAdapater.GetChild(e.GroupPosition, e.ChildPosition), ToastLength.Short).Show();
            };


        }

        private void SetData(out ExpandableListViewAdapter mAdapter)
        {
            List<string> groupA = new List<string>();


            List<string> groupB = new List<string>();
            groupB.Add("B-1");
            groupB.Add("B-2");
            groupB.Add("B-3");

            List<string> groupC = new List<string>();
            groupC.Add("C-1");
            groupC.Add("C-2");
            groupC.Add("C-3");

            List<string> groupD = new List<string>();
            groupD.Add("D-1");
            groupD.Add("D-2");
            groupD.Add("D-3");

            List<string> groupE = new List<string>();
            groupE.Add("E-1");
            groupE.Add("E-2");
            groupE.Add("E-3");



            group.Add("Monday");
            group.Add("Tuesday");
            group.Add("Wednesday");
            group.Add("Thursday");
            group.Add("Friday");


            dicMyMap.Add(group[0], groupA);
            dicMyMap.Add(group[1], groupB);
            dicMyMap.Add(group[2], groupA);
            dicMyMap.Add(group[3], groupB);
            dicMyMap.Add(group[4], groupA);


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
