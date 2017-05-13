using Ninject;
using PCJack.Interfaces.ViewModelInterfaces;
using PCJack.Interfaces.ViewsInterfaces;
using PCJack.ViewModels.ViewModelInterfaces;
using PCJack.Views.ViewsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCJack.IoC.Kernel
{
    public static class ViewToViewModelLinks
    {
        static Dictionary<Type, Type> Links = new Dictionary<Type, Type>();
        static ViewToViewModelLinks()
        {
            Links.Add(typeof(ILandingPageView), typeof(ILandingPageViewModel));
            Links.Add(typeof(ILandingPageDetailView), typeof(ILandingPageDetailViewModel));
            Links.Add(typeof(ILandingPageMasterView), typeof(ILandingPageMasterViewModel));
            Links.Add(typeof(IMainYouTubeView), typeof(IMainYouTubeViewModel));
        }
        public static object GetBindingContext(Type type)
        {
            return NinjectKernel.Kernel.Get(Links[type]);
        }
        public static Type GetViewType(Type type)
        {
            return Links.FirstOrDefault((x) => x.Value == type).Key;
        }
    }
}
