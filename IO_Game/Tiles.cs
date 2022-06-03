using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace IO_Game
{
    class Tiles
    { 
            public static List<Tile> tiles;

            public Tiles()
            {
                tiles = new List<Tile>();
            }

            public void LoadContent(ContentManager content)
            {
                try
                {
                    foreach (var item in tiles)
                    {
                        item.LoadContent(content);
                    }
                }
                catch(Exception e)
                {
                    throw e;
                }
            }

            public void Floor(Point location, int numveces)
            {
                AddWall(location.X, location.Y, numveces, true);
            }

            public void VerticalWall(Point location, int numveces)
            {
                AddWall(location.X, location.Y, numveces, false);
            }

            private void AddWall(int x, int y, int numveces, bool isHorizontal)
            {
                for (int i = 0; i < numveces; i++)
                {
                    tiles.Add(new Tile(new Point(45, 45), new Point(x, y)));
                    if (isHorizontal)
                    {
                        x += 45;
                    }
                    else
                    {
                        y += 45;
                    }
                }
            }

            public void Draw(SpriteBatch sp)
            {
                foreach (var item in tiles)
                {
                    item.Draw(sp, Color.White);
                }
            }
        }
    }

