using PCJack.Common;
using PCJack.Common.Enums;
using PCJack.ConnectionService.Common;
using PCJack.ConnectionService.SystemControlReference;
using PCJack.Interfaces.Common;
using PCJack.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCJack.ConnectionService
{
    public class SystemControlClient :NetService, ISystemControlClient
    {
        private readonly IApplicationPropertiesWrapper _appPropertiesWrapper;
        private static readonly string _testConnectionString = "Hello";
        private SystemServiceClient _systemServiceCient;

        public SystemControlClient(IApplicationPropertiesWrapper appPropertiesWrapper)
        {
            if (appPropertiesWrapper == null)
            {
                throw new ArgumentNullException("appPropertiesWrapper");
            }
            _appPropertiesWrapper = appPropertiesWrapper;
            _systemServiceCient = new SystemServiceClient(SystemServiceClient.EndpointConfiguration.BasicHttpBinding_ISystemService);

        }
        public bool CheckConnection(string IP)
        {
            EventHandler<CheckConnectionCompletedEventArgs> callback = null;
            _systemServiceCient.Endpoint.Address = new System.ServiceModel.EndpointAddress(GenerateUri(IP));
            var tcs = new TaskCompletionSource<string>();
            callback = (sender, args) =>
            {
                RequestState state = PerformOperation(() => { tcs.SetResult(args.Result); });
                if (state != RequestState.Correct)
                {
                    tcs.SetResult("");
                }
                _systemServiceCient.CheckConnectionCompleted -= callback;
            };
            _systemServiceCient.CheckConnectionCompleted += callback;
            _systemServiceCient.CheckConnectionAsync();
            return (tcs.Task.Result.Equals(_testConnectionString));
            

        }
        public double GetVolume()
        {
            EventHandler<GetVolumeCompletedEventArgs> callback = null;
            var tcs = new TaskCompletionSource<double>();
            callback = (sender, args) =>
            {
                RequestState state = PerformOperation(() => { tcs.SetResult(args.Result); });
                if (state != RequestState.Correct)
                {
                    tcs.SetResult(0);
                }
                _systemServiceCient.GetVolumeCompleted -= callback;
            };
            _systemServiceCient.GetVolumeCompleted += callback;
            _systemServiceCient.GetVolumeAsync();
            return (tcs.Task.Result);
        }

        public void SetVolume(double value)
        {
            _systemServiceCient.ChangeVolumeAsync(value);
        }

        public void ShutDown()
        {
            _systemServiceCient.ShutDownAsync();
        }
    }
}
