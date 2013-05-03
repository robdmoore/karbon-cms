using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Web.Model
{
    public interface IPageModel
    {
        string Slug { get; }
        string Url { get; }
        DateTime Created { get; }
        DateTime Modified { get; }
    }
}
