using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace IO_Game
{
    class Projectile : Sprite
    {
        public enum Direction { right, left }

        public Projectile(ContentManager contentManager) : this(contentManager, new Point(),Direction.left)
        {

        }

        public Projectile(ContentManager contentManager, Point location,Direction direction) : base("sprites/DrillBit", location + new Point(50, 25), new Point(30, 15))
        {
            LoadContent(contentManager);
        }

        public void Move(Direction direction)
        {
            Location = new Point(Location.X + 8, Location.Y);
        }
    }
}



