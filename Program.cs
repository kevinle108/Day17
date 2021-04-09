using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            string ballotFileName = "BALLOT_0002.json";
            
            string contestCode;
            string contestData;
            JsonDocument contestDoc;
            JsonElement contestRoot;
            string contestName;
            int contestMaxChoices;
            bool contestWriteIn;
            JsonElement contestCandidates;
            Contest contest;
            
            string candidateCode;
            string candidateName;
            string candidateParty;
            Candidate candidate;

            Dictionary<string, Candidate> candidatesTable;
            List<string> candidatesOrder;

            // access the ballot json file 
            string ballotData = File.ReadAllText(ballotFileName);
            JsonDocument ballotDoc = JsonDocument.Parse(ballotData);
            JsonElement ballotRoot = ballotDoc.RootElement;
            string ballotName = ballotRoot.GetProperty("BallotName").ToString();
            JsonElement contests = ballotRoot.GetProperty("Contests");
            Ballot ballot = new Ballot(ballotName);

            // for each contest in ballot
            for (int i = 0; i < contests.GetArrayLength(); i++)
            {
                // get the order of candidate codes
                JsonElement order = contests[i].GetProperty("CandidateCodes");
                candidatesOrder = new List<string>();
                for (int orderIndex = 0; orderIndex < order.GetArrayLength(); orderIndex++)
                {
                    candidatesOrder.Add(order[orderIndex].GetString());
                }

                // begin new candidatesTable
                candidatesTable = new Dictionary<string, Candidate>();

                // access the contest json file to get all candidate information
                contestCode = contests[i].GetProperty("ContestCode").ToString();
                contestData = File.ReadAllText("CONTEST_" + contestCode + ".json");
                contestDoc = JsonDocument.Parse(contestData);
                contestRoot = contestDoc.RootElement;
                contestName = contestRoot.GetProperty("ContestName").GetString();
                contestRoot.GetProperty("MaxChoices").TryGetInt32(out contestMaxChoices);
                contestWriteIn = contestRoot.GetProperty("WriteIn").GetBoolean();
                contestCandidates = contestRoot.GetProperty("Candidates");             
                contest = new Contest(contestCode, contestName, contestMaxChoices);

                // for each candidate in contest
                for (int j = 0; j < contestCandidates.GetArrayLength(); j++)
                {
                    candidateCode = contestCandidates[j].GetProperty("CandidateCode").GetString();
                    candidateName = contestCandidates[j].GetProperty("CandidateName").GetString();
                    candidateParty = contestCandidates[j].GetProperty("CandidateParty").GetString();
                    candidate = new Candidate(candidateCode, candidateName, candidateParty);
                    //contest.AddCandidate(candidate);

                    // add candidate and its candidateCode to candidatesTable
                    candidatesTable.Add(candidateCode, candidate);
                }

                // add candidates in the correct order
                foreach (string candCode in candidatesOrder)
                {
                    contest.AddCandidate(candidatesTable[candCode]);
                }

                // add writeins if appropriate
                if (contestWriteIn) contest.AddBlankWriteIns();

                ballot.AddContest(contest);
            }

            //var ballot = new Ballot("Demo Ballot");

            //contest = new Contest("BF", "Board of Finance", 4);
            //contest.AddCandidate(new Candidate("RUTH", "Babe Ruth", "REP"));
            //contest.AddCandidate(new Candidate("THORPE", "Jim Thorpe", "REP"));
            //contest.AddCandidate(new Candidate("MARAVICH", "Pete Maravich", "REP"));
            //contest.AddCandidate(new Candidate("ROBINSON", "Jackie Robinson", "REP"));
            //contest.AddCandidate(new Candidate("COOLIDGE", "Calvin Coolidge", "DEM"));
            //contest.AddCandidate(new Candidate("KENNEDY", "John F. Kennedy", "DEM"));
            //contest.AddBlankWriteIns();
            //ballot.AddContest(contest);

            //contest = new Contest("BE", "Board of Education", 3);
            //contest.AddCandidate(new Candidate("JONES", "Bobby Jones", "REP"));
            //contest.AddCandidate(new Candidate("RUDOLPH", "Wilma Rudolph", "REP"));
            //contest.AddCandidate(new Candidate("WALKER", "Jimmy Walker", "DEM"));
            //contest.AddCandidate(new Candidate("DALEY", "Richard Daley", "DEM"));
            //contest.AddCandidate(new Candidate("CHARLES", "Ray Charles", "PET"));
            //contest.AddCandidate(new Candidate("DISNEY", "Walt Disney", "PET"));
            //contest.AddCandidate(new Candidate("LINCOLN", "Abraham Lincoln", "PET"));
            //contest.AddBlankWriteIns();
            //ballot.AddContest(contest);

            //contest = new Contest("BA", "Board of Assessment Appeals", 1);
            //contest.AddCandidate(new Candidate("MARCIANO", "Rocky Marciano", "REP"));
            //contest.AddBlankWriteIns();
            //ballot.AddContest(contest);

            //contest = new Contest("PZ", "Planning and Zoning Commission", 4);
            //contest.AddCandidate(new Candidate("WASHINGTON", "George Washington", "REP"));
            //contest.AddCandidate(new Candidate("BARTON", "Clara Barton", "REP"));
            //contest.AddCandidate(new Candidate("ASH", "Arthur Ash", "REP"));
            //contest.AddCandidate(new Candidate("PIERCE", "Arthur Pierce", "REP"));
            //contest.AddCandidate(new Candidate("FRANKLIN-DEM", "Benjamin Franklin", "DEM"));
            //contest.AddCandidate(new Candidate("FRANKLIN-SAN", "Benjamin Franklin", "SNA"));
            //contest.AddBlankWriteIns();
            //ballot.AddContest(contest);
            ballot.Output();
            Vote(ballot);
        }

        static void Vote(Ballot ballot)
        {
            ballot.PrepForVoting();
            char userInput;
            bool done = false;
            do
            {
                ballot.DisplayCurrentCandidate();
                List<char> options = ballot.DisplayOptions();
                userInput = Console.ReadKey(true).KeyChar;

                // if user does not enter a valid option, set userInput to trigger default switch case
                if (!options.Contains(userInput)) userInput = '!';
                switch (userInput)
                {
                    case '0':
                        ballot.Output();
                        break;
                    case '2':
                        ballot.GoToPrevContest();
                        break;
                    case '4':
                        ballot.GoToPrevCandidate();
                        break;
                    case '5':
                        ballot.SelectCandidate();
                        break;
                    case '6':
                        ballot.GoToNextCandidate();
                        break;
                    case '8':
                        if (ballot.CurrentContest.IsLastContest)
                        {
                            done = true;
                            break;
                        }
                        ballot.GoToNextContest();
                        break;
                    default:
                        Console.WriteLine("Invalid key!");
                        break;
                }
            } while (!done);
            Console.WriteLine("Here is your final ballot:");
            ballot.Output();
            Console.WriteLine("\n...Program ended!");
        }        
    }

    
}
