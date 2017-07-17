using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using MySql.Data.MySqlClient;
using PleaseWorkV1.DataSets;
using Newtonsoft.Json;

namespace PleaseWorkV1
{
    [Activity(Label = "PleaseWorkV1", MainLauncher = true, Icon = "@drawable/icon")]
    public class Login : Activity
    {
        public Database.GetConnectionClass Connection = new Database.GetConnectionClass();
        public UserInstance User = new UserInstance(); 

        public EditText username;
        public EditText password;
        public Button loginButton;
        public TextView forgotPass;

        public object GetConnection { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            //Link with axml 
            username = FindViewById<EditText>(Resource.Id.userName);
            password = FindViewById<EditText>(Resource.Id.password);
            loginButton = FindViewById<Button>(Resource.Id.loginButton);
            forgotPass = FindViewById<TextView>(Resource.Id.help_button);

            // set onclick listener here, by deleting some process
            loginButton.Click += delegate
            {
                LoginButtonClick();

            };

            forgotPass.Click += delegate
            {
                ForgotPassClick();
            };

        }

        private void ForgotPassClick()
        {
            Toast.MakeText(this, "Oops", ToastLength.Long).Show();
        }

        public void LoginButtonClick()
        {
            User.StudentNumber = username.Text.ToString();
            User.Password = password.Text;

            if (ValidateDetails() == true)
            {
                Toast.MakeText(this, "Move To Next", ToastLength.Long).Show();
                var Activity = new Intent(this, typeof(Menu));
                // Use JSON To Pass The User Object
                Activity.PutExtra("User", JsonConvert.SerializeObject(User));
                StartActivity(Activity);
            }
        }

        public bool ValidateDetails()
        {
            //Username Correct
            if (username.Text.Length != 0)
            {
                //Password Correct
                if (password.Text.Length != 0)
                {
                    if (Database.GetConnectionClass.CheckForInternetConnection() == true)
                    {
                       if (WithinDatabase(User.StudentNumber, User.Password) == true)
                        {
                            return true; 
                        }
                       else
                        {
                            Toast.MakeText(this, "Details Do Not Exist Within Database", ToastLength.Short).Show();
                            return false; 
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Please Check Internet Connection And Try Again", ToastLength.Short).Show();
                        return false;
                    }
                }
                //Password Incorrect Format
                else
                {
                    Toast.MakeText(this, "Password Must Be Five Or More Characters And Cannot Be Blank", ToastLength.Short).Show();
                    return false;
                }
            }
            //Username Incorrect Format
            else
            {
                Toast.MakeText(this, "Username Must Be Eight Characters And Cannot Be Blank", ToastLength.Short).Show();
                return false;
            }
        }

        public bool WithinDatabase(string StudentNumber, string Password)
        {
            // Type equals student as well once included 
            string query = "SELECT count(StudentNumber) FROM students WHERE StudentNumber = " + StudentNumber + " AND Password = " + "'" + Password + "';";
            int Count = -1;

            //Create Mysql Command
            using (MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection()))
            {
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            
                if (mySqlDataReader.Read())
                {
                    Count = mySqlDataReader.GetInt32(0);
                }

            }

            if (Count > 0)
            {
                GetStudentDetails(StudentNumber);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GetStudentDetails (string StudentNumber) 
        {
            string sql = "SELECT * FROM students WHERE StudentNumber = '" + StudentNumber + "';";
            
            using (MySqlCommand cmd = new MySqlCommand(sql, Connection.GetConnection()))
            {
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //UserInstance u = new UserInstance(reader["StudentNumber"].ToString(), reader["Password"].ToString(), reader.GetInt32("IntakeYear"), reader["Email"].ToString(), reader["Cohort"].ToString(), reader["Name"].ToString());
                    User.IntakeYear = User.FindSchoolYear(reader.GetInt32("IntakeYear"));
                    User.Email = reader["StudentEmail"].ToString();
                    User.Cohort = reader["Cohort"].ToString();
                    User.Name = reader["Name"].ToString();
                }
            }
        }   
    }
}


