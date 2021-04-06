using System;
using System.Collections.Generic;
using System.Text;

namespace Day17
{
    class Contest
    {
        public string Code { get; private set; }        
        public string Name { get; private set; }
        public int VoteFor { get; private set; }
        public List<Candidate> Candidates { get; private set; }
        public Candidate CurrentCandidate { get; private set; }
        public bool IsFirstContest { get; set; }
        public bool IsLastContest { get; set; }

        public Contest(string code, string name, int voteFor)
        {
            Code = code;
            Name = name;
            VoteFor = voteFor;
            Candidates = new List<Candidate>();
            IsFirstContest = false;
            IsLastContest = false;
        }

        public void AddCandidate(Candidate cand)
        {
            Candidates.Add(cand);
        }
        
        public void AddBlankWriteIns()
        {
            for (int i = 0; i < VoteFor; i++)
            {
                Candidates.Add(new Candidate($"Writein-{i}", "Write-in:", ""));
            }
            CurrentCandidate = Candidates[0];
            Candidates[0].IsFirstCandidate = true;
            Candidates[Candidates.Count-1].IsLastCandidate = true;
        }

        public void Print()
        {
            Console.WriteLine(Name);
            foreach (Candidate cand in Candidates)
            {
                Console.WriteLine(cand.DisplayText());                
            }
        }

    }
}
