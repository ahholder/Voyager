using AH_LinkedInShowcase2.Controllers;
using AH_LinkedInShowcase2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH_LinkedInShowcase2.Views
{
    public class Info_Screen
    {
        //Creates the information view with line-by-line information
        public static void MakeLined(string title, List<string> info)
        {
            Guidelines.Reset();
            Boxes.Title_Window(title);
            foreach (var line in info)
            {
                var read = line;
                if (line[0] != '|') read = Guidelines.Frame(Guidelines.Center(line, Guidelines.LineLength() -2), Guidelines.LineLength());
                Console.WriteLine(Guidelines.Center(read, Guidelines.LineLength() - 2));
            }
            Console.WriteLine(Guidelines.Frame(" ", Guidelines.LineLength()));
            Console.WriteLine(Guidelines.Frame(Guidelines.Continuer(Guidelines.LineLength() - 1), Guidelines.LineLength()));
            Guidelines.Spacer(Guidelines.LineLength() - 2, false);
            Selection.Continuer();
        }

        //Creates the information view
        public static void Make(string title, string info)
        {
            Guidelines.Reset();
            Boxes.Title_Window(title);
            List<string> lines = Guidelines.LineWrapper(info, Guidelines.LineLength() - 2, 1, true);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine(Guidelines.Frame(" ",Guidelines.LineLength()));
            Console.WriteLine(Guidelines.Frame(Guidelines.Continuer(Guidelines.LineLength() - 1), Guidelines.LineLength()));
            Guidelines.Spacer(Guidelines.LineLength() - 2, false);
            Selection.Continuer();
        }

        //Returns the text used for the Tutorial-Intro
        public static string Intro()
        {
            string intro = "In the distant future, a small group of humans have left Earth to start a colony on a distant world. ";
            intro += "You are an advanced A.I. responsible for ensuring a safe arrival to their new home. ";
            intro += "The crew's most capable officers will work in rotations leaving and exiting stasis. ";
            intro += "As responsibilities appear and events occur, you can delegate a task to a standby officer. ";
            //intro += "";
            return intro;
        }
        public static string Intro2()
        {
            string intro = $"You win the game by reaching {Guidelines.VictoryProgress()}% PROGRESS towards your goal.  ";
            intro += $"You lose the game by reaching {Guidelines.DefeatThreshold()} ";
            for (var i = 0; i < Guidelines.ShipRescCount(); i++)
            {
                if (i != 0) intro += ", ";
                if (i == Guidelines.ShipRescCount() - 1) intro += "or ";
                intro += Guidelines.RescName(i).ToUpper();
            }
            intro += " at any point in your journey. ";
            intro += "Each officer will use ";
            for (var i = 0; i < Guidelines.CrewStatCount(); i++)
            {
                if (i != 0) intro += ", ";
                if (i == Guidelines.CrewStatCount() - 1) intro += "and ";
                intro += Guidelines.StatName(i).ToUpper();
            }
            intro += " to complete tasks. ";
            intro += $"Completing a task increases the officer's stat used in that task. ";
            intro += $"Once an officer completes a task, they return to the reserve for your {Guidelines.TotalCrewSize()}-person crew. ";
            intro += $"Every {Guidelines.EventCycles()} CYCLES, a new event will appear to impact your vessel. ";
            //intro += "";
            return intro;
        }

        //Returns a Victory or Defeat Message
        public static string Victory(Game game)
        {
            string intro = $"After {game.player.Cycle} cycles, you have made it to a new world to call your own! ";
            intro += "Your crew has survived to see a new era in human history. ";
            //intro += "";
            return intro;
        }
        public static string Defeat(Game game)
        {
            int tally = 0;
            string intro = $"";
            //Determine the number of defeat flags
            for (var i = 0; i < Guidelines.ShipRescCount(); i++)
            {
                if (game.player.Resc[i] <= Guidelines.DefeatThreshold())
                {
                    tally += 1;
                }
            }
            //Provide printout based on number of defeat flags
            if (tally == 1)
            {
                for (var i = 0; i < Guidelines.ShipRescCount(); i++)
                {
                    if (game.player.Resc[i] <= Guidelines.DefeatThreshold())
                    {
                        intro = $"{Guidelines.RescName(i).ToLower()}";
                        if (Guidelines.RescName(i) == "GUN") intro += "s";
                    }
                }
            } else if (tally == 2)
            {
                tally = 0;
                for (var i = 0; i < Guidelines.ShipRescCount(); i++)
                {
                    if (game.player.Resc[i] <= Guidelines.DefeatThreshold())
                    {
                        tally += 1;
                        if (tally == 1) intro = $"{Guidelines.RescName(i).ToLower()}";
                        if (tally == 2) intro = $"{intro} and {Guidelines.RescName(i).ToLower()}";
                        if (Guidelines.RescName(i) == "GUN") intro += "s";
                    }
                }
            }
            else
            {
                int threshold = tally;
                tally = 0;
                for (var i = 0; i < Guidelines.ShipRescCount(); i++)
                {
                    if (game.player.Resc[i] <= Guidelines.DefeatThreshold())
                    {
                        tally += 1;
                        if (tally == 1)
                        {
                            intro = $"{Guidelines.RescName(i).ToLower()}";
                        }
                        else if (tally == threshold)
                        {
                            intro += $", and {Guidelines.RescName(i).ToLower()}";
                        } else
                        {
                            intro += $", {Guidelines.RescName(i).ToLower()}";
                        }
                        if (Guidelines.RescName(i) == "GUN") intro += "s";
                    }
                }
            }
            intro = $"After {game.player.Cycle} cycles, running low on {intro}, your crew perishes within the void of space. ";
            intro += "What may be the last hope for humanity has been lost under your guidance... ";
            //intro += "";
            return intro;
        }

        //Creates a closing message
        public static List<string> ThanksForPlaying(Game game)
        {
            List<string> info = new List<string>();
            info.Add("Thanks for playing!");
            info.Add("This has been a C# Console Application project created by Alexander Holder.");
            info.Add(" ");
            info.Add(" ");
            info.Add("Think you've mastered this game?");
            info.Add("Dislike how things turned out?");
            info.Add("Try again to see if the next scenario ends the same way! ");
            info.Add(" ");
            info.Add(" ");
            info.Add("Additionally, you can check my GitHub link for future projects:");
            info.Add("https://github.com/ahholder?tab=repositories");
            return info;
        }
    }
}
