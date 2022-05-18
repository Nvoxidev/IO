using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;



namespace IO_Game
{
    class Tiles
    {
        protected Texture2D texture;
        private Rectangle rectangle;

        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        private static ContentManager content;
        public ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
    class ColissionTiles : Tiles
    {
        public ColissionTiles(int i, Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("sprites/tile" + i);
            this.Rectangle = newRectangle;
        }
    }
}
