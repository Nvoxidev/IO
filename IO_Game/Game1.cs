using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IO_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Player rover;
        Sprite stone;
        Sprite bgSky;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            rover = new Player();
            stone = new Sprite();
            bgSky = new Sprite();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bgSky= new Sprite("sprites/SpaceFinal", new Point(0, 0), new Point(1200, 630));
            bgSky.LoadContent(Content);
            
            rover.LoadContent(Content);

            stone = new Sprite("sprites/StoneSet", new Point(0,550), new Point(800,50));
            stone.LoadContent(Content);

            


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState myKeyboard = Keyboard.GetState();

            if (myKeyboard.IsKeyDown(Keys.Left))
            {
                rover.Move(false);
            }
            else if (myKeyboard.IsKeyDown(Keys.Right))
            {
                rover.Move(true);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            bgSky.Draw(this._spriteBatch, Color.White);
            rover.Draw(this._spriteBatch, Color.White);
            stone.Draw(this._spriteBatch, Color.White);
            

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
