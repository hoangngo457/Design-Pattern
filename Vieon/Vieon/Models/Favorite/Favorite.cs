using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vieon.Models
{
    public partial class Favorite
    {
        public int ID_PhimYeuThich { get; set; }
        public string TenPhim { get; set; }
        public int ID_Phim { get; set; }

        public virtual Phim Phim { get; set; }
    }
}