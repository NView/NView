using System;

namespace NView
{
	/// <summary>
	/// A cross-platform view.
	/// </summary>
	public interface IView
	{
		/// <summary>
		/// Creates an un-bound native object that is compatible with this cross-platform view.
		/// </summary>
		/// <returns>The native object.</returns>
		/// <param name="context">
		/// On Android, set this to your Android.Content.Context. 
		/// All other platforms can pass null.</param>
		object CreateNative (object context = null);

		/// <summary>
		/// Binds the <see cref="IView"/> to a native view.
		/// </summary>
		/// <param name="native">Native object to bind with.</param>
		/// <param name="options">Overrides to the default behavior of BindToNative.</param>
		void BindToNative (object native, BindOptions options = BindOptions.None);

		/// <summary>
		/// Unbinds the <see cref="IView"/> from a native view.
		/// </summary>
		void UnbindFromNative ();
	}
}

