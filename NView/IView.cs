using System;

namespace NView
{
	/// <summary>
	/// NView View
	/// </summary>
	public interface IView
	{
		/// <summary>
		/// Binds the <see cref="IView"/> to a native view.
		/// </summary>
		/// <returns>A disposable view</returns>
		/// <param name="nativeView">Native view to bind with.</param>
		/// <param name="options">Overrides to the default behavior of BindToNative.</param>
		IDisposable BindToNative (object nativeView, BindOptions options = BindOptions.None);

		/// <summary>
		/// Gets the type of the preferred native view.
		/// </summary>
		/// <value>The System.Type of the preferred native view.</value>
		Type PreferredNativeType { get; }
	}
}

