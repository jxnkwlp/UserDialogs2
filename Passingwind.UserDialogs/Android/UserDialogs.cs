using Android.App;
using Passingwind.UserDialogs.Platforms;
using System;

namespace Passingwind.UserDialogs
{
	public partial class UserDialogs
	{
		private static IUserDialogs _instance = null;

		public static IUserDialogs Instance
		{
			get
			{
				if (_instance == null)
				{
					throw new Exception("[Passingwind.UserDialogs] In android, you must call UserDialogs.Init(Activity) from your first activity OR UserDialogs.Init(App) from your custom application OR provide a factory function to get the current top activity via UserDialogs.Init(() => supply top activity)");
				}

				return _instance;
			}
			set => _instance = value;
		}

		public static void Init(Func<Activity> topActivityFactory)
		{
			_instance = new UserDialogsImpl(topActivityFactory);
		}

		/// <summary>
		/// Initialize android user dialogs
		/// </summary>
		public static void Init(Application app)
		{
			ActivityLifecycleCallbacks.Register(app);
			Init(() => ActivityLifecycleCallbacks.CurrentTopActivity);
		}

		/// <summary>
		/// Initialize android user dialogs
		/// </summary>
		public static void Init(Activity activity)
		{
			ActivityLifecycleCallbacks.Register(activity);
			Init(() => ActivityLifecycleCallbacks.CurrentTopActivity);
		}
	}
}