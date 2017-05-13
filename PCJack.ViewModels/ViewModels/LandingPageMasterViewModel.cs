using PCJack.Interfaces.Common;
using PCJack.Interfaces.Model;
using PCJack.Interfaces.ViewModelInterfaces;
using PCJack.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PCJack.ViewModels.ViewModels
{
    public class LandingPageMasterViewModel : ViewModelController, ILandingPageMasterViewModel
    {
        private readonly IMenuItemBuilder _menuItemBuilder;

        private ObservableCollection<IMasterMenuItem> _itemList;

        public LandingPageMasterViewModel(IMenuItemBuilder menuItemBuilder)
        {

            if (menuItemBuilder == null)
            {
                throw new ArgumentNullException("menuItemBuilder");
            }
            _menuItemBuilder = menuItemBuilder;

            ItemList = _menuItemBuilder.GetMenuItems();
        }
        public ObservableCollection<IMasterMenuItem> ItemList
        {
            get
            {
                return _itemList;
            }
            set {
                _itemList = value;
                OnPropertyChanged();
            }
        }
        public IMasterMenuItem ItemSelected
        {
            set
            {
                OnPropertyChanged();
                (value as MenuItem).Command.Execute(null);
            }
        }
    }
}
