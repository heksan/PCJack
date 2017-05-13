using PCJack.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace PCJack.Common
{
    public class ApplicationPropertiesWrapper : IApplicationPropertiesWrapper
    {
        public T GetValue<T>(string appPropertiesString)
        {
            if (Application.Current.Properties.ContainsKey(appPropertiesString))
            {
                return (T)Application.Current.Properties[appPropertiesString];
            }
            else
            {
                return default(T);
            }
        }

        public void Set<T>(string appPropertiesString, object toSave)
        {
            if (Application.Current.Properties.ContainsKey(appPropertiesString))
            {
                Application.Current.Properties[appPropertiesString] = toSave;
            }
            else
            {
                Application.Current.Properties.Add(appPropertiesString, (T)toSave);
            }

            Application.Current.SavePropertiesAsync();
        }
    }
}
