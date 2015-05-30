using System;

using Android.Views;
using Android.App;
using Android.Content;

namespace NView
{
	/// <summary>
	/// Methods to assist binding IViews to native views.
	/// </summary>
	public static partial class ViewHelpers
	{
		/// <summary>
		/// Bind extension to use resource Id to find and bind native view from Activity
		/// </summary>
		/// <returns>The to IDispoasble bound View.</returns>
		/// <param name="activity">Activity that contains the android view.</param>
		/// <param name="view">View to bind to.</param>
		/// <param name="resourceId">Resource identifier of android view.</param>
		/// <param name="options">Overrides to the default behavior of BindToNative.</param>
		/// <typeparam name="T">Type of native android view.</typeparam>
		public static void BindToNative<T>(this Activity activity, IView view, int resourceId, BindOptions options = BindOptions.None) where T : View
		{
			var nativeView = activity.FindViewById<T> (resourceId);
			if (nativeView == null) {
				throw new InvalidOperationException ("Cannot find view for resource " + resourceId);
			}
			view.BindToNative (nativeView, options);
		}

		/// <summary>
		/// Bind extension to use resource Id to find and bind native view from ViewGroup or Fragment
		/// </summary>
		/// <returns>The to IDispoasble bound View.</returns>
		/// <param name="androidView"> View that contains android view</param>
		/// <param name="view">View to bind to.</param>
		/// <param name="resourceId">Resource identifier of android view.</param>
		/// <param name="options">Overrides to the default behavior of BindToNative.</param>
		/// <typeparam name="T">Type of native android view.</typeparam>
		public static void BindToNative<T>(this View androidView, IView view, int resourceId, BindOptions options = BindOptions.None) where T : View
		{
			var nativeView = androidView.FindViewById<T> (resourceId);
			if (nativeView == null) {
				throw new InvalidOperationException ("Cannot find view for resource " + resourceId);
			}
			view.BindToNative (nativeView, options);
		}

		/// <summary>
		/// Gets or converts the view to the specified type.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="nativeObject">Native object.</param>
		/// <typeparam name="T">The native view type.</typeparam>
		public static T GetView<T> (object nativeObject) where T : View
		{
			var v = nativeObject as T;
			if (v == null)
				throw new InvalidOperationException ("Cannot get " + typeof(T) + " from " + nativeObject);
			return v;
		}

		/// <summary>
		/// Creates the native object for the given cross-platform <see cref="IView"/>.
		/// </summary>
		/// <returns>The bound native object.</returns>
		/// <param name="view">View.</param>
		/// <param name="context">Android context in which to run the newly created view.</param>
		/// <param name="options">Overrides to the default behavior of BindToNative.</param>
		public static object CreateBoundNative (this IView view, Context context, BindOptions options = BindOptions.None)
		{
			if (view == null)
				throw new ArgumentNullException ("view");
			var n = view.CreateNative (context);
			view.BindToNative (n, options);
			return n;
		}

		/// <summary>
		/// Creates the preferred native view and binds the given <see cref="IView"/> to it.
		/// </summary>
		/// <returns>The bound native view.</returns>
		/// <param name="view">View.</param>
		/// <param name="context">Android context in which to run the newly created view.</param>
		/// <param name="options">Overrides to the default behavior of BindToNative.</param>
		public static View CreateBoundNativeView (this IView view, Context context, BindOptions options = BindOptions.None)
		{
			if (view == null)
				throw new ArgumentNullException ("view");
			var n = view.CreateBoundNative (context, options);
			var nativeView = n as View;
			if (nativeView == null)
				throw new InvalidOperationException ("Cannot bind " + view + " to a native view");
			return nativeView;
		}
	}
}

