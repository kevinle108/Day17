using System;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            Candidate can1 = new Candidate("KEV", "Kevin Le", "IND");
            can1.ToggleSelection();
            //can1.ToggleSelection();
            can1.Print();
        }
    }

    public class Candidate
    {

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Party { get; private set; }

        public bool Selected { get; private set; }

        public Candidate(string code, string name, string party)
        {
            Code = code;
            Name = name;
            Party = party;
            Selected = false;
        }

        public void Print()
        {
            string txt = $"{Name}";
            if (!Name.Contains("Write-in:"))
            {
                txt += $" ({Party})";
            }
            if (Selected)
            {
                txt += $" -- Selected";
            }
            Console.WriteLine(txt);
        }

        public void ToggleSelection()
        {
            if (Selected) Selected = false;
            else Selected = true;
        }
    }
}
