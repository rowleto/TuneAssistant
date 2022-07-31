using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TuneAssistant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ArgumentHandler argumentHandler = new ArgumentHandler(args);
            if (args.Length == 0)
            {
                Console.WriteLine(argumentHandler.GetArgumentList());
                return;
            }
            if (!argumentHandler.ArgumentScanner())
            {
                Console.WriteLine("Error: no path provided");
                return;
            }

            Console.WriteLine($"tune-collector: initializing\n");
            //Checking if the path is valid
            try
            {
                Directory.EnumerateFiles(argumentHandler.GetArgument(0), "*.mp3");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            //Accepted file extensions
            Regex regex = new Regex(@"[.]mp3|[.]wav|[.]flac|[.]mp4|[.]mka|[.]mkv|[.]ogg|[.]wma|[.]aif|[.]aifc|[.]aiff$");
            
            var files = Directory.EnumerateFiles(argumentHandler.GetArgument(0), "*").Where(file => regex.IsMatch(file)).ToArray();
            Console.WriteLine($"Found {files.Length} files in {argumentHandler.GetArgument(0)}");
            Tagger tagger = new Tagger(files);

            IDictionary<int,string> arguments = argumentHandler.GetArguments();

            foreach (var item in arguments)
            {
                tagger.RunTagger(item.Key, item.Value);
            }

            Console.WriteLine("\nSuccessfully finished all changes\nThank you for using tune_collector!");
        }
    }
}
