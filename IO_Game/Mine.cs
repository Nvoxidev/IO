using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace IO_Game
{
    class Mine: Sprite
    {
        public Mine(ContentManager contentManager) : this(contentManager, new Point())
        {

        }

        public Mine(ContentManager contentManager, Point location) : base("sprites/Mine", location, new Point(50, 25))
        {
            this.LoadContent(contentManager);
        }

        public void Drop()
        {
            this.Location = new Point(Location.X, Location.Y+3);
        }
    }
}

