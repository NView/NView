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
			return (T)GetViewOfType (nativeObject, typeof(T));
		}

		/// <summary>
		/// Given a UI object, find the first view and make it into the native type.
		/// </summary>
		/// <returns>The view with the specified type.</returns>
		/// <param name="nativeObject">Native object.</param>
		/// <param name="type">Native view type.</param>
		public static NativeView GetViewOfType (object nativeObject, Type type)
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
		public static NativeView FindView (object nativeObject)
		{
			if (nativeObject == null)
				return null;
			
			var v = nativeObject as NativeView;
			if (v != null)
				return v;

			var vc = nativeObject as NativeViewController;
			if (vc != null)
				return vc.View;

			return null;
		}

		/// <summary>
		/// Returns a view of the specified type by either simply
		/// casting the given view, or by adding a new subview.
		/// </summary>
		/// <returns>The native view with the given type.</returns>
		/// <param name="nativeView">Native view.</param>
		/// <param name="type">Type.</param>
		public static NativeView MakeViewIntoType (NativeView nativeView, Type type)
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
			var newView = CreateView (type);
			newView.TranslatesAutoresizingMaskIntoConstraints = false;
			nativeView.AddSubview (newView);

			//
			// Force the new view to completely cover the old view
			//
			Func<NSLayoutAttribute, NSLayoutConstraint> constrain = attr =>
				NSLayoutConstraint.Create (
					nativeView, attr,
					NSLayoutRelation.Equal,
					newView, attr,
					1, 0);
			
			nativeView.AddConstraints (new[] {
				constrain (NSLayoutAttribute.Leading),
				constrain (NSLayoutAttribute.Trailing),
				constrain (NSLayoutAttribute.Top),
				constrain (NSLayoutAttribute.Bottom)
			});

			return newView;
		}

		/// <summary>
		/// Creates a view given its native type.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="type">Native view type.</param>
		public static NativeView CreateView (Type type)
		{
			if (type == null)
				throw new ArgumentNullException ("type");
			return (NativeView)Activator.CreateInstance (type);
		}
	}
}

