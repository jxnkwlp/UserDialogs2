using System;
using System.Collections.Generic;
using System.Text;
using Android.OS;

namespace Passingwind.UserDialogs.Platforms
{
    public class ConfigStore
    {
        public string BundleKey { get; set; } = "UserDialogFragmentConfig";
        long counter = 0;
        readonly IDictionary<long, object> configStore = new Dictionary<long, object>();

        public static ConfigStore Instance { get; } = new ConfigStore();

        public void Store(Bundle bundle, object config)
        {
            this.counter++;
            this.configStore[this.counter] = config;
            bundle.PutLong(this.BundleKey, this.counter);
        }

        public bool Contains(Bundle bundle) => bundle?.ContainsKey(this.BundleKey) ?? false;

        public T Pop<T>(Bundle bundle) where T : class
        {
            var id = bundle.GetLong(this.BundleKey);
            var cfg = (T)this.configStore[id];
            this.configStore.Remove(id);
            return cfg;
        }
    }
}