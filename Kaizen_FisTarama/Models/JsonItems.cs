using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen_FisTarama.Models
{
    public class JsonItems
    {
        public class Root
        {
            public string description { get; set; }
            public BoundingPoly boundingPoly { get; set; }
        }
        public class vertices
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public class BoundingPoly
        {
            public List<vertices> vertices { get; set; }
        }

        public class  Referance
        {
            public string desc { get; set; }
            public int xLeftTop { get; set; }
            public int xLeftBottom { get; set; }
            public int xRightTop { get; set; }
            public int xRightBottom { get; set; }
            public int threshold { get; set; }
        }
    }
}
