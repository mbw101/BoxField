using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BoxField
{
    class Box
    {
        // need colour
        public int x, y, size;
        public Color colour;
       

        public Box(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;
            colour = Color.White;
        }

        public Box(int _x, int _y, int _size, Color _colour)
        {
            x = _x;
            y = _y;
            size = _size;
            colour = _colour;
        }

        public void Move(int _x, int _y)
        {
            x += _x;
            y += _y;
        }
    }
}
