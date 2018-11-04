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

            Rooms[] rooms = new Rooms[] //create empty array to pass through with ref
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

            PopulateArrays(ref rooms, ref items, ref questions, ref roomRequires); //build initial room direction array
            OpenFile(player, currentRoom);
            //moveRoom(currentRoom, rooms, items, questions, roomRequires);
            Menu(currentRoom, rooms, items, questions, roomRequires, player);
            WriteFile(player, currentRoom);
            Console.ReadLine();
        }

        static void PopulateArrays(ref Rooms[] rooms, ref Items[] items, ref Questions[] questions, ref Requires[] roomRequires)
        {

            rooms = new Rooms[] //create empty array to pass through with ref
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
                new Requires(){ room = "A2", item = "torch"},
            };

            questions = new Questions[]
            {
                new Questions(){ room = "A2", question = "What is a DoS attack?\n\ta - Answer 1\n\tb - Answer 2", answer = 'a', reward = "pen"}
            };

            items = new Items[]
            {
                new Items() {room = "A1", torch = true }
            };

            Console.WriteLine(questions[0].question);
#if DEBUG
            Console.ReadLine();
#endif
        }

        static void OpenFile(Player[] player, char[] currentRoom)
        {
            string curFile = @"save.txt";
            if (!File.Exists(curFile)) // If file doesn't exist, create a new save file
            {
                StreamWriter sw = new StreamWriter(curFile);
                sw.WriteLine("100, A1, false, false, false, false, false");
                sw.Close();
            }
            StreamReader sr = new StreamReader(curFile);
            string temp = sr.ReadLine();
            sr.Close();
            string[] stats = temp.Split(',');

            player[0].health = Convert.ToInt32(stats[0]);
            player[0].room = stats[1].Trim(' ');
            player[0].torch = Convert.ToBoolean(stats[2]);
            player[0].hammer = Convert.ToBoolean(stats[3]);
            player[0].key = Convert.ToBoolean(stats[4]);
            player[0].pen = Convert.ToBoolean(stats[5]);
            player[0].diploma = Convert.ToBoolean(stats[6]);

            // Gets the saved room and loads it into the current room
            currentRoom[0] = Convert.ToChar(stats[1].Substring(0,1));
            currentRoom[1] = Convert.ToChar(stats[1].Substring(1,1));

        }

        static void WriteFile(Player[] player, char[] currentRoom)
        {
            string room = Convert.ToString(currentRoom[0]) + currentRoom[1];
            string curFile = @"save.txt";
            StreamWriter sw = new StreamWriter(curFile);
            string saveLine = player[0].health + "," + player[0].room + "," + player[0].torch + "," + player[0].hammer + "," + player[0].key + "," + player[0].pen + "," + player[0].diploma;
            sw.WriteLine(saveLine);
            sw.Close();
        }

        static void moveRoom(char[] currentRoom, Rooms[] rooms, Items[] items, Questions[] questions, Requires[] roomRequires)
        {
            char x = currentRoom[0];
            char y = currentRoom[1];
            string room = Convert.ToString(x) + y;
            int z = FindRoom(room, rooms);
            
        }

        static bool CanMoveIntoRoom(char[] currentRoom, Rooms[] rooms, Items[] items, Questions[] questions, Requires[] roomRequires)
        {

            return false;
        }

        static int FindRoom(string room, Rooms[] rooms)
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                if (rooms[i].room == room)
                {
                    return i;
                }
            }
            return 99;
        }

        static void Menu(char[] currentRoom, Rooms[] rooms, Items[] items, Questions[] questions, Requires[] roomRequires, Player[] player)
        {
            char x = currentRoom[0];
            char y = currentRoom[1];
            string room = Convert.ToString(x) + y;
            int z = FindRoom(room, rooms);
            Console.Clear();
            Console.WriteLine("You are currently in room " + room + ". " + rooms[z].description);
            Console.Write("You can move ");
            if (rooms[z].up) Console.Write("(u)p ");
            if (rooms[z].down) Console.Write("(d)own ");
            if (rooms[z].left) Console.Write("(l)eft ");
            if (rooms[z].right) Console.Write("(r)ight ");

            Console.WriteLine("What would you like to do (press h for help) ? ");
            char a = Console.ReadKey().KeyChar;
            a = char.ToLower(a);
            switch (a)
            {
                case 'h':
                    Console.Clear();
                    DisplayHelp();
                    Console.ReadLine();
                    Menu(currentRoom, rooms, items, questions, roomRequires, player);
                    break;
                case 'u':
                    if (!rooms[z].up)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid move. Please try again.");
                        Console.ReadLine();
                        Menu(currentRoom, rooms, items, questions, roomRequires, player);
                    }
                    currentRoom[0]--;
                    if (CanMoveIntoRoom(currentRoom, rooms, items, questions, roomRequires));
                    break;
                case 'd':
                    if (!rooms[z].down)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid move. Please try again.");
                        Console.ReadLine();
                        Menu(currentRoom, rooms, items, questions, roomRequires, player);
                    }
                    currentRoom[0]++;
                    if (CanMoveIntoRoom(currentRoom, rooms, items, questions, roomRequires));
                    break;
                case 'l':
                    if (!rooms[z].left)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid move. Please try again.");
                        Console.ReadLine();
                        Menu(currentRoom, rooms, items, questions, roomRequires, player);
                    }
                    currentRoom[1]--;
                    if (CanMoveIntoRoom(currentRoom, rooms, items, questions, roomRequires)) ;
                    break;
                case 'r':
                    if (!rooms[z].right)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid move. Please try again.");
                        Console.ReadLine();
                        Menu(currentRoom, rooms, items, questions, roomRequires, player);
                    }
                    currentRoom[1]++;
                    if (CanMoveIntoRoom(currentRoom, rooms, items, questions, roomRequires)) ;
                    break;
                case 's':
                    WriteFile(player, currentRoom);
                    break;
                default:
                    break;
            }
        }

        static char selection(int z, Rooms[] rooms)
        {
            return ' ';
        }

        static void DisplayHelp()
        {
            Console.WriteLine("Press either u, d, l, r to move in a direction.");
            Console.WriteLine("Press s to save, l to look in the room and p to pick up the item");
        }

        static void QuestionsJonas()
        {
            new Questions() { room = "A2", question = "What is a DoS attack?\n\ta - Answer 1\n\tb - Answer 2", answer = 'a', reward = "pen" };
        }

        static void Requires1()
        {
            new Requires() { room = "A2", item = "pen" };
            
        }

    }
}
