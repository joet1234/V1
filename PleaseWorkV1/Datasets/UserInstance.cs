using System;

namespace PleaseWorkV1.DataSets
{
    public class UserInstance
    {
        public String StudentNumber { set; get; }
        public String Password { set; get; }
        public int IntakeYear { set; get; }
        public String Email { set; get; }
        public String Cohort { set; get; }
        public String Name { set; get; }
        
        public UserInstance()
        {

        }

        public UserInstance(String StudentNumber)
        {
            this.StudentNumber = StudentNumber;
        }

        public UserInstance(String StudentNumber, String Password)
        {
            this.StudentNumber = StudentNumber;
            this.Password = Password;
        }

        public UserInstance(String StudentNumber, String Password, int IntakeYear, String Email, String Cohort, String Name)
        {
            this.StudentNumber = StudentNumber;
            this.Password = Password;
            this.IntakeYear = FindSchoolYear(IntakeYear);
            this.Email = Email;
            this.Cohort = Cohort;
            this.Name = Name; 
        }

        public int FindSchoolYear(int intakeYear)
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

            int CurrentMonth = datevalue.Month + 1;
            int CurrentYear = datevalue.Year;

            int PastSept = 0;

            if (CurrentMonth >= 9)
            {
                PastSept++;
            }

            int whichYear = (CurrentYear - intakeYear) + PastSept;

            return whichYear;
        }
    }
}