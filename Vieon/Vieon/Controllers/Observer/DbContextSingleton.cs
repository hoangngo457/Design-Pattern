using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vieon.Models;

namespace Vieon.Controllers.Observer
{
    public class DbContextSingleton
    {
        private static VieONEntities _instance ;

        // Đảm bảo rằng chỉ có một đối tượng được tạo ra
        private DbContextSingleton() { }

        public static VieONEntities getInstance
        {
            get
            {
                    // Khởi tạo đối tượng nếu chưa tồn tại
                    _instance = new VieONEntities();
                return _instance;
            }
        }
    }
}