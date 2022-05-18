using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace IO_Game
{
    class Player : Sprite
    {
        public enum Direction { right, left }
        public List<Projectile> drillBit;
        public List<Mine> mine;


        public Player() :base("sprites/rover", new Point(50, 340), new Point(80, 60)) 
        {
            drillBit = new List<Projectile>();
            mine = new List<Mine>();
        }               
        public void Move(string direction)
        {
            if (direction == "up")
            {
                Location = new Point(Location.X, Location.Y - 5);
            }
            else if(direction == "left")
            {
                Location = new Point(Location.X - 5, Location.Y);
            }
            if (direction == "down")
            {
                Location = new Point(Location.X, Location.Y + 5);
            }
            else if (direction == "right")
            {
                Location = new Point(Location.X + 5, Location.Y);
            }
        }
        public void Shoot(ContentManager content, Point location, Direction direction)
        {
            
            drillBit.Add(new Projectile(content, location, (Projectile.Direction)direction));
            
        }
        public void PlantMine(ContentManager content, Point location)
        {

            mine.Add(new Mine(content, location));

        }
    }
}
