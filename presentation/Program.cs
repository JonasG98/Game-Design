﻿using System;
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

            Requires[] roomRequires = new Requires[] //create empty array to pass through with ref
            {
               new Requires(){ room = null, item = null }
            };


            Player[] player = new Player[1]; //create empty array to pass through with ref

            Questions[] questions = new Questions[]
            {
                new Questions(){ room = null, question = null, answer = ' ', answered = false }
            };

            PopulateArrays(ref rooms, ref questions, ref roomRequires); //build initial room direction array
            OpenFile(player, currentRoom);
            Menu(currentRoom, rooms, questions, roomRequires, player);
            WriteFile(player, currentRoom);
            //Console.ReadLine();
        }

        static void PopulateArrays(ref Rooms[] rooms, ref Questions[] questions, ref Requires[] roomRequires)
        {

            rooms = new Rooms[] //create empty array to pass through with ref
            {
               new Rooms(){ room = "A1", up = false, down = false, right = true, left = false, description = "You are at the entrance of the cave."},
               new Rooms(){ room = "A2", up = false, down = true, right = false, left = false, description = "Description goes here" },
               new Rooms(){ room = "A3", up = true, down = true, right = true, left = false, description = "There are two ways. Head North or turn South?" },
               new Rooms(){ room = "A4", up = true, down = false, right = false, left = false, description = "There's a sheet of paper on top of the stone. You might need a pen." },
               new Rooms(){ room = "B1", up = false, down = false, right = true, left = true, description = "A torch is on the ground." },
               new Rooms(){ room = "B2", up = false, down = true, right = true, left = false, description = "Description goes here" },
               new Rooms(){ room = "B3", up = true, down = true, right = false, left = true, description = "There are two ways. Continue North or turn East?" },
               new Rooms(){ room = "B4", up = true, down = false, right = true, left = false, description = "Description goes here" },
               new Rooms(){ room = "C1", up = false, down = false, right = true, left = true, description = "You are currently in a very dark area. It's very hard to see." },
               new Rooms(){ room = "C2", up = false, down = false, right = true, left = true, description = "Description goes here" },
               new Rooms(){ room = "C3", up = false, down = true, right = false, left = false, description = "Description goes here" },
               new Rooms(){ room = "C4", up = true, down = false, right = true, left = true, description = "There's a door. You need a key to remove the lock." },
               new Rooms(){ room = "D1", up = false, down = true, right = false, left = true, description = "Description goes here" },
               new Rooms(){ room = "D2", up = true, down = true, right = false, left = true, description = "There are two ways. Continue North or turn East?" },
               new Rooms(){ room = "D3", up = true, down = true, right = false, left = false, description = "It's a dead end. But one wall looks different." },
               new Rooms(){ room = "D4", up = true, down = false, right = false, left = true, description = "Description goes here" }
            };

            roomRequires = new Requires[] //@TODO: Room requrements go here
            {
                new Requires(){ room = "A1", item = null},
                new Requires(){ room = "A2", item = null},
                new Requires(){ room = "A3", item = null},
                new Requires(){ room = "A4", item = null},
                new Requires(){ room = "B1", item = null},
                new Requires(){ room = "B2", item = null},
                new Requires(){ room = "B3", item = null},
            };

            questions = new Questions[]
            {
                new Questions(){ room = "A1", answered = true},
                new Questions(){ room = "A2", question = "a2What is a DoS attack?\n\ta - Answer 1\n\tb - Answer 2", answer = 'a', reward = ""},
                new Questions(){ room = "A3", question = "What is a DoS attack?\n\ta - Answer 1\n\tb - Answer 2", answer = 'a', reward = ""},
                new Questions(){ room = "A4", question = "What is a DoS attack?\n\ta - Answer 1\n\tb - Answer 2", answer = 'a', reward = ""},
                new Questions(){ room = "B1", question = "b1What is a DoS attack?\n\ta - Answer 1\n\tb - Answer 2", answer = 'a', reward = "torch"},
                new Questions(){ room = "B2", question = "What is a DoS attack?\n\ta - Answer 1\n\tb - Answer 2", answer = 'a', reward = ""}
            };

            Console.WriteLine(questions[0].question);
#if DEBUG
            //Console.ReadLine();
#endif
        }

        static void OpenFile(Player[] player, char[] currentRoom)
        {
            string curFile = @"save.txt";
            if (!File.Exists(curFile)) // If file doesn't exist, create a new save file
            {
                StreamWriter sw = new StreamWriter(curFile);
                sw.WriteLine("100,A1,false,false,false,false,false");
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
            currentRoom[0] = Convert.ToChar(stats[1].Substring(0, 1));
            currentRoom[1] = Convert.ToChar(stats[1].Substring(1, 1));

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

        static void MoveIntoRoom(char[] currentRoom, Rooms[] rooms, Questions[] questions, Requires[] roomRequires, Player[] player)
        {
            char x = currentRoom[0];
            char y = currentRoom[1];
            string room = Convert.ToString(x) + y;
            int z = FindRoom(room, rooms);
            Console.WriteLine(z);
            string question = questions[z].question;
            Console.WriteLine(question);
            char response = Console.ReadKey().KeyChar;
            if (response == questions[z].answer)
            {
                Console.WriteLine("Congratulations, you got the question correct.");
                if (questions[z].reward != null)
                {
                    switch (questions[z].reward)
                    {
                        case "torch":
                            player[0].torch = true;
                            break;
                        case "pen":
                            player[0].pen = true;
                            break;
                        case "hammer":
                            player[0].hammer = true;
                            break;
                        case "key":
                            player[0].key = true;
                            break;
                        case "diploma":
                            player[0].diploma = true;
                            break;
                        default:
                            break;
                    }
                }
                Menu(currentRoom, rooms, questions, roomRequires, player);
            }
            else
            {
                Console.WriteLine("You got it wrong. you loose 25 health");
            }
        }

        static bool CanMoveIntoRoom(char[] currentRoom, char[] newRoom, Rooms[] rooms, Questions[] questions, Requires[] roomRequires, Player[] player)
        {
            char x = currentRoom[0];
            char y = currentRoom[1];
            string nextRoom = Convert.ToString(x) + y;
            int newRoomId = FindRoom(nextRoom, rooms);

            if (roomRequires[newRoomId].item != null)
            {
                string item = roomRequires[newRoomId].item;
                Console.WriteLine(item);
                Console.ReadLine();

                switch (item)
                {
                    case "torch":
                        if (player[0].torch)
                        {
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("You are missing the torch");
                        }
                        break;
                    case "pen":
                        if (player[0].pen)
                        {
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("You are missing the pen");
                        }
                        break;
                    case "hammer":
                        if (player[0].hammer)
                        {
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("You are missing the hammer");
                        }
                        break;
                    case "key":
                        if (player[0].key)
                        {
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("You are missing the key");
                        }
                        break;
                    case "diploma":
                        if (player[0].diploma)
                        {
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("You are missing the diploma");
                        }
                        break;
                    default:
                        break;
                }
                return false;
            }
            //Console.ReadLine();
            return true;
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

        static void Menu(char[] currentRoom, Rooms[] rooms, Questions[] questions, Requires[] roomRequires, Player[] player)
        {
            char x = currentRoom[0];
            char y = currentRoom[1];
            char[] newRoom = { x, y };
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
                    Menu(currentRoom, rooms, questions, roomRequires, player);
                    break;
                case 'u':
                    Console.Clear();
                    if (!rooms[z].up)
                    {
                        Console.WriteLine("Invalid move. Please try again.");
                        Console.ReadLine();
                        Menu(currentRoom, rooms, questions, roomRequires, player);
                    }
                    newRoom[1]--;
                    if (CanMoveIntoRoom(currentRoom, newRoom, rooms, questions, roomRequires, player))
                    {
                        MoveIntoRoom(newRoom, rooms, questions, roomRequires, player);
                        Console.WriteLine("YES");
                    }
                    else
                    {
                        Console.WriteLine("NO");
                    }
                    break;
                case 'd':
                    Console.Clear();
                    if (!rooms[z].down)
                    {
                        Console.WriteLine("Invalid move. Please try again.");
                        Console.ReadLine();
                        Menu(currentRoom, rooms, questions, roomRequires, player);
                    }
                    newRoom[1]++;
                    if (CanMoveIntoRoom(currentRoom, newRoom, rooms, questions, roomRequires, player))
                    {
                        Console.WriteLine("YES");
                    }
                    else
                    {
                        Console.WriteLine("NO");
                    }
                    break;
                case 'l':
                    Console.Clear();
                    if (!rooms[z].left)
                    {
                        Console.WriteLine("Invalid move. Please try again.");
                        Console.ReadLine();
                        Menu(currentRoom, rooms, questions, roomRequires, player);
                    }
                    newRoom[0]--;
                    if (CanMoveIntoRoom(currentRoom, newRoom, rooms, questions, roomRequires, player))
                    {
                        Console.WriteLine("YES");
                    }
                    else
                    {
                        Console.WriteLine("NO");
                    }
                    break;
                case 'r':
                    Console.Clear();
                    if (!rooms[z].right)
                    {
                        Console.WriteLine("Invalid move. Please try again.");
                        Console.ReadLine();
                        Menu(currentRoom, rooms, questions, roomRequires, player);
                    }
                    newRoom[0]++;
                    if (CanMoveIntoRoom(currentRoom, newRoom, rooms, questions, roomRequires, player))
                    {
                        Console.WriteLine("YES");
                        Console.ReadLine();
                        MoveIntoRoom(newRoom, rooms, questions, roomRequires, player);
                    }
                    else
                    {
                        Console.WriteLine("You can't go into that room at this time.");
                    }
                    break;
                case 's':
                    WriteFile(player, currentRoom);
                    Menu(currentRoom, rooms, questions, roomRequires, player);
                    break;
                case 'e':
                    break;
                default:
                    Menu(currentRoom, rooms, questions, roomRequires, player);
                    break;
            }
            Console.ReadLine();
        }

        static char selection(int z, Rooms[] rooms)
        {
            return ' ';
        }

        static void DisplayHelp()
        {
            Console.WriteLine("Press either u, d, l, r to move in a direction.");
            Console.WriteLine("\ts to save\n\te to exit");
        }

        static void QuestionsJonas()
        {
            new Questions() { room = "A2", question = "What is a DoS attack?\n\ta - Answer 1\n\tb - Answer 2", answer = 'a', reward = "pen", answered = false };
        }

        static void Requires1()
        {
            new Requires() { room = "A2", item = "pen" };

        }

    }
}
