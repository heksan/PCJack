using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Ninject;
using PCJack.Interfaces.Common;
using PCJack.Views.ViewsInterfaces;
using PCJack.ViewModels.ViewModelInterfaces;
using PCJack.IoC.Kernel;
using PCJack.Interfaces.ViewModelInterfaces;

namespace PCJack.Controllers.Navigation
{
    public class NavigationService : INavigationService
    {
        private MasterDetailPage _masterPage;
        public NavigationService()
        {

        }
        public void InitializeLandingPage()
        {
            _masterPage = NinjectKernel.Kernel.Get<ILandingPageView>() as MasterDetailPage;
            _masterPage.BindingContext = ViewToViewModelLinks.GetBindingContext(typeof(ILandingPageView));

            var detail = NinjectKernel.Kernel.Get<ILandingPageDetailView>() as ContentPage;
            detail.BindingContext = ViewToViewModelLinks.GetBindingContext(typeof(ILandingPageDetailView));

            var master = NinjectKernel.Kernel.Get<ILandingPageMasterView>() as ContentPage;
            master.BindingContext = ViewToViewModelLinks.GetBindingContext(typeof(ILandingPageMasterView));

            _masterPage.Detail =detail;
            _masterPage.Master = master;
            Application.Current.MainPage = _masterPage;
        }
        public void NavigateToOverlay(Type t)
        {
            var page = (NinjectKernel.Kernel.Get(t) as ContentPage);
            page.BindingContext = ViewToViewModelLinks.GetBindingContext(t);
            _masterPage.Detail.Navigation.PushAsync(page);
        }

        public void NavigateTo(Type t)
        {
            var page = (NinjectKernel.Kernel.Get(t) as ContentPage);
            page.BindingContext = ViewToViewModelLinks.GetBindingContext(t); //note to future me - this line fetches and initializes viewmodels
            _masterPage.Detail = new NavigationPage(page);
        }
        [Obsolete]
        public void NavigateTo(Page p)
        {
            _masterPage.Detail = p;
        }

        public void NavigateToModal(Type t)
        {
            var page = (NinjectKernel.Kernel.Get(t) as ContentPage);
            page.BindingContext = ViewToViewModelLinks.GetBindingContext(t);
            _masterPage.Detail = page;
        }
    }
}
