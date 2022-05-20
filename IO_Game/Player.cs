using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IO_Game
{
    class Player : Sprite
    {
        public enum Direction { right, left }
        public List<Projectile> drillBit;
        public List<Mine> mine;
        private Texture2D texture;
        private Vector2 velocity;
        public Vector2 position = new Vector2(50, 327);
        private Rectangle rectangle;
        float x = 0;
        float y = 0;

        private bool hasJumped = false;
        private Vector2 Position
        {
            get { return position; }
        }

        public Player()
        {
            drillBit = new List<Projectile>();
            mine = new List<Mine>();
        }
        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("sprites/rover");
        }
        public void Update(GameTime gameTime)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            Move(gameTime);

            if (velocity.Y < 10)
            {
                velocity.Y += 0.4f;
            }
        }
        public void Move(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            }
            else velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.W) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y -= 9f;
                hasJumped = true;
            }
        }
        public void Colission(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }
            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - newRectangle.Width - 2;
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
            }
            if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;

            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;

            x = position.X;
            y = position.Y;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
        public void Shoot(ContentManager content,Point location, Direction direction)
        {
                        drillBit.Add(new Projectile(content, new Point((int)x,(int)y), (Projectile.Direction)direction));

        }
        public void PlantMine(ContentManager content, Point location)
        {

            mine.Add(new Mine(content, new Point((int)x + 40, (int)y)));

        }
    }
}
