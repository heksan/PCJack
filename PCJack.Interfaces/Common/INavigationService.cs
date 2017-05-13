using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PCJack.Interfaces.Common
{
    public interface INavigationService
    {
        void InitializeLandingPage();
        void NavigateToModal(Type t);
        void NavigateToOverlay(Type t);
        void NavigateTo(Type t);
    }
}
