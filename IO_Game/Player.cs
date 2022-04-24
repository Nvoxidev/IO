using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace IO_Game
{
    class Player : Sprite
    {
        public Player() :base("sprites/Rover_Idle", new Point(50, 483), new Point(116, 68)) 
        {
        
        }
 
        public void Move(bool direction)
        {
            if (direction == true)
            {
                this.Location = new Point(this.Location.X + 5, this.Location.Y);
            }
            else
            {
                this.Location = new Point(this.Location.X - 5, this.Location.Y);
            }
        }
    }
}
