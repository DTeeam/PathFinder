using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PathFinder
{
    [Table("achievments")]

    public class achievments
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public double coordX { get; set; }
        public double coordY { get; set; }
    }

    [Table("points")]

    public class points
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public double coordX { get; set; }
        public double coordY { get; set; }
        public string description { get; set; }
        public string hint { get; set; }
    }

    
}
