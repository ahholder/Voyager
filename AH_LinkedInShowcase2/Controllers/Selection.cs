using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH_LinkedInShowcase2.Controllers
{
    public class Selection
    {
        //Standard choice options - Determine valid input based on key press + available choices
        public static int Option(int maxChoices, int index)
        {
            bool choosing = true;
            int chosen = index;
            while (choosing)
            {
                //Receive key input
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        chosen -= 1;
                        break;
                    case ConsoleKey.RightArrow:
                        chosen += 1;
                        break;
                    case ConsoleKey.Q:
                        chosen -= 1;
                        break;
                    case ConsoleKey.W:
                        chosen += 1;
                        break;
                    case ConsoleKey.Enter:
                        chosen = -100;
                        break;
                    default:
                        break;
                }
                if (chosen >= maxChoices) chosen = 0;
                if (chosen < 0 && chosen != -100) chosen = maxChoices - 1;
                Console.WriteLine("\b \b");
                choosing = false;
            }
            Console.WriteLine("\b \b");
            return chosen;

        }

        //Generic "Press Any Key to Continue"
        public static void Continuer()
        {
            Console.ReadKey();
            Console.WriteLine("\b \b");
        }
    }
}
