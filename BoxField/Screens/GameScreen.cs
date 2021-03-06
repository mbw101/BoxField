﻿using System;
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

        Box player;
        Bitmap playerImage;
        Rectangle actualShip = new Rectangle(5, 13, 14, 20);
        int playerWidth = 52;
        int playerHeight = 32;

        Random random = new Random();

        int boxCounter;
        int boxSpeed = 5;
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

            player = new Box(75, 450, 20);
            playerImage = new Bitmap(Properties.Resources.ship);
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
                b.Move(boxSpeed);
            }

            foreach (Box b in rightBoxes)
            {
                b.Move(boxSpeed);
            }

            if (leftArrowDown)
            {
                player.Move(5, "left");
            }

            if (rightArrowDown)
            {
                player.Move(5, "right");
            }

            // check for collision between player and boxes (foreach loop with two lists)
            foreach (Box b in leftBoxes.Union(rightBoxes))
            {
                if (player.Collision(b))
                {
                    gameLoop.Stop();
                }
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
                index++;
            }
            else if (!moveRight)
            {
                index--;
            }

            if (index >= 360)
            {
                // move left
                moveRight = false;
            }
            else if (index < 50)
            {
                moveRight = true;
            }

            if (boxCounter % 5 == 0)
            {               
                Box b1 = new Box(25 + (xValue * index), 24, 20, MakeRandomColour());
                leftBoxes.Add(b1);

                Box b2 = new Box(125 + (xValue * index), 24, 20, b1.colour);
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

            // draw player ship
            boxBrush.Color = Color.White;
            e.Graphics.FillEllipse(boxBrush, player.x, player.y, player.size, player.size);
            //e.Graphics.DrawImage(playerImage, player.x, player.y);
        }
    }
}
