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
               new Rooms(){ room = "A1", up = false, down = false, right = true, left = false, description = "You are at the entrance of the cave."},
               new Rooms(){ room = "A2", up = false, down = true, right = false, left = false, description = "It's a dead end. 'You must answer a question to obtain a useful item' is written on the wall." },
               new Rooms(){ room = "A3", up = true, down = true, right = true, left = false, description = "There are two ways. Head North or turn South?" },
               new Rooms(){ room = "A4", up = true, down = false, right = false, left = false, description = "There's a sheet of paper on top of the stone. You might need a pen." },
               new Rooms(){ room = "B1", up = false, down = false, right = true, left = true, description = "A torch is on the ground." },
               new Rooms(){ room = "B2", up = false, down = true, right = true, left = false, description = "To move ahead through the cave, you must answer a question correctly." },
               new Rooms(){ room = "B3", up = true, down = true, right = false, left = true, description = "There are two ways. Continue North or turn East?" },
               new Rooms(){ room = "B4", up = true, down = false, right = true, left = false, description = "To continue on, you must answer a question correctly." },
               new Rooms(){ room = "C1", up = false, down = false, right = true, left = true, description = "You are currently in a very dark area. It's very hard to see." },
               new Rooms(){ room = "C2", up = false, down = false, right = true, left = true, description = "There's only one way ahead. But you need to correctly answer a question to continue." },
               new Rooms(){ room = "C3", up = false, down = true, right = false, left = false, description = "Congratulations! You have answered all questions correctly and collected important items. You have finally found a BIT Diploma!" },
               new Rooms(){ room = "C4", up = true, down = false, right = true, left = true, description = "There's a door. You need a key to remove the lock." },
               new Rooms(){ room = "D1", up = false, down = true, right = false, left = true, description = "To continue on, you must answer this question correctly." },
               new Rooms(){ room = "D2", up = true, down = true, right = false, left = true, description = "There are two ways. Continue North or turn East?" },
               new Rooms(){ room = "D3", up = true, down = true, right = false, left = false, description = "It's a dead end. But one wall looks different." },
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
                new Requires(){ room = "C1", item = "torch" },
                new Requires(){ room = "C2", item = null },
                new Requires(){ room = "C3", item = "key" },
                new Requires(){ room = "C4", item = null },
                new Requires(){ room = "D1", item = null },
                new Requires(){ room = "D2", item = null },
                new Requires(){ room = "D3", item = null },
                new Requires(){ room = "D4", item = "hammer" }
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
                new Questions() { room = "C3",question = "Did you enjoy this game?\n\ta - Yes", answer = 'a', reward = "diploma", answered = false },
                new Questions() { room = "C4", question = "What is the fifth layer of the OSI model?\n\ta - Data Link\n\tb - Physical\n\tc - Session\n\td - Transport", answer = 'c', answered = false },
                new Questions() { room = "D1", question = "What does BIOS stand for?\n\ta - Basic input output system\n\tb - Basic information on system\n\tc - Basic integrated output system\n\td - Basic isolated operating system", answer = 'a', answered = false },
                new Questions() { room = "D2", answered = true },
                new Questions() { room = "D3", answered = true },
                new Questions() { room = "D4", question = "Under which conditions will this code run: if ((x<5) && (x>2))\n\ta - Both statements need to be true\n\tb - Both statements need to be false\n\tc - One statement only has to be right\n\td - it will never run", answer = 'a', reward = "pen", answered = false }
            };


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

        static void ResetPlayer(Player[] player, Questions[] questions, char[] currentRoom)
        {
            string curFile = @"save.txt";
            currentRoom[0] = 'A';
            currentRoom[1] = '1';
            player[0].health = 100;

            for (int i = 0; i < questions.Length; i++) //reset all questions to unanswered
            {
                if (questions[i].question != null)
                {
                    questions[i].answered = false;
                }
            }
            StreamWriter sw = new StreamWriter(curFile);
            sw.WriteLine("100,A1,false,false,false,false,false");
            sw.Close();
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
            Menu(currentRoom, rooms, questions, roomRequires, player);

        }

        static void AskInfo(Questions[] questions, Player[] player, char[] currentRoom, Rooms[] rooms, Requires[] roomRequires)
        {
            char x = currentRoom[0];
            char y = currentRoom[1];
            string room = Convert.ToString(x) + y;
            int z = FindRoom(room, rooms);
            string question = questions[z].question;

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
                Console.ReadLine();
                Menu(currentRoom, rooms, questions, roomRequires, player);
            }
            else
            {
                Menu(currentRoom, rooms, questions, roomRequires, player);
            }
        }

        static bool CanMoveIntoRoom(char[] currentRoom, char[] newRoom, Rooms[] rooms, Questions[] questions, Requires[] roomRequires, Player[] player)
        {
            char x = newRoom[0];
            char y = newRoom[1];
            string nextRoom = Convert.ToString(x) + y;
            int newRoomId = FindRoom(nextRoom, rooms);

            if (roomRequires[newRoomId].item != null)
            {
                string item = roomRequires[newRoomId].item;

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

        static void PickupItem(Player[] player, Rooms[] rooms, Questions[] questions, char[] currentRoom, string input)
        {
            char x = currentRoom[0];
            char y = currentRoom[1];
            char[] newRoom = { x, y };
            string room = Convert.ToString(x) + y;
            int z = FindRoom(room, rooms);
            string[] item = input.Split(' ');
            string unknown = item[item.Length - 1];
            if (input.Contains("pen"))
            {
                if (questions[z].reward == "pen")
                {
                    player[0].pen = true;
                    Console.WriteLine("You managed to pick up the pen");
                }
                else
                {
                    Console.WriteLine("There is no pen in the room.");
                }
            }
            else if (input.Contains("torch"))
            {
                if (questions[z].reward == "torch")
                {
                    player[0].torch = true;
                    Console.WriteLine("You managed to pick up the torch");
                }
                else
                {
                    Console.WriteLine("There is no torch in the room.");
                }
            }
            else if (input.Contains("key"))
            {
                if (questions[z].reward == "key")
                {
                    player[0].key = true;
                    Console.WriteLine("You managed to pick up the key");
                }
                else
                {
                    Console.WriteLine("There is no key in the room.");
                }
            }
            else if (input.Contains("hammer"))
            {
                if (questions[z].reward == "hammer")
                {
                    player[0].hammer = true;
                    Console.WriteLine("You managed to pick up the hammer");
                }
                else
                {
                    Console.WriteLine("There is no hammer in the room.");
                }
            }
            else if (input.Contains("diploma"))
            {
                if (questions[z].reward == "diploma")
                {
                    player[0].diploma = true;
                    Console.WriteLine("You managed to pick up the diploma");
                }
                else
                {
                    Console.WriteLine("There is no diploma in the room.");
                }
            }
            else
            {
                Console.WriteLine("There is no " + unknown + " in the room");
            }
        }

        static void Menu(char[] currentRoom, Rooms[] rooms, Questions[] questions, Requires[] roomRequires, Player[] player)
        {
            Console.Title = "Console Degree - Health: " + player[0].health + "%";
            char x = currentRoom[0];
            char y = currentRoom[1];
            char[] newRoom = { x, y };
            string room = Convert.ToString(x) + y;
            int z = FindRoom(room, rooms);
            Console.Clear();
            Console.WriteLine("You are currently in room " + room + ".");
            Console.WriteLine();

            Console.WriteLine("What would you like to do (type 'help' for help) ? ");
            string input = Console.ReadLine();
            input = input.ToLower();
            if (input.Contains("help"))
            {
                Console.Clear();
                DisplayHelp();
                Console.ReadLine();
                Menu(currentRoom, rooms, questions, roomRequires, player);
            }
            else if (input.Contains("pickup") || input.Contains("pick up"))
            {
                Console.Clear();
                PickupItem(player, rooms, questions, currentRoom, input);
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
                    Menu(currentRoom, rooms, questions, roomRequires, player);
                }
                else
                {
                    Console.WriteLine("You can't go into that room at this time.");
                    Console.ReadLine();
                    Menu(currentRoom, rooms, questions, roomRequires, player);
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
                    Menu(currentRoom, rooms, questions, roomRequires, player);
                }
                else
                {
                    Console.WriteLine("You can't go into that room at this time.");
                    Console.ReadLine();
                    Menu(currentRoom, rooms, questions, roomRequires, player);
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
                    Menu(currentRoom, rooms, questions, roomRequires, player);
                }
                else
                {
                    Console.WriteLine("You can't go into that room at this time.");
                    Console.ReadLine();
                    Menu(currentRoom, rooms, questions, roomRequires, player);
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
                    Menu(currentRoom, rooms, questions, roomRequires, player);
                }
                else
                {
                    Console.WriteLine("You can't go into that room at this time.");
                    Console.ReadLine();
                    Menu(currentRoom, rooms, questions, roomRequires, player);
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
                AskInfo(questions, player, currentRoom, rooms, roomRequires);
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
            else if (input.Contains("reset"))
            {
                Console.Clear();
                ResetPlayer(player, questions, currentRoom);
                Menu(currentRoom, rooms, questions, roomRequires, player);
            }
            else //Display help
            {
                Console.Clear();
                DisplayHelp();
                Console.ReadLine();
                Menu(currentRoom, rooms, questions, roomRequires, player);
            }

            Console.ReadLine();
        }

        static char selection(int z, Rooms[] rooms)
        {
            return ' ';
        }

        static void DisplayHelp()
        {
            Console.WriteLine("Your mission is to obtain a BIT cetificate diploma by answering year 1 questions or whatever you have learnt.");
            Console.WriteLine("You will have a great experience trying to find the things you need.");
            Console.WriteLine("");
            Console.WriteLine("Keep in mind that you will require to use your actual brain for this. I know...It's hard.");
            Console.WriteLine("But don't worry pal! You might do well. Nothing is impossible.");
            Console.WriteLine("");
            Console.WriteLine("Make sure to eat some food before you start because you should not be expecting food here.");
            Console.WriteLine("Unless you like eating questions or.. anything that is not edible. I don't know..I don't judge.");
            Console.WriteLine("Good luck!");
            Console.WriteLine("");
            Console.WriteLine("Type 'up', 'down', 'left', 'right' to move to whatever direction you want.");
            Console.WriteLine("Type 'info' to see how to get out of the room and any items that are in the room");
            Console.WriteLine("Type 'inventory' to display the items you have collected so far and see the amount of life you have left.");
            Console.WriteLine("Type 'save' to save your game and 'exit' to exit the game.");
        }


    }
}
