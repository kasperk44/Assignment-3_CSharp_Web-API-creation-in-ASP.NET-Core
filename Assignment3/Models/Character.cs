using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(50)]
        public string Alias { get; set; }
        [MaxLength(20)]
        public string Gender { get; set; }
        [MaxLength(300)]
        public string PictureURL { get; set; }
        public ICollection<Movie> Movie { get; set; }
    }
}
