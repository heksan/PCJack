using PCJack.Common;
using PCJack.Common.Enums;
using PCJack.ConnectionService.Common;
using PCJack.ConnectionService.YouTubeReference;
using PCJack.Interfaces.Common;
using PCJack.Interfaces.Services;
using PCJack.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PCJack.ConnectionService
{
    public class YouTubeClient :NetService, IYouTubeClient
    {
        private YouTubeServiceClient _youTubeCient;
        private readonly IApplicationPropertiesWrapper _appPropertiesWrapper;


        public YouTubeClient(IApplicationPropertiesWrapper appPropertiesWrapper)
        {
            if (appPropertiesWrapper == null)
            {
                throw new ArgumentNullException("appPropertiesWrapper");
            }
            _appPropertiesWrapper = appPropertiesWrapper;
            _youTubeCient = new YouTubeServiceClient(YouTubeServiceClient.EndpointConfiguration.BasicHttpBinding_IYouTubeService);
            _youTubeCient.Endpoint.Address = new System.ServiceModel.EndpointAddress(GenerateUri(_appPropertiesWrapper.GetValue<string>(ApplicationStrings.LastKnownIPValue)));
        }


        public void OpenYoutube()
        {
            _youTubeCient.OpenYoutubeAsync();
        }
        
        public async Task<ObservableCollection<IPlayableElement>> GetListedVideos()
        {
            var _taskCompletionSource = new TaskCompletionSource<ObservableCollection<PlayableElement>>();
            EventHandler<GetVideosCompletedEventArgs> callback = null;
            callback = (sender, args) =>
            {
                RequestState state= PerformOperation(() => { _taskCompletionSource.SetResult(args.Result); });
                if (state!=RequestState.Correct)
                {
                    _taskCompletionSource.SetResult(new ObservableCollection<PlayableElement>());
                }
                _youTubeCient.GetVideosCompleted -= callback;
            };
            _youTubeCient.GetVideosCompleted += callback;
            
             _youTubeCient.GetVideosAsync(); 
            
           return (new ObservableCollection<IPlayableElement>(_taskCompletionSource.Task.Result.Select(x=>new YouTubePlayableElement(x))));
            
        }



        public async Task<ObservableCollection<IPlayableElement>> GetListedVideos(int requested)
        {
            EventHandler<GetVideosNCompletedEventArgs> callback = null;
            var _taskCompletionSource = new TaskCompletionSource<ObservableCollection<PlayableElement>>();
            callback = (sender, args) =>
            {
                RequestState  state= PerformOperation(() => { _taskCompletionSource.SetResult(args.Result); });
                if (state != RequestState.Correct)
                {
                    _taskCompletionSource.SetResult(null);
                }
                _youTubeCient.GetVideosNCompleted -= callback;
            };
            _youTubeCient.GetVideosNCompleted += callback;

            _youTubeCient.GetVideosNAsync(requested);

            return (new ObservableCollection<IPlayableElement>(_taskCompletionSource.Task.Result.Select(x => x as YouTubePlayableElement)));

        }
        public override string GenerateUri(string IP)
        {
            return String.Format("http://" + IP + ":8082/youtube/basic");
        }

        public void Play(string title)
        {
            _youTubeCient.OpenVideoAsync(title);
        }

        public void PlayPause()
        {
            _youTubeCient.PausePlayAsync();
        }

        public Task<string> GetVideoTitle()
        {
            EventHandler<GetTittleCompletedEventArgs> callback = null;
            var _taskCompletionSource = new TaskCompletionSource<string>();
            callback = (sender, args) =>
            {
                RequestState state = PerformOperation(()=> _taskCompletionSource.SetResult(args.Result));
                if (state != RequestState.Correct)
                {
                    _taskCompletionSource.SetResult(null);
                }
                _youTubeCient.GetTittleCompleted -= callback;
            };
            _youTubeCient.GetTittleCompleted += callback;

            _youTubeCient.GetTittleAsync();

            return (_taskCompletionSource.Task);
        }

        public void PreviousVideo()
        {
            _youTubeCient.BackAsync();
        }

        public void Replay()
        {
            _youTubeCient.ReplayAsync();
        }

        public void NextVideo()
        {
            _youTubeCient.NextAsync();
        }

        public void Search(string searchFor)
        {
            _youTubeCient.SearchForVideosAsync(searchFor);
        }
       
    }
}
