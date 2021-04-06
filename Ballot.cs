using System;
using System.Collections.Generic;
using System.Text;

namespace Day17
{
    class Ballot
    {
        public string Name { get; private set; }
        public List<Contest> Contests { get; private set; }
        public Contest CurrentContest { get; set; }

        public Ballot(string name)
        {
            Name = name;
            Contests = new List<Contest>();
        }

        public void AddContest(Contest contest)
        {
            Contests.Add(contest);
        }

        public void Output()
        {
            Console.WriteLine("Demo Ballot");
            for (int i = 0; i < Contests.Count; i++)
            {
                Console.WriteLine($"  Contest {i+1} of {Contests.Count}: {Contests[i].Name}");
                for (int j = 0; j < Contests[i].Candidates.Count; j++)
                {
                    Console.WriteLine($"    {Contests[i].Candidates[j].DisplayText()}");
                }
            }
            Console.WriteLine();
        }

        public void DisplayCurrentCandidate()
        {
            Console.WriteLine($"{CurrentContest.Name} - {CurrentContest.CurrentCandidate.DisplayText()}");
        }

        public void FirstCandidates()
        {
            Console.WriteLine("Printing First Candidates:");
            foreach (var contest in Contests)
            {
                foreach (var candidate in contest.Candidates)
                {
                    if (candidate.IsFirstCandidate)
                    {
                        Console.WriteLine("  " + candidate.DisplayText());
                    }
                }
            }
            Console.WriteLine();
        }
        public void LastCandidates()
        {
            Console.WriteLine("Printing Last Candidates:");
            foreach (var contest in Contests)
            {
                foreach (var candidate in contest.Candidates)
                {
                    if (candidate.IsLastCandidate)
                    {
                        Console.WriteLine("  " + candidate.DisplayText());
                    }
                }
            }
            Console.WriteLine();
        }

        public void SetFirstLast()
        {
            CurrentContest = Contests[0];
            Contests[0].IsFirstContest = true;
            Contests[^1].IsFirstContest = true;
            foreach (var contest in Contests)
            {
                contest.Candidates[0].IsFirstCandidate = true;
                contest.Candidates[^1].IsLastCandidate = true;
            }
        }
    }
}
