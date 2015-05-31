using System;

using Foundation;

#if __IOS__
using UIKit;
using NativeView = UIKit.UIView;
using NativeViewController = UIKit.UIViewController;
#else
using AppKit;
using NativeView = AppKit.NSView;
using NativeViewController = AppKit.NSViewController;
#endif

namespace NView
{
	/// <summary>
	/// Methods to assist binding IViews to native views.
	/// </summary>
	public static partial class ViewHelpers
	{
		/// <summary>
		/// Gets or converts the view to the specified type.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="nativeObject">Native object.</param>
		/// <typeparam name="T">The native view type.</typeparam>
		public static T GetView<T> (object nativeObject) where T : NativeView
		{
			var v = nativeObject as T;
			if (v == null)
				throw new InvalidOperationException ("Cannot get " + typeof(T) + " from " + nativeObject);
			return v;
		}

		/// <summary>
		/// Creates a native view with the given cross-platform <see cref="IView"/> bound to it.
		/// </summary>
		/// <returns>The bound native view.</returns>
		/// <param name="view">View.</param>
		/// <param name="options">Overrides to the default behavior of BindToNative.</param>
		public static object CreateBoundNative (this IView view, BindOptions options = BindOptions.None)
		{
			if (view == null)
				throw new ArgumentNullException ("view");
			var nativeView = view.CreateNative ();
			view.BindToNative (nativeView, options);
			return nativeView;
		}

		/// <summary>
		/// Creates a native view with the given cross-platform <see cref="IView"/> bound to it.
		/// </summary>
		/// <returns>The bound native view.</returns>
		/// <param name="view">View.</param>
		/// <param name="options">Overrides to the default behavior of BindToNative.</param>
		public static NativeView CreateBoundNativeView (this IView view, BindOptions options = BindOptions.None)
		{
			if (view == null)
				throw new ArgumentNullException ("view");
			var native = view.CreateBoundNative (options);
			var nativeView = native as NativeView;
			if (nativeView != null) {
				return nativeView;
			}
			var nativeVC = native as NativeViewController;
			if (nativeVC != null)
				return nativeVC.View;
			throw new InvalidOperationException ("Cannot convert " + native + " to a native view.");
		}

		/// <summary>
		/// Creates a native view controller with the given cross-platform <see cref="IView"/> bound to it.
		/// </summary>
		/// <returns>The bound native view controller.</returns>
		/// <param name="view">View.</param>
		/// <param name="options">Overrides to the default behavior of BindToNative.</param>
		public static NativeViewController CreateBoundNativeViewController (this IView view, BindOptions options = BindOptions.None)
		{
			var n = view.CreateBoundNative (options);

			// Is it already a VC?
			var vc = n as NativeViewController;
			if (vc != null)
				return vc;
			
			// Nope, make it one
			var v = n as NativeView;
			if (v != null) {
				vc = new NativeViewController ();
				vc.View = v;
				return vc;
			}

			throw new InvalidOperationException ("Cannot bind " + view + " to a view controller");
		}
	}
}

