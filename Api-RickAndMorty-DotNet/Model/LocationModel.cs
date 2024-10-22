using System;
using System.Collections.Generic;

namespace Api_RickAndMorty_DotNet.Model
{
    public class LocationModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }  
        public DateTime Created { get; set; } 
        public string Url { get; set; } 
    } 
      
}
