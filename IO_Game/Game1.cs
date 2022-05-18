using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace IO_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _genericFont;

        Player _rover;
        Sprite _stone;
        Sprite _bgSky;
        Sprite _hud;
        Sprite _bgStone;
        Map _map;
        Tiles _tiles;
        Song _music;
        string kbrState;
        int shotDirection;
        int depth;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 750;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _rover = new Player();
            _stone = new Sprite();
            _bgSky = new Sprite();
            _bgStone = new Sprite();
            _hud = new Sprite();
            _map = new Map();
            _tiles = new Tiles();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use Content to load your game content here

            //_music = Content.Load<Song>("audio/music");
            
            _genericFont = Content.Load<SpriteFont>("fonts/genericFont");

            _bgSky= new Sprite("sprites/SpaceFinal", new Point(0, 0), new Point(800, 600));
            _bgSky.LoadContent(Content);
 
            _bgStone= new Sprite("sprites/Stonebg", new Point(0, 400), new Point(800, 600));
            _bgStone.LoadContent(Content);

            _tiles.Content = Content;
            _map.Generate(new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}

            }, 50);
                                 
            _rover.LoadContent(Content);
                       
            _hud = new Sprite("sprites/hud",new Point(0,0),new Point(800,55));
            _hud.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState myKeyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();

            if (myKeyboard.IsKeyDown(Keys.W))
            {
                _rover.Move(kbrState = "up");
            }
            if (myKeyboard.IsKeyDown(Keys.A))
            {
                _rover.Move(kbrState = "left");
            }
            if (myKeyboard.IsKeyDown(Keys.S))
            {
                _rover.Move(kbrState = "down");
            }
            if (myKeyboard.IsKeyDown(Keys.D))
            {
                _rover.Move(kbrState = "right");
            }
            if (Keyboard.HasBeenPressed(Keys.Space) == true)
            {
                    _rover.Shoot(Content, _rover.Location, (Player.Direction) shotDirection); 
            }
            if (Keyboard.HasBeenPressed(Keys.LeftShift) == true)
            {
                _rover.PlantMine(Content, _rover.Location);
            }

            foreach (var item in _rover.drillBit)
            {
                if (Keyboard.HasBeenPressed(Keys.A))
                {
                    shotDirection = 1;
                }
                else
                {
                    shotDirection = 0;
                }
                    item.Move((Projectile.Direction)shotDirection);
            }
            foreach (var item in _rover.mine)
            {
                item.Drop();
            }
            
            depth = (_rover.Location.Y - 270) / 50;



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _bgSky.Draw(_spriteBatch, Color.White);
            _bgStone.Draw(_spriteBatch, Color.White);
            _map.Draw(_spriteBatch);
            foreach (var item in _rover.drillBit)
            {
                item.Draw(_spriteBatch, Color.White);
            }
            foreach (var item in _rover.mine)
            {
                item.Draw(_spriteBatch, Color.White);
            }
            _rover.Draw(_spriteBatch, Color.White);        
            _hud.Draw(_spriteBatch, Color.White);
            _spriteBatch.DrawString(_genericFont, "Depth: " + depth + "m", new Vector2(680, 13), Color.White);
            
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
