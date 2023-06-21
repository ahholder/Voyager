using AH_LinkedInShowcase2.Controllers;
using AH_LinkedInShowcase2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH_LinkedInShowcase2.Views
{
    public class Select_Screen
    {
        //Creates the Selection View
        public static int Make(Game game)
        {
            int index = 0;
            int choice = -1;
            bool loops = true;
            int choices = Guidelines.TotalActiveCrew();
            while (loops != false)
            {
                Guidelines.Reset();
                Boxes.Title_Window("Task Selection");
                Choices(index, choices, game);
                Info(index, game);
                choice = Selection.Option(choices, index);
                if (choice != -100)
                {
                    index = choice;
                } else
                {
                    loops = false;
                }
            }
            return index;
        }

        //Creates the Selection Window
        private static void Choices(int index, int total, Game game)
        {
            /*Console.WriteLine(Guidelines.Frame(Guidelines.Center("  ", Guidelines.LineLength() - 4), 0));
            Console.WriteLine(Guidelines.Frame(Guidelines.Center("  ", Guidelines.LineLength() - 4), 0));
            Console.WriteLine(Guidelines.Frame(Guidelines.Center("  ", Guidelines.LineLength() - 4), 0));*/

            //Establish spacing & guidelines
            int max = Guidelines.LineLength() - 6;
            int empty = max - (Guidelines.BoxSize() * total);
            int buffer = empty / (total);
            int sides = (empty % (total)) / 2;
            if (total > 1) buffer = empty / (total - 1);
            if (total > 1) sides = (empty % (total - 1)) / 2;
            int adjustment = 0;
            if (index > 0) adjustment = ((buffer * index) - index) + 0;

            //Gather List of names and assign spacing
            string[] lines = new string[Guidelines.CrewStatCount()];
            List<string> names = new List<string>();
            foreach (var member in game.player.crew)
            {
                names.Add(Guidelines.Center(member.Name, Guidelines.BoxSize() - 4));
            }
            for (var i = 0; i < total; i++)
            {
                if (index == i)
                {
                    names[i] = "| " + names[i] + " |";
                } else
                {
                    names[i] = "  " + names[i] + "  ";
                }
            }

            //Design Lines
            for (var l = 0; l < Guidelines.CrewStatCount(); l++)
            {
                lines[l] = "";
                if (l == 1)
                {
                    for (var i = 0; i < total; i++)
                    {
                        lines[l] += names[i];
                        if (i != total - 1)
                        {
                            for (var i2 = 0; i2 < buffer; i2++) lines[l] += " ";
                        }
                    }
                    for (var i2 = 0; i2 < sides; i2++)
                    {
                        lines[l] += " ";
                        lines[l] = " " + lines[l];
                    }
                } else
                {
                    for (var i2 = 0; i2 < sides + adjustment; i2++) lines[l] += " ";
                    for (var i = 0; i < total; i++)
                    {
                        if (index == i)
                        {
                            for (var i2 = 0; i2 < Guidelines.BoxSize() ; i2++) lines[l] += "-";
                        } else
                        {
                            for (var i2 = 0; i2 < Guidelines.BoxSize() - 1; i2++) lines[l] += " ";
                        }
                        if (i != total - 1)
                        {
                            for (var i2 = 0; i2 < buffer; i2++) lines[l] += " ";
                        }
                    }
                    for (var i2 = 0; i2 < sides; i2++) lines[l] = lines[l] + " ";
                }
                lines[l] = Guidelines.Frame(lines[l], Guidelines.LineLength());
            }

            for (var i = 0; i < 3; i++) Console.WriteLine(lines[i]);
            Guidelines.Spacer(0, false);
        }

        //Creates the Lower Windows
        private static void Info(int index, Game game)
        {
            string line = "";
            for (var i = 0; i < Guidelines.InfoLines(); i++)
            {
                line = "";
                //Crewman Info Window
                if (i < Guidelines.CrewWindowSize() + 1)
                {
                    line += Boxes.Crewman_Window(index, i, game);
                } else if (i < Guidelines.CrewWindowSize() + 2 + Guidelines.ShipWindowSize())
                {
                    line += Boxes.Ship_Window(i - (Guidelines.CrewWindowSize() + 1), game);
                } else
                {
                    line += Boxes.Progress_Window(i - (Guidelines.CrewWindowSize() + 2 + Guidelines.ShipWindowSize()), game);
                }
                line += Boxes.Task_Window(i, game);
                //Final
                Console.WriteLine(line);
            }
        }

    }
}
