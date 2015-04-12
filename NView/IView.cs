using System;

namespace NView
{
  /// <summary>
  /// NView View
  /// </summary>
  public interface IView
  {
    /// <summary>
    /// Binds the IView to a native view.
    /// </summary>
    /// <returns>A disposable view</returns>
    /// <param name="nativeView">Native view to bind with.</param>
    IDisposable BindToNative(object nativeView);

    /// <summary>
    /// Gets the type of the preferred native control.
    /// </summary>
    /// <value>The type of the preferred native.</value>
    Type PreferredNativeType { get; }
  }
}

