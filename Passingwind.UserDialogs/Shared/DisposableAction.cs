using System;

namespace Passingwind.UserDialogs
{
    internal class DisposableAction : IDisposable
    {
        private readonly Action action;

        public DisposableAction(Action action)
        {
            this.action = action;
        }

        public void Dispose()
        {
            this.action();
            GC.SuppressFinalize(this);
        }
    }
}