using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PathFinder
{
    [Table("points")]

    public class Point
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public double coordX { get; set; }
        public double coordY { get; set; }
        public string description { get; set; }
        public string hint { get; set; }
        public int discovered { get; set; }
        public string image { get; set; }
    }

    
}
