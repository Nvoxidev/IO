using System;
using System.Collections.Generic;
using System.Text;

namespace IO_Game
{
    class Wrap
    {
        public static int WrapAround(int location, int firstWall, int secondWall) 
        {
            if (location <= firstWall) 
            {
                return secondWall;
            }
            if (location >= secondWall)
            {
                return firstWall;
            }
            else 
            {
                return location;
            }
        }

    }
}
