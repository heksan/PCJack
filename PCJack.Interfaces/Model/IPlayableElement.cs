using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCJack.Interfaces.Model
{
    public interface IPlayableElement
    {
        string Title { get; set; }
        string Length { get; set; }
        string Link { get; set; }
    }
}
