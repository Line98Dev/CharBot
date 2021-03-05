﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharBot.Models
{
    class Cat
    {
        // "breeds": [],
        // "height": 281,
        // "id": "189",
        // "url": "https://cdn2.thecatapi.com/images/189.gif",
        // "width": 500

        public string Id { get; set; }
        public string Url { get; set; }
        public int Height { get; set;  } 
        public int Width { get; set; }
        public string[] Breeds { get; set; }
    }
}
