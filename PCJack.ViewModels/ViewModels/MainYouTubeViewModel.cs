using PCJack.Common;
using PCJack.Interfaces.Model;
using PCJack.Interfaces.Services;
using PCJack.Interfaces.ViewModelInterfaces;
using PCJack.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PCJack.ViewModels.ViewModels
{
    public class MainYouTubeViewModel : ViewModelController, IMainYouTubeViewModel
    {
        private bool _isBusy;
        private bool _isPlayingPageOpened;
        private string _playPauseImage=ApplicationStrings.PauseButtonImage;
        private string _currentVideoTitle = string.Empty;
        private double _volumeValue;
        private ObservableCollection<IPlayableElement> _videos;

        private readonly IYouTubeClient _youTubeService;
        private readonly ISystemControlClient _systemService;
        public MainYouTubeViewModel(IYouTubeClient youTubeService, ISystemControlClient systemService)
        {
            if (youTubeService == null)
            {
                throw new ArgumentNullException("youTubeService");
            }
            if (systemService==null)
            {
                throw new ArgumentNullException("systemService");
            }
            _youTubeService = youTubeService;
            _systemService = systemService;
            VolumeValue = _systemService.GetVolume();
            _youTubeService.OpenYoutube();
            Task.Run(async () =>
           {
               IsBusy = true;
               await RefreshData(4000);
           });

        }
        #region props
        public ObservableCollection<IPlayableElement> VideosList
        {
            get
            {
                return _videos;
            }
            set
            {
                _videos = value;
                OnPropertyChanged("VideosList");
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                ((Command)RefreshDataCommand).ChangeCanExecute();
            }
        }
        public string CurrentVideoTitle
        {
            get
            {
                return _currentVideoTitle;
            }
            set
            {
                _currentVideoTitle = value;
                if (!string.IsNullOrEmpty(_currentVideoTitle))
                {
                    IsPlayingPageOpened = true;
                }
                else
                {
                    IsPlayingPageOpened = false;
                }
                OnPropertyChanged();
            }
        }
        public string PlayPauseImage
        {
            get
            {
                return _playPauseImage;
            }
            set
            {
                _playPauseImage = value;
                OnPropertyChanged();
            }
        }
        public double VolumeValue
        {
            get
            {
                return _volumeValue;
            }
            set
            {
                _volumeValue = value;
                _systemService.SetVolume(value);
                OnPropertyChanged();
            }
        }
        public bool IsPlayingPageOpened
        {
            get
            {
                return _isPlayingPageOpened;
            }
            set
            {
                _isPlayingPageOpened = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Commands
        public ICommand RefreshDataCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await RefreshData(0);
                });
            }
        }
        public ICommand PlayItem
        {
            get
            {
                return new Command(async (e) =>
                {
                    _youTubeService.Play(e as string);
                    CurrentVideoTitle = e as string;
                    await RefreshData();
                    PlayPauseImage = ApplicationStrings.PauseButtonImage;
                });
            }
        }
        public ICommand PreviousVideo
        {
            get
            {
                return new Command(async () =>
                {
                    _youTubeService.PreviousVideo();
                    await RefreshData();
                });
            }
        }
        public ICommand Replay
        {
            get
            {
                return new Command(() =>
                {
                    _youTubeService.Replay();
                });
            }
        }
        public ICommand PausePlay
        {
            get
            {
                return new Command(() =>
                {
                    _youTubeService.PlayPause();
                    FlipPlayPauseImage();
                });
            }
        }

        private void FlipPlayPauseImage()
        {
            if (PlayPauseImage.Equals(ApplicationStrings.PauseButtonImage))
            {
                PlayPauseImage = ApplicationStrings.PlayButtonImage;
            }
            else
            {
                PlayPauseImage = ApplicationStrings.PauseButtonImage;
            }
        }

        public ICommand NextVideo
        {
            get
            {
                return new Command(async () =>
                {
                    _youTubeService.NextVideo();
                    await RefreshData();
                });
            }
        }

        public ICommand Search
        {
            get
            {
                return new Command(async (e) =>
                {
                    _youTubeService.Search(e as string);
                    await RefreshData();
                });
            }
        }

        #endregion
        public async Task RefreshData(int delay = 1000)
        {
            IsBusy = true;
            await Task.Delay(delay);
            VideosList = await _youTubeService.GetListedVideos();
            CurrentVideoTitle =await _youTubeService.GetVideoTitle();
            IsBusy = false;
        }





    }
}
