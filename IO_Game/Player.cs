using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;


namespace IO_Game
{
    class Player : Sprite
    {
        public List<Projectile> drillBit;
        public enum Direction
        {
            Right,
            Left,
            Up,
            Down

        }
        
        public Player() : base("sprites/rover", new Point(50, 244), new Point(75, 55))
        {
            drillBit = new List<Projectile>();
        }
        public void Move(Direction direction)
        {
            Rectangle tempLocation = rectangle;
            switch (direction)
            {
                case Direction.Right:
                    tempLocation.Location = new Point(Location.X + 5, Location.Y);
                    break;
                case Direction.Left:
                    tempLocation.Location = new Point(Location.X - 5, Location.Y);
                    break;
                case Direction.Down:
                    tempLocation.Location = new Point(Location.X, Location.Y + 5);
                    break;
                case Direction.Up:
                    tempLocation.Location = new Point(Location.X, Location.Y - 20);
                    break;
                default:
                    break;

            }

            foreach (var item in Tiles.tiles)
            { 

                if (tempLocation.Intersects(item.rectangle))
                {
                    return;
                }
            }
            
            Location = tempLocation.Location;
        }

        public void Shoot(ContentManager content, Point location)
        {
             drillBit.Add(new Projectile(content, new Point(Location.X,Location.Y)));

        }

    }
}
