using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCJack.Interfaces.Common
{
    public interface IApplicationPropertiesWrapper
    {
        T GetValue<T>(string appPropertiesString);
        void Set<T>(string appPropertiesString, object toSave);
    }
}
