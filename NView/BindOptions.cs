using System;

namespace NView
{
	/// <summary>
	/// Various overrides to the default behavior of <see cref="IView.BindToNative"/>.
	/// </summary>
	[Flags]
	public enum BindOptions
	{
		/// <summary>
		/// Use the default behavior.
		/// </summary>
		None = 0,

		/// <summary>
		/// Set the IView's properties to values already set in the native view.
		/// <remarks>>
		/// </remarks>
		/// This is convenient when binding to fully initialized native views
		/// from Android Layouts or Apple Storyboards.
		/// </summary>
		PreserveNativeProperties = 0x01,
	}
}

