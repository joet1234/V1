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
using PleaseWorkV1.DataSets;
using PleaseWorkV1.Datasets;
using Android.Graphics;

namespace PleaseWorkV1.ListViewAdapters
{
    
        public class ExpandableListViewAdapter : BaseExpandableListAdapter
        {
            private Context context;
            private List<string> listGroup;
            private Dictionary<string, List<ClassInstance>> lstChild;

            public ExpandableListViewAdapter(Context context, List<string> listGroup, Dictionary<string, List<ClassInstance>> lstChild)
            {
                this.context = context;
                this.listGroup = listGroup;
                this.lstChild = lstChild;
            }


            public override int GroupCount
            {
                get
                {
                    return listGroup.Count;
                }
            }

            public override bool HasStableIds
            {
                get
                {
                    return false;
                }
            }



            public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
            {
                var result = new List<ClassInstance>();
                lstChild.TryGetValue(listGroup[groupPosition], out result);
                return result[childPosition].ToString();
            }

        public Java.Lang.Object GetChildStartTime(int groupPosition, int childPosition)
        {
            var result = new List<ClassInstance>();
            lstChild.TryGetValue(listGroup[groupPosition], out result);
            return result[childPosition].StartTime;
        }

        public Java.Lang.Object GetChildEndTime(int groupPosition, int childPosition)
        {
            var result = new List<ClassInstance>();
            lstChild.TryGetValue(listGroup[groupPosition], out result);
            return result[childPosition].EndTime;
        }

            public Java.Lang.Object GetChildSubject(int groupPosition, int childPosition)
            {
                var result = new List<ClassInstance>();
                lstChild.TryGetValue(listGroup[groupPosition], out result);
                return result[childPosition].Subject;
            }

        public Java.Lang.Object GetChildLocation(int groupPosition, int childPosition)
        {
            var result = new List<ClassInstance>();
            lstChild.TryGetValue(listGroup[groupPosition], out result);
            return result[childPosition].Location;
        }

        public Java.Lang.Object GetChildEssential(int groupPosition, int childPosition)
        {
            var result = new List<ClassInstance>();
            lstChild.TryGetValue(listGroup[groupPosition], out result);
            return result[childPosition].Essential;
        }


        public override long GetChildId(int groupPosition, int childPosition)
            {
                return childPosition;
            }

            public override int GetChildrenCount(int groupPosition)
            {
                var result = new List<ClassInstance>();
                lstChild.TryGetValue(listGroup[groupPosition], out result);
                return result.Count;
            }

            public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
            {
                if (convertView == null)
                {
                    LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                    convertView = inflater.Inflate(Resource.Layout.Children, null);
                }
                //TextView textViewItem = convertView.FindViewById<TextView>(Resource.Id.item);


            TextView StartTimeTV = convertView.FindViewById<TextView>(Resource.Id.StartIDTV);
            TextView EndTimeTV = convertView.FindViewById<TextView>(Resource.Id.EndIDTV);
            TextView SubjectTV = convertView.FindViewById<TextView>(Resource.Id.SubjectIDTV);
            TextView LocationTV = convertView.FindViewById<TextView>(Resource.Id.LocationIDTV);

            if (GetChildEssential(groupPosition, childPosition).ToString() == "True")
            {
                string start = (string)GetChildStartTime(groupPosition, childPosition);
                string end = (string)GetChildEndTime(groupPosition, childPosition);
                string subject = (string)GetChildSubject(groupPosition, childPosition);
                string location = (string)GetChildLocation(groupPosition, childPosition);

                StartTimeTV.Text = start;
                EndTimeTV.Text = end;
                SubjectTV.Text = subject;
                LocationTV.Text = location;

                StartTimeTV.SetTextColor(Color.ParseColor("red"));
                EndTimeTV.SetTextColor(Color.ParseColor("red"));
                SubjectTV.SetTextColor(Color.ParseColor("red"));
                LocationTV.SetTextColor(Color.ParseColor("red"));

            }
            else
            {
                string start = (string)GetChildStartTime(groupPosition, childPosition);
                string end = (string)GetChildEndTime(groupPosition, childPosition);
                string subject = (string)GetChildSubject(groupPosition, childPosition);
                string location = (string)GetChildLocation(groupPosition, childPosition);

                StartTimeTV.Text = start;
                EndTimeTV.Text = end;
                SubjectTV.Text = subject;
                LocationTV.Text = location;

            }
            
            // textViewItem.Text = content;
            return convertView;
            }

            public override Java.Lang.Object GetGroup(int groupPosition)
            {
                return listGroup[groupPosition];
            }

            public override long GetGroupId(int groupPosition)
            {
                return groupPosition;
            }

            public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
            {
                if (convertView == null)
                {
                    LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                    convertView = inflater.Inflate(Resource.Layout.Parent, null);
                }
                string textGroup = (string)GetGroup(groupPosition);
                TextView textViewGroup = convertView.FindViewById<TextView>(Resource.Id.ParentTVID);
                textViewGroup.Text = textGroup;
                return convertView;
            }

            public override bool IsChildSelectable(int groupPosition, int childPosition)
            {
                return true;
            }
        }
    }
