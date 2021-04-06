﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Day17
{
    class Ballot
    {
        public string Name { get; private set; }
        public List<Contest> Contests { get; private set; }
        public Contest CurrentContest { get; set; }
        public int CurrentContestIndex { get; set; }
        Dictionary<char, string> AllOptions = new Dictionary<char, string>() {
            {'0', "0: Display Ballot" },
            {'2', "2: Prev Contest" },
            {'4', "4: Prev Candidate" },
            {'5', "5: Select" },
            {'6', "6: Next Candidate" },
            {'8', "8: Next Contest" },
        };


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
            Console.WriteLine("\nDemo Ballot");
            for (int i = 0; i < Contests.Count; i++)
            {
                Console.WriteLine($"  Contest {i + 1} of {Contests.Count}: {Contests[i].Name} (Vote for {Contests[i].VoteFor})");
                for (int j = 0; j < Contests[i].Candidates.Count; j++)
                {
                    Console.WriteLine($"    {Contests[i].Candidates[j].DisplayText()}");
                }
            }
        }

        public void DisplayCurrentCandidate()
        {
            Console.WriteLine($"\n{CurrentContest.Name} - {CurrentContest.CurrentCandidate.DisplayText()}");
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
            CurrentContestIndex = 0;
            Contests[0].IsFirstContest = true;
            Contests[^1].IsLastContest = true;
            foreach (var contest in Contests)
            {
                contest.CurrentCandidateIndex = 0;
                contest.Candidates[0].IsFirstCandidate = true;
                contest.Candidates[^1].IsLastCandidate = true;
            }
        }
        
        public List<char> DisplayOptions()
        {
            List<char> options = new List<char>() { '0', '2', '4', '5', '6', '8' };
            var optionsSet = new Dictionary<char, string>(AllOptions);
            //0: Display Ballot
            //2: Prev Contest
            //4: Prev Candidate
            //5: Select
            //6: Next Candidate
            //8: Next Contest

            // remove any options not available to the situation
            if (CurrentContest.IsFirstContest)
            {
                options.RemoveAll(x => x == '2');                
            }
            if (CurrentContest.CurrentCandidate.IsFirstCandidate)
            {
                options.RemoveAll(x => x == '4');
            }
            if (CurrentContest.CurrentCandidate.IsLastCandidate)
            {
                options.RemoveAll(x => x == '6');
            }

            // modify any remaining options to the situation
            if (CurrentContest.IsLastContest)
            {
                optionsSet['8'] = "8: Done";
            }
            if (CurrentContest.CurrentCandidate.Selected)
            {
                optionsSet['5'] = "5: Deselect";
            }

            // print the options
            Console.Write("Press a key -- ");
            foreach (var item in options)
            {
                Console.Write(optionsSet.GetValueOrDefault(item) + "  ");
            }
            Console.WriteLine();

            // return the list of valid options
            return options;
        }

        public void GoToNextContest()
        {
            if (!CurrentContest.IsLastContest)
            {
                CurrentContestIndex++;
                CurrentContest = Contests[CurrentContestIndex];
                CurrentContest.CurrentCandidateIndex = 0;
                CurrentContest.CurrentCandidate = CurrentContest.Candidates[CurrentContest.CurrentCandidateIndex];
            }
        }
        
        public void GoToNextCandidate()
        {
            if (!CurrentContest.CurrentCandidate.IsLastCandidate)
            {
                CurrentContest.CurrentCandidateIndex++;
                CurrentContest.CurrentCandidate = CurrentContest.Candidates[CurrentContest.CurrentCandidateIndex];
            }
        }

        public void GoToPrevContest()
        {
            if (!CurrentContest.IsFirstContest)
            {
                CurrentContestIndex--;
                CurrentContest = Contests[CurrentContestIndex];
                CurrentContest.CurrentCandidateIndex = 0;
                CurrentContest.CurrentCandidate = CurrentContest.Candidates[CurrentContest.CurrentCandidateIndex];
            }
        }

        public void GoToPrevCandidate()
        {
            if (!CurrentContest.CurrentCandidate.IsFirstCandidate)
            {
                CurrentContest.CurrentCandidateIndex--;
                CurrentContest.CurrentCandidate = CurrentContest.Candidates[CurrentContest.CurrentCandidateIndex];
            }
        }

        public void SelectCandidate()
        {
            if (CurrentContest.NumOfVotes >= CurrentContest.VoteFor && !CurrentContest.CurrentCandidate.Selected)
            {
                Console.WriteLine("Overvote!");
                return;
            }
            CurrentContest.CurrentCandidate.ToggleSelection();
            if (CurrentContest.CurrentCandidate.Selected)
            {                
                if (CurrentContest.CurrentCandidate.Name.Contains("Write-in:"))
                {
                    Console.Write("\nEnter the writein name: ");
                    string writeinName = Console.ReadLine().Trim();
                    if (writeinName == "") 
                    {
                        // invalid name -> deselect the writein and return
                        CurrentContest.CurrentCandidate.ToggleSelection();
                        return;                    
                    }
                    CurrentContest.CurrentCandidate.Name = "Write-in: " + writeinName;
                }
                CurrentContest.NumOfVotes++;
            }
            if (!CurrentContest.CurrentCandidate.Selected)
            {
                if (CurrentContest.CurrentCandidate.Name.Contains("Write-in:"))
                {
                    CurrentContest.CurrentCandidate.Name = "Write-in:";
                }
                CurrentContest.NumOfVotes--;
            }
        }        
    }
}
