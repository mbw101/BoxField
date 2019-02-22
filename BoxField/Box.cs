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

        public void Move(int speed)
        {
            y += speed;
        }

        public void Move(int speed, string direction)
        {
            if (direction == "left")
            {
                x -= speed;
            }
            else if (direction == "right")
            {
                x += speed;
            }
        }

        public bool Collision(Box b)
        {
            Rectangle rect1 = new Rectangle(x, y, size, size);
            Rectangle rect2 = new Rectangle(b.x, b.y, b.size, b.size);

            if (rect1.IntersectsWith(rect2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
