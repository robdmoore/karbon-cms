using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Web.Model
{
    public class PageModel : IPageModel
    {
        public virtual string Slug { get; set; }
        public virtual string Url { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime Modified { get; set; }
    }
}
