using System;
using System.IO;

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
               new Rooms(){ room = "A1", up = false, down = false, right = true, left = false, description = "Welcome traveller!\n On your journey seeking out knowledge of the mysterious B.I.T. study you arrive are at the entrance of a massive labyrinth that consists of several rooms.\n Your goal is it to reach room C3 and obtain your dilpoma!"},
               new Rooms(){ room = "A2", up = false, down = true, right = false, left = false, description = "It's a dead end. 'You must answer a question to obtain a useful item' is written on the wall." },
               new Rooms(){ room = "A3", up = true, down = true, right = true, left = false, description = "You enter another room with two exits. You can go Up or Down or turn back again. " },
               new Rooms(){ room = "A4", up = true, down = false, right = false, left = false, description = "There's a sheet of paper on top of the stone. You might need a pen." },
               new Rooms(){ room = "B1", up = false, down = false, right = true, left = true, description = "The first room you step in is filled with a lot of useless stuff. " },
               new Rooms(){ room = "B2", up = false, down = true, right = true, left = false, description = "You are relieved you survived another room and are looking forward to some kind of reward but another Question is the only think that awaits you here" },
               new Rooms(){ room = "B3", up = true, down = true, right = false, left = true, description = "Not even that room could stop you. You feel like you are getting smarter and feel unstoppable. There are two ways to go here. Left or Down?" },
               new Rooms(){ room = "B4", up = true, down = false, right = true, left = false, description = "To continue on, you must answer a question correctly." },
               new Rooms(){ room = "C1", up = false, down = false, right = true, left = true, description = "You step into a pitch black room. You can't see a thing except the way back you just came from" },
               new Rooms(){ room = "C2", up = false, down = false, right = true, left = true, description = "You enter the next room and a different question awaits you. Answer correctly to move on but you can also go back." },
               new Rooms(){ room = "C3", up = false, down = true, right = false, left = false, description = "Congratulations! You have answered all questions correctly and collected important items. You have finally found a BIT Diploma!" },
               new Rooms(){ room = "C4", up = true, down = false, right = true, left = true, description = "There's a door. You need a key to remove the lock." },
               new Rooms(){ room = "D1", up = false, down = true, right = false, left = true, description = "With the torch you had no trouble finding the way into the next room. As you step into it a hugh question apears on the wall in front of you. Answer correctly or feel the punishment." },
               new Rooms(){ room = "D2", up = true, down = true, right = false, left = true, description = "You survived one of the many Questions and find yourself in a room with two ways. You can go left or down" },
               new Rooms(){ room = "D3", up = true, down = true, right = false, left = false, description = "It's a dead end. But there is something odd about this room." },
               new Rooms(){ room = "D4", up = true, down = false, right = false, left = true, description = "You have to successfully break down the wall. Goodluck!" }
            };

            roomRequires = new Requires[] //@TODO: Room requrements go here
            {
                new Requires(){ room = "A1", item = null},
                new Requires(){ room = "A2", item = null },
                new Requires(){ room = "A3", item = null },
                new Requires(){ room = "A4", item = "pen" },
                new Requires(){ room = "B1", item = null },
                new Requires(){ room = "B2", item = null },
                new Requires(){ room = "B3", item = null },
                new Requires(){ room = "B4", item = null },
                new Requires(){ room = "C1", item = null },
                new Requires(){ room = "C2", item = null },
                new Requires(){ room = "C3", item = null },
                new Requires(){ room = "C4", item = null },
                new Requires(){ room = "D1", item = null },
                new Requires(){ room = "D2", item = null },
                new Requires(){ room = "D3", item = null },
                new Requires(){ room = "D4", item = null }
            };

            questions = new Questions[]
            {
                new Questions() { room = "A1", answered = true },
                new Questions() { room = "A2", question = "What is a DoS attack?\n\ta - A denial of service attack\n\tb - A phishing attack\n\tc - A information intercepting attack\n\td - A denial of server attack", answer = 'a', reward = "hammer", answered = false },
                new Questions() { room = "A3", answered = true },
                new Questions() { room = "A4", question = "Three Question Exam", answer = 'a', reward = "key", answered = false },
                new Questions() { room = "B1", question = "Do you want the torch? y for yes, n for no", answer = 'y', reward = "torch", answered = false },
                new Questions() { room = "B2", question = "What is the first stage of group work?\n\ta - Storming\n\tb - Forming\n\tc - Norming\n\td - Performing", answer = 'b', answered = false },
                new Questions() { room = "B3", answered = true },
                new Questions() { room = "B4", question = "Expand this math equation! 4*(x+4)^2\n\ta - 4x^2+32x+64\n\tb - 4x^2+16x+16\n\tc - 8x^2+16x+64\n\td - Not Expandable", answer = 'a', answered = false },
                new Questions() { room = "C1", answered = true },
                new Questions() { room = "C2", question = "How do you print Hello World out to the screen in C#?\n\ta - Console.ReadLine(Hello World);\n\tb - Console.Read(Hello World);\n\tc - Console.WriteLine(Hello World);\n\td - WriteOut(Hello World);", answer = 'c', answered = false },
                new Questions() { room = "C3", question = "What is the fifth layer of the OSI model?\n\ta - Data Link\n\tb - Physical\n\tc - Session\n\td - Transport", answer = 'c', answered = false },
                new Questions() { room = "C4", question = "Did you enjoy this game?\n\ta - Yes", answer = 'a', reward = "diploma", answered = false },
                new Questions() { room = "D1", question = "What does BIOS stand for?\n\ta - Basic input output system\n\tb - Basic information on system\n\tc - Basic integrated output system\n\td - Basic isolated operating system", answer = 'a', answered = false },
                new Questions() { room = "D2", answered = true },
                new Questions() { room = "D3", answered = true },
                new Questions() { room = "D4", question = "Under which conditions will this code run: if ((x<5) && (x>2))\n\ta - Both statements need to be true\n\tb - Both statements need to be false\n\tc - One statement only has to be right\n\td - it will never run",


                    answer = 'a', reward = "pen", answered = false }
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
                sw.WriteLine("100,A1,false,false,false,false,false");
                sw.Close();
            }
            StreamReader sr = new StreamReader(curFile);
            string temp = sr.ReadLine();
            sr.Close();
            string[] stats = temp.Split(',');

            player[0].health = Convert.ToInt32(stats[0]);
            player[0].room = stats[1].TrimEnd(' ').TrimStart(' ');
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
            string question = questions[z].question;
            //Console.WriteLine(questions[z].answered);

            if (!questions[z].answered)
            {

                if (x == 'A' && y == '4')
                {
                    Console.WriteLine("special Room");
                }
                else
                {

                    Console.WriteLine(question);
                    char response = Console.ReadKey().KeyChar;
                    if (response == questions[z].answer)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Congratulations, you got the question correct.");
                        Console.ReadLine();
                        player[0].room = room;
                        questions[z].answered = true;
                        Menu(currentRoom, rooms, questions, roomRequires, player);
                    }
                    else
                    {
                        Console.WriteLine("You got it wrong. you loose 25 health");
                        player[0].health = player[0].health - 25;
                        Console.ReadLine();
                        Menu(currentRoom, rooms, questions, roomRequires, player);
                    }
                }

                if (player[0].health < 1)
                {
                    //You died
                    Console.WriteLine("You died. Please try again");
                    Console.ReadLine();
                    player[0].health = 100;
                    currentRoom[0] = 'A';
                    currentRoom[1] = '1';
                    for (int i = 0; i < questions.Length; i++) //reset all questions to unanswered
                    {
                        if (questions[i].question != null)
                        {
                            questions[i].answered = false;
                        }
                    }
                    WriteFile(player, currentRoom);
                    Menu(currentRoom, rooms, questions, roomRequires, player);
                }
            }
            else
            {
                Menu(currentRoom, rooms, questions, roomRequires, player);
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
            Console.WriteLine("You are currently in room " + room + ".");
            Console.WriteLine();

            Console.WriteLine("What would you like to do (press h for help) ? ");
            string input = Console.ReadLine();
            input = input.ToLower();
            if (input.Contains("help"))
            {
                Console.Clear();
                DisplayHelp();
                Console.ReadLine();
                Menu(currentRoom, rooms, questions, roomRequires, player);
            }
            else if (input.Contains("up"))
            {
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
                }
            }
            else if (input.Contains("down"))
            {
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
                    MoveIntoRoom(newRoom, rooms, questions, roomRequires, player);
                }
            }
            else if (input.Contains("left"))
            {
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
                    MoveIntoRoom(newRoom, rooms, questions, roomRequires, player);
                }
            }
            else if (input.Contains("right"))
            {
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
                    MoveIntoRoom(newRoom, rooms, questions, roomRequires, player);
                }
                else
                {
                    Console.WriteLine("You can't go into that room at this time.");
                }
            }
            else if (input.Contains("look"))
            {
                Console.Clear();
                Console.Write("You can move ");
                if (rooms[z].up) Console.Write("up ");
                if (rooms[z].down) Console.Write("down ");
                if (rooms[z].left) Console.Write("left ");
                if (rooms[z].right) Console.Write("right ");
                Console.WriteLine();

                if (questions[z].reward != null)
                {
                    Console.WriteLine("There appears to be a " + questions[z].reward + " on the ground.");
                }
                Console.ReadLine();
                Menu(currentRoom, rooms, questions, roomRequires, player);
            }
            else if (input.Contains("info"))
            {
                Console.Clear();
                Console.WriteLine(rooms[z].description);
                Console.ReadLine();
                Menu(currentRoom, rooms, questions, roomRequires, player);
            }
            else if (input.Contains("save"))
            {
                WriteFile(player, currentRoom);
                Menu(currentRoom, rooms, questions, roomRequires, player);
            }
            else if (input.Contains("quit") || input.Contains("exit") || input.Contains("end"))
            {
                Environment.Exit(0);
            }
                
            Console.ReadLine();
        }

        static char selection(int z, Rooms[] rooms)
        {
            return ' ';
        }

        static void DisplayHelp()
        {
            Console.WriteLine("Type either up, down, left, right to move in that direction.");
            Console.WriteLine("\ts to save\n\te to exit");
        }

        static void Requires1()
        {
            new Requires() { room = "A2", item = "pen" };

        }

    }
}
