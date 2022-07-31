using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TuneAssistant
{
    internal class Tagger
    {
        private string[] files;

        public Tagger(string[] files)
        {
            this.files = files;
        }

        public void RunTagger(int argument, string value)
        {
            switch (argument)
            {
                case 1: addAlbum(value); break;
                case 2: AddArtist(value); break;
                case 3: AddCover(value); break;
                case 4: SetTitle(value); break;
                case 5: SetTrackNumber(value); break;
                case 6: AddYear(value); break;
                case 7: AddGenre(value); break;
                default: break;
            }
        }

        //Adding Artist tag
        public void AddArtist(string? artistName)
        {              
            if (artistName != null && artistName.Length > 0)
            {
                foreach (string file in files)
                {
                    var tfile = TagLib.File.Create(file);
                    //Tag.Performers is the artist tag (that most apps use in my experience)
                    tfile.Tag.Performers = new string[]{artistName};
                    tfile.Save();
                    Console.WriteLine($"Added artist tag {artistName} to {file}");
                }
            }
        }

        //Adding Album tag
        public void addAlbum(string? albumName)
        {              
            
            if (albumName != null && albumName.Length > 0)
            {
                foreach (string file in files)
                {
                    var tfile = TagLib.File.Create(file);
                    tfile.Tag.Album = albumName;
                    tfile.Save();
                    Console.WriteLine($"Added album tag {albumName} to {file}");
                }
            }
        }

        //Adding cover art
        
        public void AddCover(string? coverArt)
        {
            Regex regex = new Regex(@"[.]jpg|[.]png|[.]jfif|[.]webp|[.]jpeg$");
            if (coverArt != null && coverArt.Length > 0 && regex.IsMatch(coverArt))
            {
                foreach (string file in files)
                {
                    var tfile = TagLib.File.Create(file);
                    tfile.Tag.Pictures = new TagLib.IPicture[]{new TagLib.Picture(coverArt)};
                    tfile.Save();
                    Console.WriteLine($"Added album cover {coverArt} to {file}");
                }
            }
        }
        //Adding title

        public void SetTitle(string? setTitle)
        {
            if (setTitle != null && setTitle.Length > 0 )
            {
                foreach (string file in files)
                {
                    var tfile = TagLib.File.Create(file);
                    Regex regex = new Regex(@"[ \w\-\(\)=`']+?(?=\.)");
                    MatchCollection matches = regex.Matches(file);
                    if (setTitle == "true") tfile.Tag.Title = matches[0].ToString();
                    //remove numbers if ren selected
                    else if (setTitle == "ren")
                    {
                        regex = new Regex(@"^[0-9]{1,6}\s");
                        tfile.Tag.Title = regex.Replace(matches[0].ToString(), string.Empty);
                    }
                    tfile.Save();
                    Console.WriteLine($"Added title {tfile.Tag.Title} to {file}");
                }
            }
        }

        //Adding track numbers

        public void SetTrackNumber(string? setTrackNumber)
        {
            if (setTrackNumber != null && setTrackNumber.Length > 0 && setTrackNumber == "true")
            {
                foreach (string file in files)
                {
                    var tfile = TagLib.File.Create(file);
                    //first we have to get the file name, then the number
                    Regex regex = new Regex(@"[ \w\-\(\)=`']+?(?=\.)");
                    MatchCollection matches = regex.Matches(file);
                    Regex regex2 = new Regex(@"^[0-9]{1,6}\s");
                    MatchCollection matches2 = regex2.Matches(matches[0].ToString());
                    tfile.Tag.Track = uint.Parse(matches2[0].ToString());
                    tfile.Save();
                    Console.WriteLine($"Added track number {matches2[0].ToString()}to {file}");
                }
            }
        }

        //Adding Year tag
        public void AddYear(string? releaseYear)
        {              
            
            if (releaseYear != null && releaseYear.Length > 0 && !double.IsNaN(double.Parse(releaseYear)))
            {
                foreach (string file in files)
                {
                    var tfile = TagLib.File.Create(file);
                    tfile.Tag.Year = uint.Parse(releaseYear);
                    tfile.Save();
                    Console.WriteLine($"Added year tag {releaseYear} to {file}");
                }
            }
        }

        //Adding Genre tag
        public void AddGenre(string? genreName)
        {              
            
            if (genreName != null && genreName.Length > 0)
            {
                foreach (string file in files)
                {
                    var tfile = TagLib.File.Create(file);
                    tfile.Tag.Genres = new string[] {genreName};
                    tfile.Save();
                    Console.WriteLine($"Added genre tag {genreName} to {file}");
                }
            }
        }

    }
}
