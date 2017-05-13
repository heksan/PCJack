using PCJack.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCJack.Interfaces.Services
{
    public interface IYouTubeClient
    {
        void OpenYoutube();
        Task<ObservableCollection<IPlayableElement>> GetListedVideos();
        void Play(string title);
        void PlayPause();
        Task<string> GetVideoTitle();
        void PreviousVideo();
        void Replay();
        void NextVideo();
        void Search(string v);
    }
}
