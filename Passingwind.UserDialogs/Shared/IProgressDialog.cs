using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    /// <summary>
    /// </summary>
    public interface IProgressDialog
    {
        void Hide();

        void SetProgress(int value);
    }
}
