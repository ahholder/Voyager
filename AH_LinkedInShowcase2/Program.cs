using System;
using System.Drawing;
using System.Threading;
using System.IO;
using AH_LinkedInShowcase2.Controllers;
using AH_LinkedInShowcase2.Models;
using AH_LinkedInShowcase2.Views;
//using AH_LinkedInShowcase2.Controllers;

namespace ConsoleAdventure
{
    class Program
    {

        //Start the program's functions
        static void Main(string[] args)
        {
            RunGame();
        }

        //Starts the game
        public static void RunGame()
        {
            //Create a new game and set default lists
            Game game = new Game();
            Crew.MakeNames();
            game.player = new Player();
            game.player.MakeEventPool();
            //game.player.QueueTasks();

            //Create crew and reserve
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

            //Start Tutorial-Introduction
            string title = "Voyager 1.0 Alpha";
            string info = Info_Screen.Intro();
            Info_Screen.Make(title, info);
            info = Info_Screen.Intro2();
            Info_Screen.Make(title, info);

            //Start Gameplay
            while (game.player.GameOver == false)
            {
                game.player.Cycle += 1;
                if (game.player.CurrentEvent.ID != 0 && (game.player.Cycle - 1 == 0 || (game.player.Cycle - 1) % Guidelines.EventCycles() == 0))
                {
                    game = Event.Resolve(game);
                    game.player.CheckGameOver();
                    if (game.player.GameOver == true) break;
                }
                if (game.player.Cycle - 1 == 0 || (game.player.Cycle - 1) % Guidelines.EventCycles() == 0)
                {
                    if (game.player.PendingEvents.Count <= 0)
                    {
                        game.player.MakeEventPool();
                        game.player.PendingEvents.Remove(game.player.CurrentEvent);
                    }
                    var r = new Random(Guidelines.RNG());
                    int rng = r.Next(0, game.player.PendingEvents.Count);
                    Event newEvent = game.player.PendingEvents[rng];
                    game.player.CurrentEvent = newEvent;
                    game.player.PendingEvents.Remove(newEvent);
                    Event.Announce(game);
                }

                game.player.AssignCurrentTask();
                var choice = Select_Screen.Make(game);
                Crew officer = game.player.crew[choice];
                List<string> readout = new List<string>();
                //title = $"CYCLE #{game.player.Cycle} - {game.player.CurrentTask.Flavor.ToUpper()}";
                title = $"CYCLE #{game.player.Cycle} -- TASK COMPLETE";
                readout.Add(" ");
                readout.Add($"{officer.Name} is sent to {game.player.CurrentTask.Flavor.ToLower()}.");
                readout.Add(" ");
                readout.Add(" ");
                readout.Add("Results:");
                readout.Add(" ");
                if (game.player.CurrentTask.Standard == true)
                {
                    if (game.player.CurrentTask.ResourceID() != Guidelines.ShipRescCount())
                    {
                        readout.Add(game.player.ImpactResource(game.player.CurrentTask.ResourceID(), officer.Stats[game.player.CurrentTask.StatID()]));
                    } else
                    {
                        readout.Add(game.player.ImpactProgress(officer.Stats[game.player.CurrentTask.StatID()]));
                    }
                }
                game.player.crew[choice].Stats[game.player.CurrentTask.StatID()] += 1;
                officer = game.player.crew[choice];
                readout.Add($"{officer.Name}'s {Guidelines.StatName(game.player.CurrentTask.StatID())} increases to {officer.Stats[game.player.CurrentTask.StatID()]}!");
                readout.Add(" ");
                Info_Screen.MakeLined(title, readout);
                game.player.CycleCrew(choice);
                game.player.CheckGameOver();
                //break;
            }
            if (game.player.DetermineVictory() == true)
            {
                title = "Victory!";
                info = Info_Screen.Victory(game);
            } else
            {
                title = "Defeat!";
                info = Info_Screen.Defeat(game);
            }
            Info_Screen.Make(title, info);
            List<string> info2 = Info_Screen.ThanksForPlaying(game);
            Info_Screen.MakeLined("Thanks for Playing!", info2);
            //Select_Screen.Make(game);
        }

        //Runs a specific series of test functions
        public static void Test(Game game)
        {
            //Test_5(game);
            //Test_1();
            TestFunctions.Test_6(game);
            game = TestFunctions.Test_3(game);
            for (var i = 0; i < 15; i++) game = TestFunctions.Test_2(game);
            //Test_4(game);
            Guidelines.Reset();
            //Test_5(game);
        }
    }
}