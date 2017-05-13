using Ninject;
using PCJack.ViewModels;
using PCJack.ViewModels.ViewModelInterfaces;
using PCJack.Views;
using PCJack.Views.ViewsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCJack.Interfaces.Common;

namespace PCJack.IoC.Kernel
{
    public static class NinjectKernel
    {
        private static StandardKernel _kernel;
        public static StandardKernel Kernel
        {
            get
            {
                if (_kernel == null)
                {
                    _kernel = new StandardKernel();
                }
                return _kernel;
            }
        }

    }
}
