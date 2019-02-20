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

        int boxCounter;

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            //TODO - set game start values
            Box b1 = new Box(25, 24, 20);
            leftBoxes.Add(b1);

            Box b2 = new Box(125, 24, 20);
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

            if (boxCounter % 5 == 0)
            {
                Box b1 = new Box(25, 24, 20);
                leftBoxes.Add(b1);

                Box b2 = new Box(125, 24, 20);
                rightBoxes.Add(b2);
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // draw boxes to screen
            foreach (Box b in leftBoxes)
            {
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }

            foreach (Box b in rightBoxes)
            {
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }
        }
    }
}
