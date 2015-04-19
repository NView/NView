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
		/// <typeparam name="T">Type of native android view.</typeparam>
		public static IDisposable BindToNative<T>(this Activity activity, IView view, int resourceId) where T : View
		{
			var nativeView = activity.FindViewById<T> (resourceId);
			if (nativeView == null) {
				throw new InvalidOperationException ("Cannot find a view for " + resourceId);
			}
			return view.BindToNative (nativeView);
		}

		/// <summary>
		/// Bind extension to use resource Id to find and bind native view from ViewGroup or Fragment
		/// </summary>
		/// <returns>The to IDispoasble bound View.</returns>
		/// <param name="androidView"> View that contains android view</param>
		/// <param name="view">View to bind to.</param>
		/// <param name="resourceId">Resource identifier of android view.</param>
		/// <typeparam name="T">Type of native android view.</typeparam>
		public static IDisposable BindToNative<T>(this View androidView, IView view, int resourceId) where T : View
		{
			var nativeView = androidView.FindViewById<T> (resourceId);
			if (nativeView == null) {
				throw new InvalidOperationException ("Cannot find a view for " + resourceId);
			}
			return view.BindToNative (nativeView);
		}

		/// <summary>
		/// Gets or converts the view to the specified type.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="nativeObject">Native object.</param>
		/// <typeparam name="T">The native view type.</typeparam>
		public static T GetView<T> (object nativeObject) where T : View
		{
			return (T)GetViewOfType (nativeObject, typeof(T));
		}

		/// <summary>
		/// Given a UI object, find the first view and make it into the native type.
		/// </summary>
		/// <returns>The view with the specified type.</returns>
		/// <param name="nativeObject">Native object.</param>
		/// <param name="type">Native view type.</param>
		public static View GetViewOfType (object nativeObject, Type type)
		{
			var v = FindView (nativeObject);

			if (v == null) {
				throw new InvalidOperationException ("Cannot find a view for " + nativeObject);
			}

			return MakeViewIntoType (v, type);
		}

		/// <summary>
		/// Looks for a view given a UI object.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="nativeObject">Native object.</param>
		public static View FindView (object nativeObject)
		{
			if (nativeObject == null)
				return null;
			
			var v = nativeObject as View;
			if (v != null)
				return v;

			var a = nativeObject as Activity;
			if (a != null)
				return a.FindViewById (Android.Resource.Id.Content);

			var f = nativeObject as Fragment;
			if (f != null)
				return f.View;
			
			return null;
		}

		/// <summary>
		/// Returns a view of the specified type by either simply
		/// casting the given view, or by adding a new subview.
		/// </summary>
		/// <returns>The native view with the given type.</returns>
		/// <param name="nativeView">Native view.</param>
		/// <param name="type">Type.</param>
		public static View MakeViewIntoType (View nativeView, Type type)
		{
			if (nativeView == null)
				throw new ArgumentNullException ();

			//
			// Is it already the right view?
			//
			var srcType = nativeView.GetType ();
			if (type.IsAssignableFrom (srcType))
				return nativeView;

			//
			// Nope, gotta make our own
			//
			var vg = nativeView as ViewGroup;
			if (vg == null)
				throw new InvalidOperationException ("Cannot convert " + nativeView + " to " + type + " and cannot add subviews.");

			var newView = CreateView (type);
			vg.AddView (newView);

			//
			// TODO: Force the new view to completely cover the old view
			//

			return newView;
		}

		/// <summary>
		/// Creates a view given its native type.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="type">Native view type.</param>
		public static View CreateView (Type type)
		{
			if (type == null)
				throw new ArgumentNullException ("type");
			return (View)Activator.CreateInstance (type);
		}
	}
}

