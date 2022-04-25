using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace IO_Game
{
    class Player : Sprite
    {
        public List<Projectile> drillBit;
        public List<Mine> mine;


        public Player() :base("sprites/Rover_Idle", new Point(50, 480), new Point(105, 75)) 
        {
            drillBit = new List<Projectile>();
            mine = new List<Mine>();
        }               
        public void Move(string direction)
        {
            if (direction == "r")
            {
                Location = new Point(Location.X + 5, Location.Y);
            }
            else if(direction == "l")
            {
                Location = new Point(Location.X - 5, Location.Y);
            }
        }

        public void Shoot(ContentManager content, Point location)
        {
            
            drillBit.Add(new Projectile(content, location));
            
        }
        public void PlantMine(ContentManager content, Point location)
        {

            mine.Add(new Mine(content, location));

        }
    }
}
