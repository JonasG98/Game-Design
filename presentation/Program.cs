using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation
{
    class Program
    {
        public struct Direction
        {
            public string room;
            public bool up;
            public bool down;
            public bool right;
            public bool left;
        }

        static void Main()
        {
            Direction[] direction = new Direction[] //create empty array to pass through with ref
            {
               new Direction(){ room = "", up = false, down = false, right = false, left = false }
            };
            buildArrays(ref direction);
        }

        static void buildArrays(ref Direction[] direction)
        {

            direction = new Direction[] //create empty array to pass through with ref
            {
               new Direction(){ room = "A1", up = false, down = false, right = true, left = false },
               new Direction(){ room = "A2", up = false, down = true, right = false, left = false },
               new Direction(){ room = "A3", up = true, down = true, right = true, left = false },
               new Direction(){ room = "A4", up = true, down = false, right = false, left = false },
               new Direction(){ room = "B1", up = false, down = false, right = true, left = true },
               new Direction(){ room = "B2", up = false, down = true, right = true, left = false },
               new Direction(){ room = "B3", up = true, down = true, right = false, left = true },
               new Direction(){ room = "B4", up = true, down = false, right = true, left = false },
               new Direction(){ room = "C1", up = false, down = false, right = true, left = true },
               new Direction(){ room = "C2", up = false, down = false, right = true, left = true },
               new Direction(){ room = "C3", up = false, down = true, right = false, left = false },
               new Direction(){ room = "C4", up = true, down = false, right = true, left = true },
               new Direction(){ room = "D1", up = false, down = true, right = false, left = true },
               new Direction(){ room = "D2", up = true, down = true, right = false, left = true },
               new Direction(){ room = "D3", up = true, down = true, right = false, left = false },
               new Direction(){ room = "D4", up = true, down = false, right = false, left = true },

            };
            Console.ReadLine();
        }
    }
}
