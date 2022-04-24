using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IO_Game
{
    class Projectile:Sprite
    {
        public Projectile(ContentManager contentManager) : this(contentManager, new Point())
        {

        }

        public Projectile(ContentManager contentManager, Point location) : base("sprites/DrillBit", location, new Point(50,25))
        {
            this.LoadContent(contentManager);
        }

        public void MoveLeft()
        {
            this.Location = new Point(this.Location.X + 8, this.Location.Y);
        }
    }
}
