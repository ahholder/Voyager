using AH_LinkedInShowcase2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH_LinkedInShowcase2.Models
{
    public class Event
    {
        public int ID { get; set; } = 0;
        public string Title { get; set; } = " ";
        public string Flavor { get; set; } = " ";
        public string Special { get; set; } = " ";
        public int[] Impact = { 0, 0, 0, 0, 0, 0 };

        //Determines the first stat to be impacted -- Currently Unused
        public int FirstImpact()
        {
            var impact = -1;
            for (var i = 0; i < Guidelines.ShipRescCount(); i++)
            {
                if (impact == -1 && Impact[i] != 0) impact = i;
            }
            return impact;
        }

        //Determines the second stat to be impacted -- Currently Unused
        public int SecondImpact()
        {
            var impact = -1;
            for (var i = 0; i < Guidelines.ShipRescCount(); i++)
            {
                if (impact == -1 && Impact[i] != 0 && i != FirstImpact()) impact = i;
            }
            return impact;
        }

        //Announces an upcoming event
        public static Game Announce(Game game)
        {
            List<string> info = new List<string>();
            Event current = game.player.CurrentEvent;
            string title = "Cycle # " + game.player.Cycle + ": EVENT - " + current.Title;
            info.Add(Guidelines.Frame(" ", Guidelines.LineLength()));
            info.Add(Guidelines.Frame(Guidelines.Center(current.Flavor, Guidelines.LineLength() - 2), Guidelines.LineLength()));
            info.Add(Guidelines.Frame(" ", Guidelines.LineLength()));
            info.Add(Guidelines.Frame(Guidelines.Center(current.Special, Guidelines.LineLength() - 2), Guidelines.LineLength()));
            info.Add(Guidelines.Frame(" ", Guidelines.LineLength()));
            info.Add(Guidelines.Frame(" ", Guidelines.LineLength()));
            for (var i = 0; i < current.TotalImpacts(); i++)
            {
                 info.Add(Guidelines.Frame(" ", Guidelines.LineLength()));
                 info.Add(Guidelines.Frame(Guidelines.Center(game.player.CurrentEvent.ImpactText(i, game.player.Cycle, true), Guidelines.LineLength() - 2), Guidelines.LineLength()));
            }
            Info_Screen.MakeLined(title, info);
            return game;
        }

        //Resolves the impacts of an event
        public static Game Resolve(Game game)
        {
            List<string> info = new List<string>();
            Event current = game.player.CurrentEvent;
            string title = "Cycle # " + game.player.Cycle + ": EVENT - " + current.Title;
            info.Add(Guidelines.Frame(" ", Guidelines.LineLength()));
            info.Add(Guidelines.Frame(Guidelines.Center(current.Flavor, Guidelines.LineLength() - 2), Guidelines.LineLength()));
            for (var i = 0; i < Guidelines.ShipRescCount(); i++)
            {
                //info.Add(current.ImpactText(tally,game.player.Cycle,false));
                if (current.Impact[i] != 0)
                {
                    info.Add(Guidelines.Frame(" ", Guidelines.LineLength()));
                    info.Add(Guidelines.Frame(Guidelines.Center(game.player.ImpactResource(i, current.Impact[i]), Guidelines.LineLength() - 2), Guidelines.LineLength()));
                }
            }
            Info_Screen.MakeLined(title, info);
            return game;
        }

        //Returns the title of the event
        public string EventTitle(int cycle)
        {
            string title = $"CYCLE #{cycle} - {Title}";
            return title;
        }

        //Returns the number of [ship] resources impacted
        public int TotalImpacts()
        {
            int total = 0;
            for (var i = 0; i < Guidelines.ShipRescCount(); i++)
            {
                if (Impact[i] != 0) total += 1;
            }
            return total;
        }

        //Returns a readout of the event's impacts
        public string ImpactText(int line, int cycle, bool pending)
        {
            int future = cycle + Guidelines.EventCycles();
            string[] results = new string[TotalImpacts()];
            int tally = 0;
            for (var i = 0; i < Guidelines.ShipRescCount(); i++)
            {
                if (Impact[i] != 0)
                {
                    if (pending == true) results[tally] = $"Your {(Guidelines.RescName(i)).ToUpper()} will lose {Impact[i] * -1} {Guidelines.RescHitName(i)} at the start of Cycle #{future}!";
                    if (pending == false) results[tally] = $"Your {(Guidelines.RescName(i)).ToUpper()} has lost {Impact[i] * -1} {Guidelines.RescHitName(i)}!";
                    tally += 1;
                }
            }
            return results[line];
        }

        //Makes an event based on preset values for each ID
        public static Event Make(int id)
        {
            Event result = new Event();
            result.ID = id;
            string title = " ";
            string flavor = " ";
            string special = " ";
            int[] impact = { 0, 0, 0, 0, 0, 0 };
            switch (id)
            {
                case 0:
                    title = " ";
                    flavor = " ";
                    special = " ";
                    impact[0] = 0;
                    impact[0] = 0;
                    break;
                case 1:
                    title = "Radiation Leak";
                    flavor = "Your ship's fusion reactor is leaking into crew quarters!";
                    special = "";
                    impact[0] = -1;
                    impact[1] = -1;
                    break;
                case 2:
                    title = "Munitions Explosion";
                    flavor = "Several rounds detonate in the hold, injuring your crew!";
                    special = "";
                    impact[0] = -1;
                    impact[2] = -1;
                    break;
                case 3:
                    title = "Depressurization";
                    flavor = "Space debris tears through your hull!";
                    special = "";
                    impact[0] = -1;
                    impact[3] = -1;
                    break;
                case 4:
                    title = "Alien Virus";
                    flavor = "A new disease is making your crew miserable!";
                    special = "";
                    impact[0] = -1;
                    impact[4] = -1;
                    break;
                case 5:
                    title = "Spoiled Rations";
                    flavor = "You discover many of your food stocks were improperly stored!";
                    special = "";
                    impact[0] = -1;
                    impact[5] = -1;
                    break;
                case 6:
                    title = "Corrosive Overflow";
                    flavor = "A fuel leak has damages wiring around your lasers!";
                    special = "";
                    impact[1] = -1;
                    impact[2] = -1;
                    break;
                case 7:
                    title = "Space Pirates";
                    flavor = "Shots from a hostile ship have damage your hull and fuel tanks!";
                    special = "";
                    impact[1] = -1;
                    impact[3] = -1;
                    break;
                case 8:
                    title = "Transport Mishap";
                    flavor = "Operator error causes a major fuel spill!";
                    special = "";
                    impact[1] = -1;
                    impact[4] = -1;
                    break;
                case 9:
                    title = "Range Miscalculation";
                    flavor = "Much of your water has to be converted to fuel after a routing miscalculation!";
                    special = "";
                    impact[1] = -1;
                    impact[5] = -1;
                    break;
                case 10:
                    title = "Return Fire";
                    flavor = "You are forced to battle with a hostile vessel!";
                    special = "";
                    impact[2] = -1;
                    impact[3] = -1;
                    break;
                case 11:
                    title = "Mutiny";
                    flavor = "A mutiny causes shots to be exchanged among your crew!";
                    special = "";
                    impact[2] = -1;
                    impact[4] = -1;
                    break;
                case 12:
                    title = "Rat Infestation";
                    flavor = "Rifles are used to purge stowaway rats from your food stock!";
                    special = "";
                    impact[2] = -1;
                    impact[5] = -1;
                    break;
                case 13:
                    title = "Emergency Containment";
                    flavor = "A tear in the hull isolates your crew and confines them to their quarters!";
                    special = "";
                    impact[3] = -1;
                    impact[4] = -1;
                    break;
                case 14:
                    title = "Space Raiders";
                    flavor = "A hostile vessel's crew invades your cargo hold and steals your rations!";
                    special = "";
                    impact[3] = -1;
                    impact[5] = -1;
                    break;
                case 15:
                    title = "Rationing";
                    flavor = "You realize rations are low and enforce mandatory rationing!";
                    special = "";
                    impact[4] = -1;
                    impact[5] = -1;
                    break;
                default:
                    title = " ";
                    flavor = " ";
                    special = " ";
                    break;
            }
            //Final
            for (var i = 0; i < Guidelines.ShipRescCount(); i++)
            {
                if (impact[i] == -1)
                {
                    result.Impact[i] = Guidelines.Penalty() * -1;
                } else if (impact[i] > 0)
                {
                    result.Impact[i] = impact[i] * -1;
                }
            }
            result.Title = title;
            result.Flavor = flavor;
            result.Special = special;
            result.Special = " "; //Placeholder, in case special property is not used
            return result;
        }
    }
}
