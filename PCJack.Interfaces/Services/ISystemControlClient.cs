using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCJack.Interfaces.Services
{
    public interface ISystemControlClient
    {
        bool CheckConnection(string IP);
        double GetVolume();
        void SetVolume(double value);
        void ShutDown();
    }
}
