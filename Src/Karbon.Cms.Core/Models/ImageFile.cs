using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Cms.Core.Models
{
    public class ImageFile : File, IImageFile
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
