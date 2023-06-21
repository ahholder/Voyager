using AH_LinkedInShowcase2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH_LinkedInShowcase2.Models
{
    public class Boxes
    {
        //Creates a title window for a view
        public static void Title_Window(string title)
        {
            Guidelines.Spacer(0, false);
            title = Guidelines.Center(title, Guidelines.LineLength() - 6);
            title = Guidelines.Frame(title,0);
            Console.Write(title);
            Guidelines.Spacer(0, true);
        }

        //Creates an info window line for a crewman's stats
        public static string Crewman_Window(int index, int line, Game game)
        {
            Crew crew = game.player.crew[index];
            string info = "";
            if (line == 0)
            {
                info += $"{crew.Name}'s Stats";
                info = Guidelines.Center(info, Guidelines.CrewWidth() - 1);
            }
            else if (line == 1 || line == Guidelines.CrewWindowSize() - 1)
            {
                info = Guidelines.Center(" ", Guidelines.CrewWidth() - 1);
            }
            else if (line == Guidelines.CrewWindowSize())
            {
                info = Guidelines.Closer("-", Guidelines.CrewWidth() + 1);
            } else if (line > Guidelines.CrewWindowSize())
            {
                info = " ";
            } else
            {
                info = $" {Guidelines.StatName(line - 2)}:  {crew.Stats[line - 2]}";
            }
            //Final
            if (line < Guidelines.CrewWindowSize()) return (Guidelines.Frame(info, Guidelines.CrewWidth()));
            return info;
        }

        //Creates an info window line for a ship's stats
        public static string Ship_Window(int line, Game game)
        {
            Player ship = game.player;
            string info = "";
            if (line == 0)
            {
                info += "Ship's Stats";
                info = Guidelines.Center(info, Guidelines.CrewWidth() - 1);
            }
            else if (line == 1 || line == Guidelines.ShipWindowSize() - 1)
            {
                info = Guidelines.Center(" ", Guidelines.CrewWidth() - 1);
            }
            else if (line == Guidelines.ShipWindowSize())
            {
                info = Guidelines.Closer("-", Guidelines.CrewWidth() + 1);
            }
            else if (line > Guidelines.ShipWindowSize())
            {
                info = " ";
            }
            else
            {
                info = $" {Guidelines.RescName(line - 2)}:   {Guidelines.LineUp(line - 2)}{ship.Resc[line - 2]} / {ship.MaxResc[line = 2]}";
            }
            //Final
            if (line < Guidelines.ShipWindowSize()) return (Guidelines.Frame(info, Guidelines.CrewWidth()));
            return info;
        }

        //Creates an info window line for the player's progress
        public static string Progress_Window(int line, Game game)
        {
            Player ship = game.player;
            string info = "";
            if (line == 0)
            {
                info += "Progress %";
                info = Guidelines.Center(info, Guidelines.CrewWidth() - 1);
            }
            //else if (line == 1 || line == Guidelines.ProgressWindowSize() - 1)
            else if (line == Guidelines.ProgressWindowSize() - 1)
                    {
                info = Guidelines.Center(" ", Guidelines.CrewWidth() - 1);
            }
            else if (line == Guidelines.ProgressWindowSize())
            {
                info = Guidelines.Closer("-", Guidelines.CrewWidth() + 2);
            }
            else if (line > Guidelines.ProgressWindowSize())
            {
                info = " ";
            }
            else
            {
                info = Guidelines.Center($"{ship.Progress} / {Guidelines.VictoryProgress()}", Guidelines.CrewWidth() - 1);
            }
            //Final
            if (line < Guidelines.ProgressWindowSize()) return (Guidelines.Frame(info, Guidelines.CrewWidth()));
            return info;
        }

        //Creates an info window line for the current task
        public static string Task_Window(int line, Game game)
        {
            Player ship = game.player;
            string info = "";
            Event complication = ship.CurrentEvent;
            int cycle = ship.Cycle;
            if (line == 0)
            {
                info += "Task:";
                info = Guidelines.Center(info, Guidelines.InfoWidth() + 1);
            }
            else if (line == 1 || line == Guidelines.InfoBoxesSize() - 1)
            {
                info = Guidelines.Center(" ", Guidelines.InfoWidth());
            }
            else if (line == Guidelines.InfoBoxesSize())
            {
                info = Guidelines.Closer("-", Guidelines.InfoWidth() + 1);
            }
            else if (line == 2)
            {
                info = Guidelines.Center(ship.CurrentTask.Flavor + ".", Guidelines.InfoWidth());
            }
            else if (line == 4)
            {
                info = Guidelines.Center(ship.CurrentTask.Boost(), Guidelines.InfoWidth());
            }
            else if (line == 6)
            {
                info = Guidelines.Center(ship.CurrentEvent.Special, Guidelines.InfoWidth());
            }
            else if (line == 8)
            {
                info = Guidelines.Center("Upcoming Event:", Guidelines.InfoWidth());
            }
            else if (line == 10)
            {
                info = Guidelines.Center($"{complication.ImpactText(0,cycle, true)}", Guidelines.InfoWidth());
            }
            else if (line == 12)
            {
                info = Guidelines.Center($"{complication.ImpactText(1, cycle, true)}", Guidelines.InfoWidth());
            }
            else if (line > Guidelines.InfoBoxesSize())
            {
                info = " ";
            }
            else
            {
                info = " ";
            }
            if (line != Guidelines.InfoBoxesSize() && line != 0) info = Guidelines.Center(info, Guidelines.InfoWidth()) + " ";
            if (line != Guidelines.InfoBoxesSize()) info = Guidelines.Center(info,Guidelines.InfoWidth()) + "|";
            //Final
            //if (line < Guidelines.ProgressWindowSize()) return (Guidelines.Frame(info, Guidelines.CrewWidth()));
            return info;
        }
    }
}


/*info.Add(Guidelines.Frame(" ", Guidelines.LineLength()));
info.Add(Guidelines.Frame(Guidelines.Center(game.player.CurrentEvent.ImpactText(i, game.player.Cycle, true), Guidelines.LineLength() - 2), Guidelines.LineLength()));*/