using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Scribby
{
    class Canvas_Image
    {
        public IRandomAccessStream img { get; set; }
        public int width { get; set; }

        public int height { get; set; }



    }
}
