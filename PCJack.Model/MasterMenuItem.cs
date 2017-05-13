using PCJack.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PCJack.Model
{
    public class MasterMenuItem :MenuItem, IMasterMenuItem
    {
        public string ResourcePath { get; set; }
    }
}
