using AH_LinkedInShowcase2.Models;
using AH_LinkedInShowcase2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH_LinkedInShowcase2.Controllers
{
    public class TestFunctions
    {

        //Tests a few of the program's functions - Line Wrapping
        public static void Test_1()
        {
            Guidelines.Spacer(0, false);
            List<string> test = Guidelines.LineWrapper("The quick red fox jumped over the lazy brown dog and then kept typing this ridiculous sentence. Unfortunately, without adding ANOTHER sentence, this was not at the character threshold... so here are a few more words!", 0, 1, true);
            foreach (var line in test)
            {
                Console.WriteLine(line);
            }
            Guidelines.Spacer(0, true);
            Console.ReadLine();
        }

        //Tests a few of the program's functions - Selection Screens
        public static Game Test_2(Game game)
        {
            int used = Select_Screen.Make(game);
            game.player.CycleCrew(used);
            return game;
        }

        //Tests a few of the program's functions - Creating a game's player and that player's crew
        public static Game Test_3(Game game)
        {
            game.player = new Player();
            for (var i = 0; i < Guidelines.TotalCrewSize(); i++)
            {
                if (i < Guidelines.TotalActiveCrew())
                {
                    game.player.AddCrew(true);
                }
                else
                {
                    game.player.AddCrew(false);
                }
            }
            return game;
        }

        //Tests a few of the program's functions - Determines a visibility of ~29 lines by standard console size
        public static void Test_4(Game game)
        {
            for (var i = 0; i < 29; i++)
            {
                Console.WriteLine(i);
            }
        }

        //Tests a few of the program's functions - Tallies in certain lists and groups
        public static void Test_5(Game game)
        {
            Guidelines.Reset();
            Console.WriteLine("Available Names Count: " + Crew.NamesList.Count);
            Console.ReadLine();
            Guidelines.Reset();
        }

        //Tests a few of the program's functions - Produces an info window
        public static void Test_6(Game game)
        {
            string info = "The quick red fox jumped over the lazy brown dog and then kept typing this ridiculous sentence. Unfortunately, without adding ANOTHER sentence, this was not at the character threshold... so here are a few more words!";
            info += " I also found it appropriate to add the following line, since it will really test the limits of the functions I am adding and modifying on 6/15/2023!";
            string title = "Testing Window";
            Info_Screen.Make(title, info);
        }
    }
}
