using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PocketCards.Models
{
    public class Card
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public int Level { get; set; }
        public DateTime LastFlip { get; set; }        
    }
}
