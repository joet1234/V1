using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using PleaseWorkV1.DataSets;
using Newtonsoft.Json;

namespace PleaseWorkV1
{
    [Activity(Label = "Menu")]
    public class Menu : Activity
    {
        public UserInstance User; 
        public String studentNumber;
        public Button timeTableButton;
        public int Year;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Menu);
            
            //Assign Buttons class wide variables 
            timeTableButton = FindViewById<Button>(Resource.Id.timetableBTN);
            // Populate User 
            User = JsonConvert.DeserializeObject<UserInstance>(Intent.GetStringExtra("User"));
            
            // set onclick listener here, by deleting some process
            timeTableButton.Click += delegate
            {
                TimeTableButtonClick();
            };
        }

        private void TimeTableButtonClick()
        {
            
            if (User.IntakeYear == 3)
            {
                var MoveToTimeTable = new Intent(this, typeof(PlacementYear));
                MoveToTimeTable.PutExtra("User", JsonConvert.SerializeObject(User));                
                StartActivity(MoveToTimeTable);
            }
            else
            {
                var MoveToTimeTable = new Intent(this, typeof(TimeTable));
                MoveToTimeTable.PutExtra("User", JsonConvert.SerializeObject(User));
                StartActivity(MoveToTimeTable);
            }
        }
    }
}