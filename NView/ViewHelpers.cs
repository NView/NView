using System;

namespace NView
{
	public static partial class PlatformHelpers
	{
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

		public static IDisposable CreateDisposable (Action dispose)
		{
			return new DisposeAction (dispose);
		}
	}
}

