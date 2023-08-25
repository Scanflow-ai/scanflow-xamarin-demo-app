using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scanflow.Xamarin.Android.Helper
{
    public class ImageAdapter : PagerAdapter
    {

        Context context;
        private int[] imageList =
        {
            Resource.Drawable.onboardImage1a,
            Resource.Drawable.onboardimageb,
            Resource.Drawable.onboardimagec,
            Resource.Drawable.onboardimaged
        };
        public ImageAdapter(Context context)
        {
            this.context = context;
        }

        public override int Count { 
            get
            {
                return imageList.Length;
            }
        }
      
        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view == (ImageView)@object;
        }
        public override Java.Lang.Object InstantiateItem(View container , int position)
        {
           ImageView imageView = new ImageView(context);
            imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
            imageView.SetImageResource(imageList[position]);
            ((ViewPager)container).AddView(imageView,0);
            return imageView;
        }
        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object objectValue)
        {
            container.RemoveView((ImageView)objectValue);
        }




        //fill in your items
        //holder.Title.Text = "new text here";


    }

        //Fill in cound here, currently 0
      
    }

    public class ImageAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
