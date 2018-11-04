using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation
{
    class Program
    {
        public struct Rooms
        {
            public string room;
            public bool up;
            public bool down;
            public bool right;
            public bool left;
            public string description;
        }

        public struct Items
        {
            public string room;
            public bool torch;
            public bool hammer;
            public bool key;
            public bool pen;
            public bool diploma;
        }

        public struct Player
        {
            public int health;
            public string room;
            public bool torch;
            public bool hammer;
            public bool key;
            public bool pen;
            public bool diploma;
        }

        public struct Questions
        {
            public string room;
            public string question;
            public char answer;
            public bool answered;
            public string reward;
        }

        public struct Requires
        {
            public string room;
            public string item;
        }

        static void Main()
        {
            char[] currentRoom = new char[] { 'A', '1' };

            Rooms[] direction = new Rooms[] //create empty array to pass through with ref
            {
               new Rooms(){ room = "", up = false, down = false, right = false, left = false }
            };

            Items[] items = new Items[] //create empty array to pass through with ref
            {
               new Items(){ room = "", torch = false, hammer = false, key = false, pen = false, diploma = false }
            };

            Requires[] roomRequires = new Requires[] //create empty array to pass through with ref
            {
               new Requires(){ room = null, item = null }
            };


            Player[] player = new Player[1]; //create empty array to pass through with ref

            Questions[] questions = new Questions[]
            {
                new Questions(){ room = null, question = null, answer = ' ', answered = false }
            };

            OpenFile(player);
            PopulateArrays(ref direction, ref items, ref questions, ref roomRequires); //build initial room direction array
            moveRoom(currentRoom, direction, items, questions, roomRequires);
            WriteFile(player, currentRoom);
            Console.ReadLine();
        }

        static void PopulateArrays(ref Rooms[] direction, ref Items[] items, ref Questions[] questions, ref Requires[] roomRequires)
        {

            direction = new Rooms[] //create empty array to pass through with ref
            {
               new Rooms(){ room = "A1", up = false, down = false, right = true, left = false, description = "Description goes here"},
               new Rooms(){ room = "A2", up = false, down = true, right = false, left = false, description = "Description goes here" },
               new Rooms(){ room = "A3", up = true, down = true, right = true, left = false, description = "Description goes here" },
               new Rooms(){ room = "A4", up = true, down = false, right = false, left = false, description = "Description goes here" },
               new Rooms(){ room = "B1", up = false, down = false, right = true, left = true, description = "Description goes here" },
               new Rooms(){ room = "B2", up = false, down = true, right = true, left = false, description = "Description goes here" },
               new Rooms(){ room = "B3", up = true, down = true, right = false, left = true, description = "Description goes here" },
               new Rooms(){ room = "B4", up = true, down = false, right = true, left = false, description = "Description goes here" },
               new Rooms(){ room = "C1", up = false, down = false, right = true, left = true, description = "Description goes here" },
               new Rooms(){ room = "C2", up = false, down = false, right = true, left = true, description = "Description goes here" },
               new Rooms(){ room = "C3", up = false, down = true, right = false, left = false, description = "Description goes here" },
               new Rooms(){ room = "C4", up = true, down = false, right = true, left = true, description = "Description goes here" },
               new Rooms(){ room = "D1", up = false, down = true, right = false, left = true, description = "Description goes here" },
               new Rooms(){ room = "D2", up = true, down = true, right = false, left = true, description = "Description goes here" },
               new Rooms(){ room = "D3", up = true, down = true, right = false, left = false, description = "Description goes here" },
               new Rooms(){ room = "D4", up = true, down = false, right = false, left = true, description = "Description goes here" }
            };

            roomRequires = new Requires[] //@TODO: Room requrements go here
            {
                new Requires(){ room = "A2", item = "pen"},
            };

            questions = new Questions[]
            {
                new Questions(){ room = "A2", question = "What is a DoS attack?\n\ta - Answer 1\n\tb - Answer 2", answer = 'a', reward = "pen"}
            };
            Console.WriteLine(questions[0].question);
#if DEBUG
            Console.ReadLine();
#endif
        }

        static void OpenFile(Player[] player)
        {
            string curFile = @"save.txt";
            if (!File.Exists(curFile))
            {
                StreamWriter sw = new StreamWriter(curFile);
                sw.Close();
            }
            StreamReader sr = new StreamReader(curFile);
            string temp = sr.ReadLine();
            sr.Close();
            string[] stats = temp.Split(',');

            player[0].health = Convert.ToInt32(stats[0]);
            player[0].room = stats[1];
            player[0].torch = Convert.ToBoolean(stats[2]);
            player[0].hammer = Convert.ToBoolean(stats[3]);
            player[0].key = Convert.ToBoolean(stats[4]);
            player[0].pen = Convert.ToBoolean(stats[5]);
            player[0].diploma = Convert.ToBoolean(stats[6]);
            //Console.WriteLine();

        }

        static void WriteFile(Player[] player, char[] currentRoom)
        {
            string room = Convert.ToString(currentRoom[0]) + currentRoom[1];
            Console.WriteLine(room);
            Console.ReadLine();
            string curFile = @"save.txt";
            StreamWriter sw = new StreamWriter(curFile);
            string saveLine = player[0].health + ", " + player[0].room + ", " + player[0].torch + ", " + player[0].hammer + ", " + player[0].key + ", " + player[0].pen + ", " + player[0].diploma;
            sw.WriteLine(saveLine);
            sw.Close();
        }

        static void moveRoom(char[] currentRoom, Rooms[] direction, Items[] items, Questions[] questions, Requires[] roomRequires)
        {
            char x = currentRoom[0];
            char y = currentRoom[1];
            Console.WriteLine(x + "-" + y);
        }

        static void QuestionsJonas()
        {
            new Questions() { room = "A2", question = "What is a DoS attack?\n\ta - Answer 1\n\tb - Answer 2", answer = 'a', reward = "pen" };
        }

        static void Requires()
        {
            new Requires() { room = "A2", item = "pen" };
            
        }

    }
}
