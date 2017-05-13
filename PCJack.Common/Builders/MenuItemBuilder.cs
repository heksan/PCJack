using PCJack.Interfaces.Common;
using PCJack.Interfaces.Model;
using PCJack.Interfaces.Services;
using PCJack.Interfaces.ViewsInterfaces;
using PCJack.Model;
using PCJack.Views.ViewsInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PCJack.Common.Builders
{
    public class MenuItemBuilder : IMenuItemBuilder
    {
        private readonly INavigationService _navigationService;
        private readonly ISystemControlClient _systemControl;
        public MenuItemBuilder(INavigationService navigationService, ISystemControlClient systemControl)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (systemControl == null)
            {
                throw new ArgumentNullException("systemControl");
            }
            _navigationService = navigationService;
            _systemControl = systemControl;
        }
        public ObservableCollection<IMasterMenuItem> GetMenuItems()
        {
            //this class is here to extend menuItems for LandingPage
            return new ObservableCollection<IMasterMenuItem>()
            {
                new MasterMenuItem()
                {
                    Text="YouTube",
                    Command=new Command(()=> {
                        _navigationService.NavigateTo(typeof(IMainYouTubeView));
                        (Application.Current.MainPage as MasterDetailPage).IsPresented = false;
                    })
                },
                new MasterMenuItem()
                {
                    Text="Reset Connection",
                    Command=new Command(()=>
                    {
                        _navigationService.NavigateToModal(typeof(ILandingPageDetailView));
                        (Application.Current.MainPage as MasterDetailPage).IsPresented = false;
                    })
                },
                new MasterMenuItem()
                {
                    Text="Shut Down PC",
                    Command=new Command(async ()=>
                    {

                        var answer = await Application.Current.MainPage.DisplayAlert ("Are You Sure?", "This Action may result in data loss.", "Yes", "No");
                        if (answer)
                         {
                            _systemControl.ShutDown();
                            _navigationService.NavigateToModal(typeof(ILandingPageDetailView));
                            (Application.Current.MainPage as MasterDetailPage).IsPresented = false;

                        }
                    })
                }
            };
        }
    }
}
