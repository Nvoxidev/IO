using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;

namespace IO_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _genericFont;
        private SpriteFont _endFont;
        private SpriteFont _endFont2;
        Player _rover;
        Tiles _stoneTiles;
        Sprite _bgSky;
        Sprite _hud;
        Sprite _bgStone;
        Sprite _goldStone;
        Sprite _generator;
        Sprite _bgEnd;
        Song _music;
        SoundEffect _sound;
        Random _rand;
        int probability;
        int energy = 100;
        bool gameOver = false;
        string kbrState;
        string alert = "";
        int score;

            
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 810;
            _graphics.PreferredBackBufferHeight = 945;
            _graphics.ApplyChanges();
        }
        protected override void Initialize()
        {
            _rover = new Player();
            _bgSky = new Sprite();
            _bgStone = new Sprite();
            _hud = new Sprite();
            _stoneTiles = new Tiles();
            _generator = new Sprite();
            _goldStone = new Sprite();
            _rand = new Random();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _genericFont = Content.Load<SpriteFont>("fonts/genericFont");
            
            _endFont = Content.Load<SpriteFont>("fonts/endFont");
            _endFont2 = Content.Load<SpriteFont>("fonts/endFont2");

            _bgSky = new Sprite("sprites/SpaceFinal", new Point(0, 0), new Point(810, 600));
            _bgSky.LoadContent(Content);

            _bgStone = new Sprite("sprites/Stonebg", new Point(0, 300), new Point(810, 600));
            _bgStone.LoadContent(Content);

            //1st floor
            _stoneTiles.Floor(new Point(0, 300), 15);
            //2nd floor
            _stoneTiles.Floor(new Point(535, 420), 5);
            _stoneTiles.Floor(new Point(0, 420), 10);
            //3rd floor
            _stoneTiles.Floor(new Point(225, 540), 11);
            _stoneTiles.Floor(new Point(0, 540), 3);
            //4th floor
            _stoneTiles.Floor(new Point(135, 660), 9);
            _stoneTiles.Floor(new Point(720, 660), 2);
            //5th floor
            _stoneTiles.Floor(new Point(225, 780), 13);
            _stoneTiles.Floor(new Point(0, 780), 3);
            //Final floor
            _stoneTiles.Floor(new Point(-90, 900), 22);
            
            _stoneTiles.LoadContent(Content);

            _goldStone = new Sprite("sprites/GoldStone", new Point(765, 855), new Point(45, 45));
            _goldStone.LoadContent(Content);

            _generator = new Sprite("sprites/generator", new Point(20, 170), new Point(130, 130));
            _generator.LoadContent(Content);
            
            _rover.LoadContent(Content);
            
            _hud = new Sprite("sprites/hud", new Point(0, 0), new Point(810, 55));
            _hud.LoadContent(Content);

            _bgEnd = new Sprite("sprites/bgEnd", new Point(0, 0), new Point(810, 945));
            _bgEnd.LoadContent(Content);

            _music = Content.Load<Song>("audio/Midnight Noir L");
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_music);
        }
        protected override void Update(GameTime gameTime)
        {
            if (gameOver == false)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();


                if (Keyboard.IsPressed(Keys.W) == true)
                {
                    _rover.Move(Player.Direction.Up);
                }
                if (Keyboard.IsPressed(Keys.A) == true)
                {
                    _rover.Move(Player.Direction.Left);

                }
                if (Keyboard.IsPressed(Keys.S) == true)
                {
                    _rover.Move(Player.Direction.Down);

                }
                if (Keyboard.IsPressed(Keys.D) == true)
                {
                    _rover.Move(Player.Direction.Right);
                }
                if (Keyboard.HasBeenPressed(Keys.Space) == true)
                {
                    _sound = Content.Load<SoundEffect>("audio/drillBitShot");
                    _sound.Play();

                    _rover.Shoot(Content, _rover.Location);
                }

                _rover.Location = new Point(Wrap.WrapAround(_rover.Location.X, 0 - 80, 800), _rover.Location.Y);

                foreach (var item in _rover.drillBit)
                {
                    item.Move();

                    if (item.rectangle.Intersects(_goldStone.rectangle))
                    {
                        score += 1;
                    }
                }

                probability = _rand.Next(1, 51);
                if (probability == 1)
                {
                    energy--;
                }
                else if (probability == 50)
                {
                    energy -= 2;
                }

                if (energy <= 0)
                {
                    energy = 0;
                    gameOver = true;
                }
                if (_rover.rectangle.Intersects(_generator.MyRectangle))
                {
                    if (Keyboard.HasBeenPressed(Keys.F) == true)
                    {
                        try
                        {
                            if (! File.Exists("Content/score.txt"))
                            {
                                File.Create("Content/score.txt");
                            }

                            File.WriteAllText("D:/Code/Git-hub/IO/IO_Game/Content/score.txt", score.ToString() + "\n");
                        }
                        catch (Exception e)
                        {
                            alert = e.Message;
                        }
                        gameOver = true;
                        score *= 2;
                    }
                    if (energy <= 98)
                    {
                        energy += 2;
                    }
                }
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _bgSky.Draw(_spriteBatch, Color.White);
            _bgStone.Draw(_spriteBatch, Color.White);
            foreach (var item in _rover.drillBit)
            {
                item.Draw(_spriteBatch, Color.White);
            }
            _stoneTiles.Draw(_spriteBatch);
            _generator.Draw(_spriteBatch, Color.White);
            _goldStone.Draw(_spriteBatch, Color.White);
            _rover.Draw(_spriteBatch, Color.White);
            _hud.Draw(_spriteBatch, Color.White);
            if (_rover.rectangle.Intersects(_generator.MyRectangle))
            {
                _spriteBatch.DrawString(_genericFont, "Press \"F\" to pack up ", new Vector2(70, 130), Color.White);
            }
            _spriteBatch.DrawString(_genericFont, "Score: " + score, new Vector2(670, 17), Color.White);
            _spriteBatch.DrawString(_genericFont, "Energy: " + energy + "%", new Vector2(25, 17), Color.White);

            if (gameOver == true)
            {

                _bgEnd.Draw(_spriteBatch, Color.White);
                _spriteBatch.DrawString(_endFont, "Final score: " + score, new Vector2(240, 200), Color.White);
                _spriteBatch.DrawString(_endFont2, "High scores", new Vector2(315, 300), Color.White);
      
                if (alert != "")
                {
                    _spriteBatch.DrawString(_endFont2, "Score not saved", new Vector2(285, 480), Color.White);
                }
                else 
                {
                    _spriteBatch.DrawString(_endFont2, "Score saved Successfully", new Vector2(215, 480), Color.White);
                }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
