using Ninject;
using PCJack.Common;
using PCJack.Common.Builders;
using PCJack.ConnectionService;
using PCJack.Controllers.Navigation;
using PCJack.Interfaces.Common;
using PCJack.Interfaces.Services;
using PCJack.Interfaces.ViewModelInterfaces;
using PCJack.Interfaces.ViewsInterfaces;
using PCJack.IoC.Kernel;
using PCJack.ViewModels.ViewModelInterfaces;
using PCJack.ViewModels.ViewModels;
using PCJack.Views;
using PCJack.Views.ViewsInterfaces;


namespace PCJack
{
    public class Installer
    {
        public static void Install()
        {
            //vvm
            NinjectKernel.Kernel.Bind<ILandingPageViewModel>().To<LandingPageViewModel>();
            NinjectKernel.Kernel.Bind<ILandingPageView>().To<LandingPageView>();
            NinjectKernel.Kernel.Bind<ILandingPageDetailViewModel>().To<LandingPageDetailViewModel>();
            NinjectKernel.Kernel.Bind<ILandingPageDetailView>().To<LandingPageViewDetail>();
            NinjectKernel.Kernel.Bind<ILandingPageMasterViewModel>().To<LandingPageMasterViewModel>();
            NinjectKernel.Kernel.Bind<ILandingPageMasterView>().To<LandingPageViewMaster>();
            NinjectKernel.Kernel.Bind<IMainYouTubeViewModel>().To<MainYouTubeViewModel>();
            NinjectKernel.Kernel.Bind<IMainYouTubeView>().To<MainYouTubeView>();


            //services
            NinjectKernel.Kernel.Bind<IYouTubeClient>().To<YouTubeClient>().InSingletonScope();
            //mics
            NinjectKernel.Kernel.Bind<ISystemControlClient>().To<SystemControlClient>().InSingletonScope();
            NinjectKernel.Kernel.Bind<INavigationService>().To<NavigationService>().InSingletonScope();
            NinjectKernel.Kernel.Bind<IApplicationPropertiesWrapper>().To<ApplicationPropertiesWrapper>();
            NinjectKernel.Kernel.Bind<IMenuItemBuilder>().To<MenuItemBuilder>();


        }
    }
}
