using PCJack.ConnectionService.YouTubeReference;
using PCJack.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCJack.ConnectionService
{
    public class YouTubePlayableElement : PlayableElement, IPlayableElement
    {

        public YouTubePlayableElement(PlayableElement x)
        {
            Title = x.Title;
            Length = x.Length;
            Link = x.Link;
        }
    }
}
