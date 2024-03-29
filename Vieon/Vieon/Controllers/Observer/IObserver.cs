using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vieon.Models;

namespace Vieon.Controllers.Observer
{
    public interface IObserver
    {
        void Update(PhimYeuThich phimYeuThich, TapPhim tapPhim);
    }
}