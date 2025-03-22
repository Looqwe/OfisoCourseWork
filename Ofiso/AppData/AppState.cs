using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofiso.AppData
{
        public static class AppState
        {
            public static int CurrentUserId { get; set; }
            public static bool IsAuthenticated => CurrentUserId != 0;
        }
    
}
