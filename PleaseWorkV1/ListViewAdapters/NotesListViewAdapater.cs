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
    class NotesListViewAdapter : BaseAdapter<ClassInstance>
    {
        //Set Amount of Items
        public List<ClassInstance> mItems;
        public Context mContext;

        public NotesListViewAdapter(Context mContext, List<ClassInstance> mItems)
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.NotesListViewDisplay, null, false);
            }
            
            TextView StartDateTV = row.FindViewById<TextView>(Resource.Id.StartDateTV);
            TextView StartTimeTV = row.FindViewById<TextView>(Resource.Id.StartTimeTV);
            TextView SubjectTVN = row.FindViewById<TextView>(Resource.Id.SubjectTVN);
            TextView NotesTVN = row.FindViewById<TextView>(Resource.Id.NotesTVN);

            StartDateTV.Text = mItems[position].StartTime;
            StartTimeTV.Text = mItems[position].EndTime;
            SubjectTVN.Text = mItems[position].Subject;
            NotesTVN.Text = mItems[position].Location;

            return row;
        }
    }
}