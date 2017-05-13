using PCJack.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCJack.Interfaces.Common
{
    public interface IMenuItemBuilder
    {
        ObservableCollection<IMasterMenuItem> GetMenuItems();
    }
}
