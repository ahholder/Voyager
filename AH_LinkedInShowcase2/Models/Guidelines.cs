using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH_LinkedInShowcase2.Models
{
    public class Guidelines
    {

        //Return the max length for a line in the console
        public static int LineLength()
        {
            return 110;
        }

        //Draw a standardized separation
        public static void Spacer(int size, bool drop)
        {
            if (size <= 0) size = LineLength();
            string spacer = "";
            for (var i = 0; i < LineLength() + 1; i++) spacer += "-";
            if (drop == true) spacer = "\n" + spacer;
            Console.WriteLine(spacer);
        }

        //Draws dashes to a line's length after text
        public static string Closer(string line, int length)
        {
            if (length < 1) length = LineLength();
            string spacer = line;
            for (var i = spacer.Length; i < length; i++) spacer += "-";
            return spacer;
        }

        //Secures a line of text between border lines
        public static string Frame(string line, int max)
        {
            if (max <= 0) max = LineLength();
            line = "|" + line;
            for (var i = line.Length; i < max; i++) line += " ";
            line += "|";
            return line;
        }

        //Centers the text based on line length
        public static string Center(string line, int max)
        {
            if (max <= 0) max = LineLength();
            max -= line.Length;
            int amount = max - LineLength();
            int siding = max / 2;
            int spill = max % 2;
            for (var i = 0; i < siding; i++)
            {
                line += " ";
                line = " " + line;
                //amount -= 2;
            }
            if (spill > 0) line += " ";
            //if (amount > 0) line += " ";
            return line;
        }

        //Contains a line to a given space
        public static List<string> LineWrapper(string line, int width, int buffer, bool frame)
        {
            if (width <= 0) width = LineLength() - 2;
            string[] words = line.Split(' ');
            //string[] lines = new string[words.Length];
            List<string> lines = new List<string>();
            lines.Add(" ");
            lines.Add("");
            int max = width - (buffer * 2);
            int tally = 1;
            for (var i = 0; i < words.Length; i++)
            {
                if (i == 0)
                {
                    lines[tally] += words[i];
                } else
                {
                    if ((lines[tally] + " " + words[i]).Length <= max)
                    {
                        lines[tally] += " ";
                        lines[tally] += words[i];
                    } else
                    {
                        tally += 1;
                        lines.Add("");
                        lines[tally] += words[i];
                    }
                }
            }
            lines.Add(" ");
            for (var i2 = 0; i2 < lines.Count; i2++)
            {
                for (var i = 0; i < buffer; i++)
                {
                    lines[i2] += " ";
                    lines[i2] = " " + lines[i2];
                }
                if (frame == true) lines[i2] = Frame(lines[i2],0);
            }
            return lines;
        }

        //Sets a unique int for a random seed based on the current time
        public static int RNG()
        {
            var rng = DateTime.Now.Second * DateTime.Now.Minute;
            rng += DateTime.Now.Hour * DateTime.Now.Minute;
            rng += DateTime.Now.Second * DateTime.Now.Hour;
            rng += DateTime.Now.Year * DateTime.Now.Day;
            var r = new Random();
            rng += r.Next(0, 100);
            return rng;
        }

        //Determines if 'a' or 'an' is appropriate
        public static string ArticleMaker(string word)
        {
            if (word.ToLower()[0] == 'a' || word.ToLower()[0] == 'e' || word.ToLower()[0] == 'i' || word.ToLower()[0] == 'o' || word.ToLower()[0] == 'u') return "an ";
            return "a ";
        }

        //Determines the length for a selection box
        public static int BoxSize()
        {
            return 24;
        }

        //Clears the screen and hides the cursor
        public static void Reset()
        {

            Console.Clear();
            Console.CursorVisible = false;
        }

        //Provides the generic text to continue past a non-selection prompt
        public static string Continuer(int max)
        {
            if (max <= 0) max = LineLength() - 1;
            string line = Center("Press <ANY KEY> to continue", max);
            return line;
        }

        //Returns a list of consonants
        public static string Consonants()
        {
            return "bcdfghjklmnpqrstvwxyz";
        }

        //Returns a list of Vowels
        public static string Vowels()
        {
            return "aeiou";
        }

        //Returns the total stats for a character
        public static int TotalStats()
        {
            return 10;
        }

        //Returns the names for each stat
        public static string StatName(int i)
        {
            if (i == 0) return "AGI";
            if (i == 1) return "CHA";
            if (i == 2) return "INT";
            if (i == 3) return "STR";
            return " ";
        }

        //Returns the names for each resource
        public static string RescName(int i)
        {
            if (i == 0) return "BIO";
            if (i == 1) return "FUEL";
            if (i == 2) return "GUN";
            if (i == 3) return "HULL";
            if (i == 4) return "MORALE";
            if (i == 5) return "SUPPLY";
            return "PROGRESS";
        }

        //Returns the term for gaining a resource
        public static string RescGainName(int i)
        {
            if (i == 0) return "Restores";
            if (i == 1) return "Gains";
            if (i == 2) return "Provides";
            if (i == 3) return "Repairs the";
            if (i == 4) return "Boosts";
            if (i == 5) return "Stocks";
            return "Adds";
        }

        //Returns the term for losing a resource
        public static string RescHitName(int i)
        {
            if (i == 0) return "health";
            if (i == 1) return "cells";
            if (i == 2) return "rounds";
            if (i == 3) return "durability";
            if (i == 4) return "favor";
            if (i == 5) return "stock";
            return " ";
        }

        //Returns the spacing necessary to line-up resources on the display window
        public static string LineUp(int index)
        {
            int[] lengths = new int[6];
            int max = 0;
            int extra = 0;
            string bonus = "";
            for (var i = 0; i < 6; i++)
            {
                lengths[i] = RescName(i).Length;
                if (lengths[i] > max) max = lengths[i];
            }
            extra = max - lengths[index];
            if (extra > 0)
            {
                for (var i = 0; i < extra; i++) bonus += " ";
            }
            return bonus;
        }

        //Returns the width of crew status windows
        public static int CrewWidth()
        {
            return (LineLength() - 85) - 2;
        }

        //Returns the width of the secondary info window
        public static int InfoWidth()
        {
            return (LineLength() - CrewWidth()) - 2;
        }

        //Returns the number of expected "info" lines for the lower windows
        public static int InfoLines()
        {
            return 29 - 7;
        }

        //Returns the value needed to achieve victory
        public static int VictoryProgress()
        {
            return 100;
        }

        //Returns the value that causes defeat
        public static int DefeatThreshold()
        {
            return 0;
        }

        //Returns the lines contained in the crewman stat window
        public static int CrewWindowSize()
        {
            return 7;
        }

        //Returns the lines contained in the ship stat window
        public static int ShipWindowSize()
        {
            return 9;
        }

        //Returns the lines contained in the progress stat window
        public static int ProgressWindowSize()
        {
            return 3;
        }

        //Returns the number of crewman total between reserve and active
        public static int TotalCrewSize()
        {
            return 6;
        }

        //Returns the number of active crewmen
        public static int TotalActiveCrew()
        {
            return 4;
        }

        //Returns the number of stats for each crewman
        public static int CrewStatCount()
        {
            return 4;
        }

        //Returns the number of resources tracked for the ship
        public static int ShipRescCount()
        {
            return 6;
        }

        //Returns the number of cycles before a new event appears
        public static int EventCycles()
        {
            return CrewStatCount() * 1;
        }

        //Returns the estimated total number of lines for crew + ship + progress status boxes
        public static int InfoBoxesSize()
        {
            return 21;
        }

        //Returns the estimated total number of extra stats to allocate during crew creation
        public static int BonusStats()
        {
            return 6;
        }

        //Returns the typical penalty threatened by an event
        public static int Penalty()
        {
            return 15;
        }

        //Returns the number of unique events available
        public static int EventPoolMax()
        {
            return 16;
        }

        //Returns the number of unique tasks available
        public static int TaskPoolMax()
        {
            return 28;
        }
        
        //Returns the starting resources for a player
        public static int StartResc()
        {
            return 45;
        }

        //Returns the multiplier for all progress tasks
        public static int ProgressMultiplier()
        {
            return 2;
        }
    }
}
