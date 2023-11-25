using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.CardView.Widget;
using AndroidX.RecyclerView.Widget;
using Java.Util;
using Java.Util.Zip;
using Scanflow.Xamarin.Android.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace Scanflow.Xamarin.Android.Helper
{
    class RecyclerViewHolder : RecyclerView.ViewHolder
    {
        public ImageView imageView { get; set; }
        public TextView textView { get; set; }
        public Activity context { get; set; }
        public List<Scanners> list;

        public RecyclerViewHolder(View itemView,Activity context, List<Scanners> list) : base(itemView)
        {
            imageView = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            textView = itemView.FindViewById<TextView>(Resource.Id.textView);

            itemView.Click += (sender, e) => {
                var context = itemView.Context;
                var scanType = itemView.FindViewById<TextView>(Resource.Id.textView);
                if (scanType.Text == "Tyre Scanning" || scanType.Text == "Vertical Container Scanning" || scanType.Text == "Horizontal Container Scanning")
                {
                   // var Textintent = new Intent(context, typeof(TextScanActivity));
                    //Textintent.PutExtra("ScanType", scanType.Text);
                    //context.StartActivity(Textintent);
                }
                else
                {
                    var intent = new Intent(context, typeof(ScanActivity));
                    intent.PutExtra("ScanType", scanType.Text);
                    context.StartActivity(intent);
                }
            };

           /* CardView card = ItemView.FindViewById<CardView>(Resource.Id.cardViewId);
            int width = context.Resources.DisplayMetrics.WidthPixels;
            int height = context.Resources.DisplayMetrics.HeightPixels;
            card.LayoutParameters.Height = (height - (4 * 10 + 200)) / 4;
            card.LayoutParameters.Width = (width - 80) / 2;
            this.context = context;
            this.list = list;*/

            /*  CardView card = ItemView.FindViewById<CardView>(Resource.Id.cardViewId);
              int width = context.Resources.DisplayMetrics.WidthPixels;
              int height = context.Resources.DisplayMetrics.HeightPixels;
              card.LayoutParameters.Height = (height - (4 * 10 + 200)) / 4;
              card.LayoutParameters.Width = (width - 80) / 2;
              this.context = context;
              //this.list = list;*/

        }
    }
        public class RecyclerViewAdapter : RecyclerView.Adapter
    {
        public List<Scanners>list = new List<Scanners>();
        public Activity context; View itemView;
        public override int ItemCount {

            get
            {
                return list.Count;
            }
        }

        public RecyclerViewAdapter(List<Scanners> list,Activity activity)
        {
            this.list = list;
            this.context = activity;
         
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            RecyclerViewHolder holder = viewHolder as RecyclerViewHolder;
            holder.imageView.SetImageResource(list[position].imgid);
            holder.textView.Text = list[position].description;

        }

        public override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater Inflater = LayoutInflater.From(parent.Context);
            View itemView = Inflater.Inflate(Resource.Layout.ScannersLayout, parent, false);    
            return new RecyclerViewHolder(itemView, context, list);    
        }
    }


}