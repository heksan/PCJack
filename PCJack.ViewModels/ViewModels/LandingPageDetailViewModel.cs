using PCJack.Common;
using PCJack.Interfaces.Common;
using PCJack.Interfaces.Services;
using PCJack.Interfaces.ViewModelInterfaces;
using PCJack.ViewModels.Common;
using PCJack.Views.ViewsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PCJack.ViewModels.ViewModels
{
    public class LandingPageDetailViewModel : ViewModelController, ILandingPageDetailViewModel
    {
        private string _ip = string.Empty;


        private readonly ISystemControlClient _systemControlService;
        private readonly INavigationService _navigationService;
        private readonly IApplicationPropertiesWrapper _appPropertiesWrapper;
        #region ctor
        public LandingPageDetailViewModel(ISystemControlClient systemControlService, INavigationService navigationService,IApplicationPropertiesWrapper appPropertiesWrapper)
        {
            if (systemControlService == null)
            {
                throw new ArgumentNullException("systemControlService");
            }
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (appPropertiesWrapper==null)
            {
                throw new ArgumentNullException("appPropertiesWrapper");
            }
            _systemControlService = systemControlService;
            _navigationService = navigationService;
            _appPropertiesWrapper = appPropertiesWrapper;
            var lastKnownIP = _appPropertiesWrapper.GetValue<string>(ApplicationStrings.LastKnownIPValue);
            if (!string.IsNullOrEmpty(lastKnownIP)) 
            {
                IP = lastKnownIP;
            }
            else
            {
                IP = "192.168.0.27";
            }
            
        }
        #endregion
        #region props
        public string IP
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public Command Connect
        {
            get
            {
                return new Command(async (e) =>
                {
                    await Application.Current.SavePropertiesAsync();
                    if (_systemControlService.CheckConnection(e as string))
                    {
                        _appPropertiesWrapper.Set<string>(ApplicationStrings.LastKnownIPValue, e);
                        _navigationService.NavigateTo(typeof(ILandingPageDetailView));
                    }
                    else
                    {
                        _appPropertiesWrapper.Set<string>(ApplicationStrings.LastKnownIPValue, "");
                    }

                });
            }
        }

    }
}
