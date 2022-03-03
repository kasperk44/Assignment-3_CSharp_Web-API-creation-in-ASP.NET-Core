using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        [MaxLength(100)]
        public string MovieTitle { get; set; }
        [MaxLength(20)]
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        [MaxLength(50)]
        public string Director { get; set; }
        [MaxLength(300)]
        public string PictureURL { get; set; }
        [MaxLength(300)]
        public string TrailerURL { get; set; }
        public ICollection<Character> Character { get; set; }
        public Franchise Franchise { get; set; }
        public int FranchiseId { get; set; }
    }
}
