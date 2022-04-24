using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IO_Game
{
    class Player : Sprite
    {
        public List<Projectile> drillBit;
        

        public Player() :base("sprites/Rover_Idle", new Point(50, 480), new Point(105, 75)) 
        {
            drillBit = new List<Projectile>();
        }               
        public void Move(string direction)
        {
            if (direction == "r")
            {
                this.Location = new Point(this.Location.X + 5, this.Location.Y);
            }
            else if(direction == "l")
            {
                this.Location = new Point(this.Location.X - 5, this.Location.Y);
            }
        }

        public void Shoot(ContentManager content, Point location)
        {
            
            drillBit.Add(new Projectile(content, location));
            
        }
    }
}
