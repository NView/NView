using System;

namespace NView
{
	/// <summary>
	/// Wraps an Action in an IDisposable so that the action is
	/// invoked when this object is destroyed.
	/// </summary>
	public class DisposeAction : IDisposable
	{
		bool disposed;
		Action disposeAction;

		/// <summary>
		/// Initializes a new instance of the <see cref="NView.DisposeAction"/> class.
		/// </summary>
		/// <param name="disposeAction">The action to run when this object is disposed.</param>
		public DisposeAction (Action disposeAction)
		{
			this.disposeAction = disposeAction;
		}

		/// <inheritdoc/>
		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		/// <summary>
		/// Releases all resource used by the <see cref="NView.DisposeAction"/> object.
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> when called from Dispose.</param>
		protected virtual void Dispose (bool disposing)
		{
			if (disposed)
				return;
			if (disposing && disposeAction != null)
				disposeAction ();
			disposeAction = null;
			disposed = true;
		}
	}
}

