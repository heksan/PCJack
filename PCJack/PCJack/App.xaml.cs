using Ninject;
using PCJack.Controllers.Navigation;
using PCJack.Interfaces.Common;
using PCJack.IoC.Kernel;
using PCJack.Views.ViewsInterfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PCJack
{
    public partial class App : Application
    {
        private readonly INavigationService _navigationService;
        public App()
        {
            InitializeComponent();
            Installer.Install();

            _navigationService=NinjectKernel.Kernel.Get<INavigationService>();
            _navigationService.InitializeLandingPage();
            
        }


    }
}
