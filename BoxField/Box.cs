using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxField
{
    class Box
    {
        // need colour
        public int x, y, size;
        public string colour;

        public Box(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;
            colour = "White";
        }

        public Box(int _x, int _y, int _size, string _colour)
        {
            x = _x;
            y = _y;
            size = _size;
            colour = _colour;
        }

        public void Move()
        {

        }
    }
}
