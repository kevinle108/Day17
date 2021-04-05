using System;
using System.Collections.Generic;
using System.Text;

namespace Day17
{
    class Ballot
    {
        public string Name { get; private set; }
        public List<Contest> Contests { get; private set; }

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
            for (int i = 0; i < Contests.Count; i++)
            {
                Contests[i].Print();
            }
        }
    }
}
