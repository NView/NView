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

		public DisposeAction (Action disposeAction)
		{
			this.disposeAction = disposeAction;
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

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

