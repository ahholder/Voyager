using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH_LinkedInShowcase2.Models
{
    public class Crew
    {
        public string Name { get; set; } = "";
        public int[] Stats = { 1, 1, 1, 1 };
        public bool Busy { get; set; } = false;
        public static List<string> NamesList { get; set; } = new List<string>();

        //Creates a list of all possible 3-letter name combinations (consonant, vowel, consonant)
        public static void MakeNames()
        {
            NamesList = new List<string>();
            string consonants = Guidelines.Consonants();
            string vowels = Guidelines.Vowels();
            string title = "";
            var r = new Random(Guidelines.RNG());
            for (var i = 0; i < consonants.Length; i++)
            {
                for (var i2 = 0; i2 < vowels.Length; i2++)
                {
                    for (var i3 = 0; i3 < consonants.Length; i3++)
                    {
                        title = $"{char.ToUpper(consonants[i])}{vowels[i2]}{consonants[i3]}";
                        NamesList.Add(title);
                    }
                }
            }
        }

        //Creates a 3-letter name for a crewman -- Replaced by MakeNames
        public void MakeNameOriginal()
        {
            string consonants = Guidelines.Consonants();
            string vowels = Guidelines.Vowels();
            string name = "";
            var r = new Random(Guidelines.RNG());
            int i = r.Next(0, consonants.Length);
            name += $"{char.ToUpper(consonants[i])}";
            i = r.Next(0, vowels.Length);
            name += $"{vowels[i]}";
            i = r.Next(0, consonants.Length);
            name += $"{consonants[i]}";
            Name = name;
        }

        //Assigns a 3-letter name for a crewman from an existing list of ~2205 combinations
        public void MakeName()
        {
            var r = new Random(Guidelines.RNG());
            int i = r.Next(0, NamesList.Count);
            string title = NamesList[i];
            Name = title;
            NamesList.Remove(title);
        }

        //Assigns stats to a crewman -- Replaced by MakeStats
        public void MakeStatsOriginal()
        {
            int total = Guidelines.TotalStats();
            int max = total - 1;
            int remaining = total;
            var r = new Random(Guidelines.RNG());
            //Assign Stat #1
            int i = r.Next(1, max);
            int s = r.Next(0, 3);
            Stats[s] = i;
            remaining = Guidelines.TotalStats() - (Stats[0] + Stats[1] + Stats[2]);
            max = remaining - 1;
            //Assign Stat #2
            s += r.Next(1, 2);
            if (s > 2) s -= 2;
            i = r.Next(1, max);
            Stats[s] = i;
            remaining = Guidelines.TotalStats() - (Stats[0] + Stats[1] + Stats[2]);
            //Assign Stat #3
            for (var i2 = 0; i2 < 3; i2++)
            {
                if (Stats[i2] == 0)
                {
                    Stats[i2] = remaining;
                }
            }
        }

        //Assigns stats to a crewman
        public void MakeStats()
        {
            int total = Guidelines.TotalStats();
            var r = new Random(Guidelines.RNG());
            int s = 0;
            for (var i = 0; i < Guidelines.BonusStats(); i++)
            {
                s = r.Next(0, Guidelines.CrewStatCount());
                Stats[s] += 1;
            }
        }
    }
}
