using System;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            var ballot = new Ballot("Demo Ballot");
            Contest contest;

            contest = new Contest("BF", "Board of Finance", 4);
            contest.AddCandidate(new Candidate("RUTH", "Babe Ruth", "REP"));
            contest.AddCandidate(new Candidate("THORPE", "Jim Thorpe", "REP"));
            contest.AddCandidate(new Candidate("MARAVICH", "Pete Maravich", "REP"));
            contest.AddCandidate(new Candidate("ROBINSON", "Jackie Robinson", "REP"));
            contest.AddCandidate(new Candidate("COOLIDGE", "Calvin Coolidge", "DEM"));
            contest.AddCandidate(new Candidate("KENNEDY", "John F. Kennedy", "DEM"));
            contest.AddBlankWriteIns();
            ballot.AddContest(contest);

            contest = new Contest("BE", "Board of Education", 3);
            contest.AddCandidate(new Candidate("JONES", "Bobby Jones", "REP"));
            contest.AddCandidate(new Candidate("RUDOLPH", "Wilma Rudolph", "REP"));
            contest.AddCandidate(new Candidate("WALKER", "Jimmy Walker", "DEM"));
            contest.AddCandidate(new Candidate("DALEY", "Richard Daley", "DEM"));
            contest.AddCandidate(new Candidate("CHARLES", "Ray Charles", "PET"));
            contest.AddCandidate(new Candidate("DISNEY", "Walt Disney", "PET"));
            contest.AddCandidate(new Candidate("LINCOLN", "Abraham Lincoln", "PET"));
            contest.AddBlankWriteIns();
            ballot.AddContest(contest);

            contest = new Contest("BA", "Board of Assessment Appeals", 1);
            contest.AddCandidate(new Candidate("MARCIANO", "Rocky Marciano", "REP"));
            contest.AddBlankWriteIns();
            ballot.AddContest(contest);

            contest = new Contest("PZ", "Planning and Zoning Commission", 4);
            contest.AddCandidate(new Candidate("WASHINGTON", "George Washington", "REP"));
            contest.AddCandidate(new Candidate("BARTON", "Clara Barton", "REP"));
            contest.AddCandidate(new Candidate("ASH", "Arthur Ash", "REP"));
            contest.AddCandidate(new Candidate("PIERCE", "Arthur Pierce", "REP"));
            contest.AddCandidate(new Candidate("FRANKLIN-DEM", "Benjamin Franklin", "DEM"));
            contest.AddCandidate(new Candidate("FRANKLIN-SAN", "Benjamin Franklin", "SNA"));
            contest.AddBlankWriteIns();
            ballot.AddContest(contest);
            ballot.Output();
            Vote(ballot);
            ballot.FirstCandidates();
            ballot.LastCandidates();
        }

        static void Vote(Ballot ballot)
        {
            ballot.SetFirstLast();
            Console.WriteLine();
            ballot.DisplayCurrentCandidate();
        }
        

    }

    
}
