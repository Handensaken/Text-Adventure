using System;

namespace TextAdventure
{
    public class HeroCreator
    {
        public Hero HeroSpawner(GameManager manager)    //Gives the user the option to choose class 
        {
            string[] heroChoices = { "Warrior", "Wizard" };
            switch (manager.Selection(heroChoices, "Select Class"))
            {
                case 0:
                    {
                        return new Warrior();
                    }
                case 1:
                    {
                        return new Wizard();
                    }
                default:
                    {
                        System.Console.WriteLine("woopsie doopsie you done fucked up");
                        return new Hero();
                    }
            }
        }
    }
}
