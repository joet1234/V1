using System;

namespace PleaseWorkV1.Datasets
{
    public class ClassInstance
    {
        public String StartDate { set; get; }
        public String StartTime { set; get; }
        public String EndTime { set; get; }
        public String Subject { set; get; }
        public String Location { set; get; }
        public String Teacher { set; get; }
        public String Essential { set; get; }
        public String Notes { set; get; }

        public ClassInstance ()
        {

        }

        public ClassInstance(String StartDate, String StartTime, String Subject, String Notes)
        {
            this.StartDate = StartDate;
            this.StartTime = StartTime;
            this.Subject = Subject;
            this.Notes = Notes;
        }

        public ClassInstance(String StartTime, String EndTime, String Subject, String Location, String Essential)
        {
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.Subject = Subject;
            this.Location = Location;
            this.Essential = Essential;
        }

        public ClassInstance(String Date, String StartTime, String EndTime, String Subject, String Location, String Teacher)
        {
            this.StartDate = Date; 
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.Subject = Subject;
            this.Location = Location;
            this.Teacher = Teacher;
        }
    }
}