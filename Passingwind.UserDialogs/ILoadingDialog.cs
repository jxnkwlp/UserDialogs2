using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    public interface ILoadingDialog
    {
        void Hide();

        void SetProgress(double value);
    }
}
