using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzioPlayer.SqlLite
{
    public class Song
    {
        public string Name { get; set; }

        public string Artist { get; set; }

        public double Duration { get; set; }

        public string Path { get; set; }

        public bool Selected { get; set; }

        public Song(string name, string artist, double duration, string path)
        {
            Name = name;
            Artist = artist;
            Duration = duration;
            Path = path;
        }

        public Song() { }

    }
}
