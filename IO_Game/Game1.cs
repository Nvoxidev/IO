using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        SoundEffect _sound;
        int countdown = 0;
        int countDuration = 100;
        float currentTime = 0f;
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
                {0,0,0,0,0,0,0,0,0,1,1,1,1,1,1},
                {0,0,0,0,0,0,0,0,0,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}

            }, 50);
                                 
            _rover.Load(Content);
                       
            _hud = new Sprite("sprites/hud",new Point(0,0),new Point(750,55));
            _hud.LoadContent(Content);

            _music = Content.Load<Song>("audio/Midnight Noir L");
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_music);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState myKeyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();

          
            
            
            if (Keyboard.HasBeenPressed(Keys.Space) == true)
            {
                _sound = Content.Load<SoundEffect>("audio/drillBitShot");
                _sound.Play();

                _rover.Shoot(Content, _rover.Location, (Player.Direction)shotDirection);
            }
            if (Keyboard.HasBeenPressed(Keys.LeftShift) == true)
            {
                _rover.PlantMine(Content, _rover.Location);
                _sound = Content.Load<SoundEffect>("audio/mineShot");
                _sound.Play(0.4f, 0.0f, 0.0f);
            }
            foreach(ColissionTiles tile in _map.ColissionTiles) 
            {
                _rover.Colission(tile.rectangle,_map.Width, _map.Height);
            }      
            _rover.Update(gameTime);
            
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
            if (_rover.Location.Y <= 340)
            {
                depth = 0;
            }
            else
            {
                depth = (_rover.Location.Y - 340) / 50;
            }
            
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            countdown = (countDuration - (int)currentTime);

            if (countdown <= 0)
            {
                countdown = 0;
            }
            
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
            _rover.Draw(_spriteBatch);        
            _hud.Draw(_spriteBatch, Color.White);
            _spriteBatch.DrawString(_genericFont, "Depth: " + depth + "m", new Vector2(615, 14), Color.White);
            _spriteBatch.DrawString(_genericFont, "Energy: " + countdown + "%" , new Vector2(16, 14), Color.White);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
