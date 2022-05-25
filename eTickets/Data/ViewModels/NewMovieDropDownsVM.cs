using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropDownsVM
    {
        public NewMovieDropDownsVM()
        {
            Cinemas = new List<Cinema>();
            Producers = new List<Producer>();
            Actors = new List<Actor>();
        }

        public List<Cinema> Cinemas { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
