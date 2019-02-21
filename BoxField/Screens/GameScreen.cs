using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown;

        //used to draw boxes on screen
        SolidBrush boxBrush = new SolidBrush(Color.White);

        // a list to hold a column of boxes        
        List<Box> leftBoxes = new List<Box>();
        List<Box> rightBoxes = new List<Box>();

        Random random = new Random();

        int boxCounter;
        int xValue = 2;
        int index = 0;
        bool moveRight = false;

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public Color MakeRandomColour()
        {
            int rColour = random.Next(0, 255);
            int gColour = random.Next(0, 255);
            int bColour = random.Next(0, 255);
            return Color.FromArgb(rColour, gColour, bColour);
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            // set game start values
            Box b1 = new Box(25, 24, 20, MakeRandomColour());
            leftBoxes.Add(b1);

            Box b2 = new Box(125, 24, 20, b1.colour);
            rightBoxes.Add(b2);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;           
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            // player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            // update location of all boxes (drop down screen)
            foreach (Box b in leftBoxes)
            {
                b.y += 5;
            }

            foreach (Box b in rightBoxes)
            {
                b.y += 5;
            }

            // remove box if it has gone of screen
            if (leftBoxes[0].y > this.Height - leftBoxes[0].size)
            {
                leftBoxes.RemoveAt(0);
            }
            if (rightBoxes[0].y > this.Height - rightBoxes[0].size)
            {
                rightBoxes.RemoveAt(0);
            }

            // add new box if it is time
            boxCounter++;

            // increment index to create pattern
            if (moveRight)
            {
                if (index >= 25)
                {
                    index++;
                    moveRight = false;
                }              
            }
            else if (!moveRight)
            {
                index--;
            }



            if (boxCounter % 5 == 0)
            {
                
                Box b1 = new Box(25 - (xValue * index), 24, 20, MakeRandomColour());
                leftBoxes.Add(b1);

                Box b2 = new Box(125 - (xValue * index), 24, 20, b1.colour);
                rightBoxes.Add(b2);
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // draw boxes to screen
            foreach (Box b in leftBoxes)
            {
                boxBrush.Color = b.colour;
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }

            foreach (Box b in rightBoxes)
            {
                boxBrush.Color = b.colour;
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }
        }
    }
}
