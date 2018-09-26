using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    public interface IProgressDialog
    {
        void Hide();

        void SetProgress(int value);
    }
}
