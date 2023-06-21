using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH_LinkedInShowcase2.Models
{
    public class Player
    {
        public bool GameOver { get; set; } = false;
        public int Cycle { get; set; } = 0;
        public List<Crew> crew = new List<Crew>();
        public List<Crew> reserve = new List<Crew>();
        public int[] Resc = { Guidelines.StartResc(), Guidelines.StartResc(), Guidelines.StartResc(), Guidelines.StartResc(), Guidelines.StartResc(), Guidelines.StartResc() };
        public int[] MaxResc = { 50, 50, 50, 50, 50, 50 };
        public int Progress { get; set; } = 0;
        public Event CurrentEvent = new Event();
        public Task CurrentTask = new Task();
        public List<Event> PendingEvents = new List<Event>();
        public List<Task> PendingTasks = new List<Task>();
        public List<Task> AgiTasks = new List<Task>();
        public List<Task> ChaTasks = new List<Task>();
        public List<Task> IntTasks = new List<Task>();
        public List<Task> StrTasks = new List<Task>();

        //Determines if the game was won or lost
        public bool DetermineVictory()
        {
            bool victory = true;
            for (var i = 0; i < Guidelines.ShipRescCount(); i++) if (Resc[i] <= Guidelines.DefeatThreshold()) victory = false;
            return victory;
        }

        //Assign Current Task
        public void AssignCurrentTask()
        {
            if (PendingTasks.Count <= 0) QueueTasks();
            var r = new Random(Guidelines.RNG());
            var rng = r.Next(0, PendingTasks.Count);
            Task pending = PendingTasks[rng];
            CurrentTask = pending;
            PendingTasks.Remove(pending);
        }

        //Check to ensure each task pool is stocked; restocks if empty
        public void CheckTaskPools()
        {
            if (AgiTasks.Count < 1) AgiTasks = Task.MakeAllTasks(0);
            if (ChaTasks.Count < 1) ChaTasks = Task.MakeAllTasks(1);
            if (IntTasks.Count < 1) IntTasks = Task.MakeAllTasks(2);
            if (StrTasks.Count < 1) StrTasks = Task.MakeAllTasks(3);
        }

        //Adds tasks to the list of available
        public void QueueTasks()
        {
            var r = new Random(Guidelines.RNG());
            int rng = 0;
            Task pending;
            for (var i = 0; i < Guidelines.EventCycles() / Guidelines.CrewStatCount(); i++)
            {
                //Checks and updates individual task pools
                CheckTaskPools();
                //AGI tasks
                rng = r.Next(0, AgiTasks.Count);
                pending = AgiTasks[rng];
                PendingTasks.Add(pending);
                AgiTasks.Remove(pending);
                //CHA tasks
                rng = r.Next(0, ChaTasks.Count);
                pending = ChaTasks[rng];
                PendingTasks.Add(pending);
                ChaTasks.Remove(pending);
                //INT tasks
                rng = r.Next(0, IntTasks.Count);
                pending = IntTasks[rng];
                PendingTasks.Add(pending);
                IntTasks.Remove(pending);
                //STR tasks
                rng = r.Next(0, StrTasks.Count);
                pending = StrTasks[rng];
                PendingTasks.Add(pending);
                StrTasks.Remove(pending);
            }
        }

        //Resolves an impact on progress
        public string ImpactProgress(int value)
        {
            value *= Guidelines.ProgressMultiplier();
            string result = "";
            if (value <= 0)
            {
                if (value > Progress) value = Progress * - 1;
                result = $"You lost {value * -1}% of your PROGRESS!";
                Progress += value;
            }
            else
            {
                if (value + Progress > Guidelines.VictoryProgress()) value = Guidelines.VictoryProgress() - Progress;
                result = $"You gain {value}% PROGRESS towards your goal!";
                Progress += value;
            }
            return result;
        }

        //Resolves an impact on a resource
        public string ImpactResource(int resc, int value)
        {
            string result = "";
            if (value <= 0)
            {
                if (value > Resc[resc]) value = Resc[resc];
                result = $"Your {(Guidelines.RescName(resc)).ToUpper()} lost {value * -1} {Guidelines.RescHitName(resc)}!";
                Resc[resc] += value;
            } else
            {
                if (value + Resc[resc] > MaxResc[resc]) value = MaxResc[resc] - Resc[resc];
                result = $"Your {(Guidelines.RescName(resc)).ToUpper()} gains {value} {Guidelines.RescHitName(resc)}!";
                Resc[resc] += value;
            }
            return result;
        }

        //Checks if the player's game is over
        public void CheckGameOver()
        {
            if (Progress >= Guidelines.VictoryProgress()) GameOver = true;
            for (var i = 0; i < Guidelines.ShipRescCount(); i++)
            {
                if (Resc[i] <= Guidelines.DefeatThreshold()) GameOver = true;
            }
        }

        //Creates a pool of events
        public void MakeEventPool()
        {
            for (var i = 0; i < Guidelines.EventPoolMax(); i++)
            {
                if (i == 0 && Cycle == 0) CurrentEvent = Event.Make(i);
                if (i != 0) PendingEvents.Add(Event.Make(i));
            }
        }

        //Creates a new member of the crew
        public Crew MakeCrew()
        {
            Crew crew = new Crew();
            crew.MakeName();
            crew.MakeStats();
            return crew;
        }
        //Adds a crewman to the active or reserve list
        public void AddCrew(bool active)
        {
            if (active == true) crew.Add(MakeCrew());
            if (active == false) reserve.Add(MakeCrew());
        }

        //Moves the acting crewman to reserve and the next reserve to active
        public void CycleCrew(int index)
        {
            if (reserve.Count > 0)
            {
                Crew used = crew[index];
                crew[index] = reserve[0];
                for (var i = 0; i < reserve.Count; i++)
                {
                    if (i < reserve.Count - 1)
                    {
                        reserve[i] = reserve[i + 1];
                    }
                    else
                    {
                        reserve[i] = used;
                    }
                }
            }
        }
    }
}
