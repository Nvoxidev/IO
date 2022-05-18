using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IO_Game
{
    class Map
    {
        private List<ColissionTiles> colissionTiles = new List<ColissionTiles>();
        private Random _rnd;
       

        public List<ColissionTiles> ColissionTiles
        {
            get { return colissionTiles; }
        }

        private int height;
        private int width;

        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }

        public Map()
        {
        }
        public void Generate(int[,] map, int size)
        { 
            _rnd = new Random();
            
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    if (number > 0)
                    {
                        ColissionTiles.Add(new ColissionTiles(_rnd.Next(1,4), new Rectangle(x * size, y * size, size, size)));

                        width = (x + 1) * size;
                        height = (y + 1) * size;
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ColissionTiles tile in colissionTiles)
            {
                tile.Draw(spriteBatch);
            }

        }
    }
}
