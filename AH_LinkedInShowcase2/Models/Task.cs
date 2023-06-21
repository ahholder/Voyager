using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH_LinkedInShowcase2.Models
{
    public class Task
    {
        public int ID { get; set; } = 0;
        //public int[] Stats = { 0, 0, 0, 0 }; //May be changed for different usage
        public int[] Resc = { -1, -1, -1, -1, -1, -1, -1 }; //May be changed for different usage
        public string Title { get; set; } = " ";
        public string Flavor { get; set; } = " ";
        public bool Standard { get; set; } = true;

        //Provides the result for a completed task
        public Game Reward(Game game, int index)
        {
            Player ship = game.player;
            Crew officer = game.player.crew[index];
            ship.ImpactResource(ResourceID(), StatID());
            return game;
        }

        //Reads the potential value for a task
        public string Boost()
        {
            string info = "";
            /*for (var i = 0; i < Guidelines.ShipRescCount(); i++)
            {
                if (UsesStat(i) == true) info = $"{Guidelines.RescGainName(i)} {Guidelines.RescName(i).ToUpper()} equal to {Guidelines.StatName(ResourceID()).ToUpper()}.";
            }*/
            info = $"{Guidelines.RescGainName(ResourceID())} {Guidelines.RescName(ResourceID()).ToUpper()} equal to {Guidelines.StatName(StatID()).ToUpper()}";
            if (ResourceID() == Guidelines.ShipRescCount()) info += $" x {Guidelines.ProgressMultiplier()}";
            info += ".";
            return info;
        }

        //Returns the id of the resource adjusted
        public int ResourceID()
        {
            int result = -1;
            for (var i = 0; i < Guidelines.ShipRescCount(); i++)
            {
                //if (UsesStat(i) == true) result = i;
                if (Resc[i] > -1 && Resc[i] < Guidelines.CrewStatCount()) result = i;
                if (result == -1) result = Guidelines.ShipRescCount();
            }
            return result;
        }

        //Returns the id of the crewman stat used
        public int StatID()
        {
            int result = -1;
            for (var i = 0; i < Guidelines.ShipRescCount() + 1; i++) if (Resc[i] > -1 && Resc[i] < Guidelines.CrewStatCount() + 1) result = Resc[i];
            return result;
        }

        //Reads the potential value for a task -- replaced by Boost
        public string BoostOld(int resc)
        {
            string info = "";
            if (Resc[resc] > -1) info = $"{Guidelines.RescGainName(resc)} {Guidelines.RescName(resc).ToUpper()} using {Guidelines.StatName(Resc[resc]).ToUpper()}.";
            return info;
        }

        //Makes a list of all tasks; adjustable to specific stat pool
        public static List<Task> MakeAllTasks(int pool)
        {
            List<Task> tasks = new List<Task>();
            for (var i = 0; i < Guidelines.TaskPoolMax(); i++)
            {
                Task task = new Task();
                task.ID = i;
                task.Title = TaskInfo(i, true);
                task.Flavor = TaskInfo(i, false);
                task.TaskResc();
                if (pool > -1 && pool < Guidelines.CrewStatCount())
                {
                    for (var i2 = 0; i2 < Guidelines.CrewStatCount(); i2++)
                    {
                        if (pool == i2 && task.UsesStat(i2) == true) tasks.Add(task);
                    }
                } else
                {
                    tasks.Add(task);
                }
            }
            return tasks;
        }

        //Determines if a task uses a stat
        public bool UsesStat(int statChecked)
        {
            for (var i = 0; i < Guidelines.ShipRescCount() + 1; i++) if (Resc[i] == statChecked) return true;
            return false;
        }

        //Returns the string portions of each task
        public static string TaskInfo(int id, bool title)
        {
            string result = " ";
            //Returns the title
            //Returns the flavor text
            if (title == false)
            {
                if (id == 0) result = "Manually grind medicinal herbs";
                if (id == 1) result = "Barter with a passing vessel for medicine";
                if (id == 2) result = "Forage for medical plants on a primitive world";
                if (id == 3) result = "Relocate and assemble medical equipment";
                if (id == 4) result = "Siphon fuel from a distracted, hostile vessel";
                if (id == 5) result = "Barter with a passing vessel for power cells";
                if (id == 6) result = "Redesign fuel intake to increase efficiency";
                if (id == 7) result = "Manually load the reactor's power cells";
                if (id == 8) result = "Take precise shots against a hostile vessel";
                if (id == 9) result = "Negotiate a cease-fire with a hostile vessel";
                if (id == 10) result = "Improve your weapons' auto-targeting systems";
                if (id == 11) result = "Wield heavy weapons from your ship's arsenal";
                if (id == 12) result = "Manually plug small holes across your hull";
                if (id == 13) result = "Convince a friendly vessel to aid in repairs";
                if (id == 14) result = "Increase shielding to vulnerable parts of the hull";
                if (id == 15) result = "Weld heavy metal sheets to hull breaches";
                if (id == 16) result = "Sneak small gifts into the crews' quarters";
                if (id == 17) result = "Make a passionate, motivating speech";
                if (id == 18) result = "Organize leisure events for the crew";
                if (id == 19) result = "Cover the shifts for injured crewmen";
                if (id == 20) result = "Steal rations from a distracted, hostile vessel";
                if (id == 21) result = "Barter with a passing vessel for fresh water";
                if (id == 22) result = "Forage for food and water on a primitive world";
                if (id == 23) result = "Hunt for fresh meat on a primitive world";
                if (id == 24) result = "Steal replacement parts from distracted, hostile vessels";
                if (id == 25) result = "Negotiate a place to live with the system's native cultures";
                if (id == 26) result = "Chart a route to your ultimate destination";
                if (id == 27) result = "Act as a peacekeeper and protector";
            }
            return result;
        }

        //Returns the modified resource(s) for each task
        public void TaskResc()
        {
            int id = ID;
            int set = 0;
            for (var i = 0; i < Guidelines.CrewStatCount() + 1; i++) Resc[i] = -1;
            //Unique assignments
            if (id == 0) Resc[set] = 0;
            if (id == 1) Resc[set] = 1;
            if (id == 2) Resc[set] = 2;
            if (id == 3) Resc[set] = 3;
            set += 1;
            if (id == 4) Resc[set] = 0;
            if (id == 5) Resc[set] = 1;
            if (id == 6) Resc[set] = 2;
            if (id == 7) Resc[set] = 3;
            set += 1;
            if (id == 8) Resc[set] = 0;
            if (id == 9) Resc[set] = 1;
            if (id == 10) Resc[set] = 2;
            if (id == 11) Resc[set] = 3;
            set += 1;
            if (id == 12) Resc[set] = 0;
            if (id == 13) Resc[set] = 1;
            if (id == 14) Resc[set] = 2;
            if (id == 15) Resc[set] = 3;
            set += 1;
            if (id == 16) Resc[set] = 0;
            if (id == 17) Resc[set] = 1;
            if (id == 18) Resc[set] = 2;
            if (id == 19) Resc[set] = 3;
            set += 1;
            if (id == 20) Resc[set] = 0;
            if (id == 21) Resc[set] = 1;
            if (id == 22) Resc[set] = 2;
            if (id == 23) Resc[set] = 3;
            set += 1;
            if (id == 24) Resc[set] = 0;
            if (id == 25) Resc[set] = 1;
            if (id == 26) Resc[set] = 2;
            if (id == 27) Resc[set] = 3;
        }

    }
}
