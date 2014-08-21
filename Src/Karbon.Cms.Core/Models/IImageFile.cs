using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Cms.Core.Models
{
    public interface IImageFile : IFile
    {
        int Width { get; set; }
        int Height { get; set; }
    }
}
