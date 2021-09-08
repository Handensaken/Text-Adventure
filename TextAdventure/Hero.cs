using System;
using System.Collections.Generic;
namespace TextAdventure
{
    public class Hero
    {

        public int Health { get; protected set; }

        //skapa stats

        

        public List<string> Items = new List<string>(); //Can be replaced with a list of an Item class

        GameManager manager = new GameManager();    //Creates new Game manager
        
        public string Name { get; private set; }    //Creates Name property
        public Hero()   //Constructor for Hero
        {
            do
            {
                System.Console.WriteLine("Enter your name");
                Name = manager.TextFeeder();
            } while (1 == manager.Selection(new[] { "Yes", "No" }, $"is {Name} your name?"));
        }
    }
}
