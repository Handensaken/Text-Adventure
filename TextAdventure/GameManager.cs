using System;
using System.Collections.Generic;
namespace TextAdventure
{
    public class GameManager
    {

        public enum Rooms
        {
            Start,
            TableRoom,
            Corridor,
            LockedRoom,
            ThirdRoom,
            BackOutside,
            BossRoom
        }
        public Rooms CurrentRoom = Rooms.Start;
        Hero hero;

        public void MainGameLoop()
        {
            while (true)
            {
                Start();
            }
        }
        private Rooms Movement()    //add choices for each room
        {
            string prompt = $"Choose direction, current room is {CurrentRoom}";
            switch (CurrentRoom)
            {
                case Rooms.Start:
                    {
                        switch (Selection(new[] { "Forwards" }, prompt))
                        {
                            case 0:
                                {
                                    return Rooms.TableRoom;
                                }
                            default:
                                {
                                    System.Console.WriteLine("Dev fucked up somewhere");
                                    return CurrentRoom;
                                }
                        }
                    }
                case Rooms.TableRoom:
                    {
                        switch (Selection(new[] { "Back to start", "Forwards" }, prompt))
                        {
                            case 0:
                                {
                                    return Rooms.Start;
                                }
                            case 1:
                                {
                                    return Rooms.Corridor;
                                }
                            default:
                                {
                                    System.Console.WriteLine("Dev fucked up again yeehaw");
                                    return CurrentRoom;
                                }
                        }
                    }
                case Rooms.Corridor:
                    {
                        switch (Selection(new[] { "Backwards", "Forwards", "Right" }, prompt))
                        {
                            case 0:
                                {
                                    return Rooms.TableRoom;
                                }
                            case 1:
                                {
                                    return Rooms.ThirdRoom;
                                }
                            case 2:
                                {
                                    if (hero.Items.Contains("Key"))
                                    {
                                        return Rooms.LockedRoom;
                                    }
                                    else
                                    {
                                        return Movement();
                                    }
                                }
                            default:
                                {
                                    System.Console.WriteLine("Yippie kai ey I done diddeli fucked up again boy");
                                    return CurrentRoom;
                                }
                        }
                    }
                case Rooms.LockedRoom:
                    {
                        switch (Selection(new[] { "Back out", " Forwards" }, prompt))
                        {
                            case 0:
                                {
                                    return Rooms.Corridor;
                                }
                            case 1:
                                {
                                    return Rooms.ThirdRoom;
                                }
                            default:
                                {
                                    System.Console.WriteLine("Ah shit hide ya moonshine boy the debug coppers are comin'");
                                    return CurrentRoom;
                                }
                        }
                    }
                case Rooms.ThirdRoom:
                    {
                        switch (Selection(new[] { "Back to corridor", "Back through right door", "Forwards" }, prompt))
                        {
                            case 0:
                                {
                                    return Rooms.Corridor;
                                }
                            case 1:
                                {
                                    if (hero.Items.Contains("Key"))
                                    {
                                        return Rooms.LockedRoom;
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("Door is locked, I bet I could get in with a key...");
                                        System.Console.WriteLine("press any key to continue");
                                        Console.ReadKey(true);
                                        return Movement();
                                    }
                                }
                            case 2:
                                {
                                    return Rooms.BackOutside;
                                }
                            default:
                                {
                                    System.Console.WriteLine("Aw gee wiz man like this is totes wacko cracko my guy");
                                    return CurrentRoom;
                                }
                        }
                    }
                case Rooms.BackOutside:
                    {
                        BackOutside();
                        return Rooms.BossRoom;
                    }
                case Rooms.BossRoom:
                    {
                        switch (Selection(new[] { "Exit", "Fuck around for a while and then exit" }, prompt))
                        {
                            case 0:
                                {
                                    Exit();
                                    break;
                                }
                            case 1:
                                {
                                    System.Console.WriteLine("you fuck around before leaving");
                                    Exit();
                                    break;
                                }
                        }
                        return CurrentRoom;
                    }
                default:
                    {
                        return CurrentRoom;
                    }
                    /*
                    case end
                    */
            }
        }

        private void Start()
        {
            HeroCreator heroCreator = new HeroCreator();
            hero = heroCreator.HeroSpawner(this);
            Console.ReadLine();
        }
        private void TableRoom()
        {
            System.Console.WriteLine("You are equipped with one wooden sword, and your task is to slay the monster at the end of the adventure. " +
            "In front of you is a stone table with two items on it, a knife and a key. You may only choose one.");
            switch (Selection(new[] { "Knife", "Key" }, "Choose one"))
            {
                case 0:
                    {
                        System.Console.WriteLine("you picked up knife");
                        hero.Items.Add("Knife");
                        break;
                    }
                case 1:
                    {
                        System.Console.WriteLine("you picked up key");
                        hero.Items.Add("Key");
                        break;
                    }
                default:
                    {
                        System.Console.WriteLine("It do be fucked up tho");
                        break;
                    }

            }
            CurrentRoom = Rooms.Corridor;
        }
        private void Corridor()
        {
            System.Console.WriteLine("You exit the room into a dark and dank corridor, cobwebs line the walls and there is a musky smell in the air." +
            "You can make out the shape of a door on the right side of the corridor...");
            KeyPress();
            Movement();
        }
        private void LockedRoom()
        {
            System.Console.WriteLine("Inserting your key into the keyhole and turning it, you hear a satisfying 'click' as the door becomes unlocked" +
            "As the door opens you're met with an unexpected warmth coming from a chest located on the far side of the room.");
            KeyPress();
            switch (Selection(new[] { "yes", "no" }, "Do you want to check the chest?"))
            {
                case 0:
                    {
                        System.Console.WriteLine("As you open the chest you see a faintly red viking sword, decorated with intricite runes." +
                        "The sword gives off a warm sensation.");
                        switch (Selection(new[] { "yes", "no" }, "Do you pick it up?"))
                        {
                            case 0:
                                {
                                    System.Console.WriteLine("As you pick up the sword you realize that you would have to ditch the wooden sword to make room in your scabbard.");
                                    hero.Items.Remove("wooden sword");
                                    hero.Items.Add("Shiny sword");
                                    break;
                                }
                            case 1:
                                {
                                    System.Console.WriteLine("You decide that it would be best to leave the sword be.");
                                    break;
                                }
                        }
                        break;
                    }
                case 1:
                    {
                        System.Console.WriteLine("You get a bad feeling from that chest and decide to leave it be");
                        break;
                    }
            }
        }
        private void ThirdRoom()
        {
            System.Console.WriteLine("As you enter the room your vision tunnels towards the corpse of what you assume is an explorer." +
            "In his cold, lifeless hand lies a shiny amulet");
            KeyPress();
            if (Selection(new[] { "yes", "no" }, "You you want to pick up the amulet?") == 0)
            {
                hero.Items.Add("amulet"); //this will be changed later to a class to accomodate the amulet being cursed or not
                if (RollDie(6) >= 3)
                {
                    System.Console.WriteLine("You feel a surge of energy running throughout your body.");
                }
                else
                {
                    System.Console.WriteLine("you feel a chill running down your spine, as the amulet engraves itself into your hand.");
                }
            }
            else
            {
                System.Console.WriteLine("You decicde not to mess with the belongings of the dead.");
            }
        }
        private void BackOutside()
        {
            System.Console.WriteLine("As you go outside into a large courtyard you are suddenly charged by a large minotaur." +
            "This minotaur aint havin your shit and want to mess your day up royally.");
            KeyPress();
            BossRoom();
        }
        private void BossRoom()
        {
            System.Console.WriteLine("As the minotaur comes to a stop, it roars in your face and you get ready for combat. square tf up bitch");
        }
        private void Exit()
        {
            switch (Selection(new[] { "yes", "no" }, "Play again?"))
            {
                case 0:
                    {
                        //reset everything and start again. Some things will have to be rearranged and shit but we can do that another time
                        break;
                    }
            }
        }







        private void CombatLoop()
        {
            Minotaur minotaur = new Minotaur();
            while (minotaur.Health > 0)
            {
                List<string> choices = new List<string>() { "Mighty kick", "Gallant headbutt" };
                if (hero.Items.Contains("Shiny sword")) { choices.Add("Shiny sword"); }
                else if (hero.Items.Contains("Knife")) { choices.Add("Stabby Knife"); }
            }
        }


        private int RollDie(int amount)
        {
            int Sides = amount;
            Random rand = new Random();
            return rand.Next() % Sides + 1;
        }
        private void KeyPress()
        {
            System.Console.WriteLine("Press a key");
            Console.ReadKey();
        }
        public string TextFeeder()      //returns user input as a string
        {
            return Console.ReadLine();
        }
        public int Selection(string[] choices, string prompt)   //gives the user a choice between a number of choices
        {
            int currentlySelected = 0;
            while (true)
            {
                PrintChoices(choices, currentlySelected, prompt);
                ConsoleKeyInfo key = Console.ReadKey(true); //stores user input data
                Console.Clear();
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            currentlySelected++;
                            currentlySelected = currentlySelected % choices.Length; //makes sure the user can't go outside the bounds of the choice array
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            currentlySelected--;
                            if (currentlySelected < 0)  //This one also makes sure the user can't go out of bounds but modulo is wacko in negative numbers
                            {
                                currentlySelected = choices.Length - 1;
                            }
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            return currentlySelected;   //returns the selection
                        }
                }
            }
        }
        private void PrintChoices(string[] choices, int currentlySelected, string prompt)  //prints the choice array and marks the currently selected option
        {
            System.Console.WriteLine(prompt);
            for (int i = 0; i < choices.Length; i++)
            {
                if (i == currentlySelected)
                {
                    System.Console.WriteLine($"> {choices[i]}");
                }
                else
                {
                    System.Console.WriteLine($"{choices[i]}");
                }
            }
        }
    }
}
