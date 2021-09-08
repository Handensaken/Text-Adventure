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
            return CurrentRoom;
            switch (CurrentRoom)
            {
                case Rooms.Start:
                    {
                        break;
                    }
                case Rooms.TableRoom:
                    {
                        break;
                    }
                case Rooms.Corridor:
                    {
                        break;
                    }
                case Rooms.LockedRoom:
                    {
                        break;
                    }
                case Rooms.ThirdRoom:
                    {
                        break;
                    }
                case Rooms.BackOutside:
                    {
                        break;
                    }
                case Rooms.BossRoom:
                    {
                        break;
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
            //call movement
        }







        private void CombatLoop()
        {
            Minotaur minotaur = new Minotaur();
            while (minotaur.Health > 0)
            {
                List<string> choices = new List<string>() { "Wooden sword", "Mighty kick", "Gallant headbutt" };
                if (hero.Items.Contains("Shiny sword")) { choices.Add("Shiny sword"); }
                else if (hero.Items.Contains("Knife")) { choices.Add("Stabby Knife"); }
            }
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
