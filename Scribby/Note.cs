using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribby
{
    class Note
    {
        public string gpr_coordinate { get; set; }

        public string Media_Type { get; set; }

        public int Like { get; set; }

        public string UserId { get; set; }

        public double Yaw { get; set; }

        public double Pitch { get; set; }

        public string Media_Url { get; set; }

        public string User_Access;
    }
}
