using PCJack.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCJack.ConnectionService.Common
{
    public class NetService 
    {
        public virtual string GenerateUri(string IP)
        {
            return String.Format("http://" + IP + ":8082/system/basic");
        }
        public virtual RequestState PerformOperation(Action v)
        {
            try
            {
                v.Invoke();
                return RequestState.Correct;
            }
            catch (Exception e)
            {
                if (e.InnerException.GetType() == typeof(System.ServiceModel.FaultException))
                {
                    return RequestState.ServiceModelFault;
                }
                else
                {
                    return RequestState.UnknownError;
                    throw new InvalidOperationException(String.Format("Unknows comms fault - ", e.InnerException));
                }
            }
        }
    }
}
