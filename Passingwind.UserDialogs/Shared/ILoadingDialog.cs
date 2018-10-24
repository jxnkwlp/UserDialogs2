using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    /// <summary>
    ///  define <see cref="IProgressDialog"/>
    /// </summary>
    public interface IProgressDialog
    {
        void Hide();

        void SetProgress(int value);
    }
}
