using Android;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using AndroidX.RecyclerView.Widget;
using Scanflow.Xamarin.Android;
using Scanflow.Xamarin.Android.Helper;
using Scanflow.Xamarin.Android.Model;
using System.Collections.Generic;

namespace Scanflow.Xamarin
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        private RecyclerView recyclerView;
        private RecyclerViewAdapter adapter;
        private RecyclerView.LayoutManager layoutManager;
        private const int RequestCameraPermission = 100;



        private List<Scanners> list = new List<Scanners>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Homelayout);
            InitData();
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            //recyclerView.HasFixedSize= true;
            layoutManager = new LinearLayoutManager(this);  
            recyclerView.SetLayoutManager(layoutManager);
            adapter = new RecyclerViewAdapter(list,this);
            recyclerView.SetLayoutManager(new GridLayoutManager(this, 3));
            recyclerView.SetAdapter(adapter);
            recyclerView.SetBackgroundColor(Color.Transparent);

            if (CheckSelfPermission(Manifest.Permission.Camera) != Permission.Granted)
            {
                RequestPermissions(new string[] { Manifest.Permission.Camera }, RequestCameraPermission);
            }
            else
            {
            }

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (requestCode == RequestCameraPermission)
            {
                if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                {
                }
                else
                {
                }
            }
        }


        private void InitData()
        {
            list.Add(new Scanners() { imgid=Resource.Drawable.qr_code, description="QR CODE"});
            list.Add(new Scanners() { imgid = Resource.Drawable.barcode, description = "Barcode" });
            list.Add(new Scanners() { imgid = Resource.Drawable.any, description = "Any" });
            /*list.Add(new Scanners() { imgid = Resource.Drawable.batch, description = "Batch/Inventory" });
            list.Add(new Scanners() { imgid = Resource.Drawable.ic_one_of_many_codes, description = "One of many codes" });
            list.Add(new Scanners() { imgid = Resource.Drawable.ic_pivot_view, description = "Pivot View" });
            list.Add(new Scanners() { imgid = Resource.Drawable.batch, description = "Tyre Scanning" });
            list.Add(new Scanners() { imgid = Resource.Drawable.batch, description = "Vertical Container Scanning" });
            list.Add(new Scanners() { imgid = Resource.Drawable.batch, description = "Horizontal Container Scanning" });*/
        }

      
    }
}