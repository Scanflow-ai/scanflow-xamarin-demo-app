using System;
using UIKit;
namespace Scanflow.Xamarin.Native.iOS.Models
{
    public class SlideUpTransitionDelegate : UIViewControllerTransitioningDelegate
    {
        public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForPresentedController(UIViewController presented, UIViewController presenting, UIViewController source)
        {
            return new SlideUpAnimator();
        }

        public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForDismissedController(UIViewController dismissed)
        {
            return new SlideDownAnimator();
        }
    }

    public class SlideUpAnimator : UIViewControllerAnimatedTransitioning
    {
        public override double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
        {
            return 0.3; // Set the duration of the animation
        }

        public override void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
        {
            var containerView = transitionContext.ContainerView;
            var toView = transitionContext.GetViewControllerForKey(UITransitionContext.ToViewControllerKey).View;

            // Set the initial frame of the toView
            toView.Frame = new CoreGraphics.CGRect(0, containerView.Bounds.Height, containerView.Bounds.Width, containerView.Bounds.Height);

            containerView.AddSubview(toView);

            UIView.Animate(
                TransitionDuration(transitionContext),
                () =>
                {
                    // Set the final frame for the toView to slide it up from the bottom
                    toView.Frame = containerView.Bounds;
                },
                () =>
                {
                    transitionContext.CompleteTransition(!transitionContext.TransitionWasCancelled);
                });
        }
    }

    public class SlideDownAnimator : UIViewControllerAnimatedTransitioning
    {
        public override double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
        {
            return 0.3; // Set the duration of the animation
        }

        public override void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
        {
            var containerView = transitionContext.ContainerView;
            var fromView = transitionContext.GetViewControllerForKey(UITransitionContext.FromViewControllerKey).View;

            UIView.Animate(
                TransitionDuration(transitionContext),
                () =>
                {
                    // Set the final frame for the fromView to slide it down to the bottom
                    fromView.Frame = new CoreGraphics.CGRect(0, containerView.Bounds.Height, containerView.Bounds.Width, containerView.Bounds.Height);
                },
                () =>
                {
                    transitionContext.CompleteTransition(!transitionContext.TransitionWasCancelled);
                });
        }
    }

}

