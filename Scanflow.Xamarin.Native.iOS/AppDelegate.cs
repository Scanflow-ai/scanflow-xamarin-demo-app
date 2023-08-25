using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using CoreData;
using CoreAudioKit;
using CoreFoundation;

namespace Scanflow.Xamarin.Native.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }
        UINavigationController navigationController;
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            SetRootViewController();
            return true;
        }

        public override UISceneConfiguration GetConfiguration(UIApplication application, UISceneSession connectingSceneSession, UISceneConnectionOptions options)
        {
            // Called when a new scene session is being created.
            // Use this method to select a configuration to create the new scene with.
            return UISceneConfiguration.Create("Default Configuration", connectingSceneSession.Role);
        }

        public override void DidDiscardSceneSessions(UIApplication application, NSSet<UISceneSession> sceneSessions)
        {
            // Called when the user discards a scene session.
            // If any sessions were discarded while the application was not running, this will be called shortly after application:didFinishLaunchingWithOptions.
            // Use this method to release any resources that were specific to the discarded scenes, as they will not return.
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
        {
            return UIInterfaceOrientationMask.Portrait;
        }

        public void SetRootViewController()
        {
            try
            {
                Window = new UIWindow(UIScreen.MainScreen.Bounds);
                var storyboard = UIStoryboard.FromName("Main", null);
                 navigationController = storyboard.InstantiateViewController("navigationController") as UINavigationController;

                OnBoardingViewController viewController = storyboard.InstantiateViewController("OnBoardingViewController") as OnBoardingViewController;
                UIViewController[] uIViewControllers = { viewController };
                navigationController.ViewControllers = uIViewControllers;
                navigationController.NavigationBar.BarStyle = UIBarStyle.Black;
                Window.RootViewController = navigationController;
                Window.MakeKeyAndVisible();



            }
            catch (Exception ex)
            {

            }
        }
    }

}


