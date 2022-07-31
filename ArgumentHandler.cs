using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TuneAssistant
{
    internal class ArgumentHandler
    {
        private readonly string[] args;
        private readonly string argumentList = @"Usage:
/p:{path}           PATH            Path to the folder containing files
/al:{albumname}     ALBUMNAME       Set the album name
/ar:{artistname}    ARTISTNAME      Set the artist name
/ca:{coverart}      COVERART        Set the cover art
/st:{true/ren}      SETTITLE        Set the title based on the filename (use rn with /nt if you want to get rid off numbers)
/nt:{true}          NUMBERTRACKS    Set number tracks based on the filename (example: `01 Test.mp3` -> #1)
/yr:{year}          YEAR            Set the release year
/gr:{genre}         GENRE           Set the genre";

        private IDictionary<int, string> arguments = new Dictionary<int,string>();

        public ArgumentHandler(string[] args) {
            this.args = args;
        }

        public bool ArgumentScanner()
        {
            foreach (string item in args)
            {
                for (int i = 0; i < regices.Length; i++)
                {
                    if (regices[i].IsMatch(item) && !arguments.ContainsKey(i))
                    {
                        string argument = regices[i].Replace(item, string.Empty);
                        arguments[i] = argument;
                    }
                }
            }

            if (!arguments.ContainsKey(0)) return false;
            return true;
        }

        public string GetEnabledArguments()
        {
            string s = "";
            foreach (var item in arguments)
            {
                s += item.Key.ToString() + "\t" + item.Value.ToString() + "\n";
            }
            return s;
        }

        public string GetArgumentList()
        {
            return argumentList;
        }

        public string? GetArgument(int i)
        {
            if (!arguments.ContainsKey(i))
            {
                return null;
            }
            string? argument = regices[i].Replace(arguments[i], string.Empty);
            return argument;
        }

        public IDictionary<int, string> GetArguments()
        {
            return arguments;
        }

        //Regex for arguments
        //Adding a new argument boils down to adding a new Regex

        Regex[] regices = new Regex[] {
            new Regex(@"/p:"), 
            new Regex(@"/al:"),
            new Regex(@"/ar:"),
            new Regex(@"/ca:"),
            new Regex(@"/st:"),
            new Regex(@"/nt:"),
            new Regex(@"/yr:"),
            new Regex(@"/gr:"),
            };
    }
}
