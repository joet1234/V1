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
using Android.Graphics;

namespace PleaseWorkV1.Datasets
{
    class ListViewTimeTableAdapter : BaseAdapter<ClassInstance>
    {
        //Set Amount of Items
        public List<ClassInstance> mItems;
        public Context mContext;

        public ListViewTimeTableAdapter (Context mContext, List<ClassInstance> mItems)
        {
            this.mContext = mContext;
            this.mItems = mItems;
        }

        //Simple just return the position
        public override long GetItemId(int position)
        {
            return position;
        }

        //List Of Rows so say oh i have 10 items lets make 10 rows 
        public override int Count
        {
            get { return mItems.Count; }
        }

        public override ClassInstance this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.ListViewTimeTable, null, false);
            }

           

            TextView StartTimeTV = row.FindViewById<TextView>(Resource.Id.StartTV);
            TextView EndTimeTV = row.FindViewById<TextView>(Resource.Id.EndTV);
            TextView SubjectTV = row.FindViewById<TextView>(Resource.Id.SubjectTV);
            TextView LocationTV = row.FindViewById<TextView>(Resource.Id.LocationTV);

            if (mItems[position].Essential == "True")
            {
                StartTimeTV.Text = mItems[position].StartTime;
                EndTimeTV.Text = mItems[position].EndTime;
                SubjectTV.Text = mItems[position].Subject;
                LocationTV.Text = mItems[position].Location;

                StartTimeTV.SetTextColor(Color.ParseColor("red"));
                EndTimeTV.SetTextColor(Color.ParseColor("red"));
                SubjectTV.SetTextColor(Color.ParseColor("red"));
                LocationTV.SetTextColor(Color.ParseColor("red"));
            }
            else
            {
                StartTimeTV.Text = mItems[position].StartTime;
                EndTimeTV.Text = mItems[position].EndTime;
                SubjectTV.Text = mItems[position].Subject;
                LocationTV.Text = mItems[position].Location;
            }

            

            return row;
        }
    }
}