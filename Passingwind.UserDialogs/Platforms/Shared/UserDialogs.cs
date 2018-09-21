using System;
using System.Collections.Generic;
using System.Text;

namespace Passingwind.UserDialogs
{
    public partial class UserDialogs
    {

#if NETSTANDARD

        static IUserDialogs _instance = null;

        public static IUserDialogs Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new Exception("[Passingwind.UserDialogs] This is the bait library, not the platform library.  You must install the nuget package in your main executable/application project");
                }

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

#endif

    }
}
