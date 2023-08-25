using System;
using CoreData;
using Foundation;

namespace Scanflow.Xamarin.Native.iOS.Models
{
    [Register("MySettings")]
    public class MySettings : NSManagedObject
    {
        protected MySettings(NSObjectFlag t) : base(t)
        {
        }

        [Export("auto_exposure")]
        public bool Auto_exposure { get; set; }

        [Export("auto_flash")]
        public bool Auto_flash { get; set; }

        [Export("auto_zoom")]
        public bool Auto_zoom { get; set; }

        [Export("zoom_none")]
        public bool Zoom_none { get; set; }

        [Export("fourk_pixel")]
        public bool Fourk_pixel { get; set; }

        [Export("fullhd_pixel")]
        public bool Fullhd_pixel { get; set; }

        [Export("hd_pixel")]
        public bool Hd_pixel { get; set; }

        [Export("one_touch_zoom")]
        public bool One_touch_zoom { get; set; }

        [Export("sd_pixel")]
        public bool Sd_pixel { get; set; }

        [Export("settings_id")]
        public short Settings_id { get; set; }

        [Export("is_onboard_completed")]
        public bool Is_onboard_completed { get; set; }

        public static MySettings Create(MySettings data, NSManagedObjectContext context)
        {
            var entity = NSEntityDescription.InsertNewObjectForEntityForName("MySettings", context) as MySettings;

            return entity;
        }
    }
}

