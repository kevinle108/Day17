using System;
using System.Collections.Generic;
using System.Text;

namespace Day17
{
    public class Candidate
    {

        public string Code { get; }
        public string Name { get; set; }
        public string Party { get; }
        public bool Selected { get; private set;}
        public bool IsFirstCandidate { get; set; }
        public bool IsLastCandidate { get; set; }

        public Candidate(string code, string name, string party)
        {
            Code = code;
            Name = name;
            Party = party;
            Selected = false;
            IsFirstCandidate = false;
            IsLastCandidate = false;
        }

        public string DisplayText()
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
            return txt;
        }

        public void ToggleSelection()
        {
            if (Selected) Selected = false;
            else Selected = true;
        }
    }
}
