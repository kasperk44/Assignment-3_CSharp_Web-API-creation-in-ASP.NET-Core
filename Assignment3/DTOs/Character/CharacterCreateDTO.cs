using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.DTOs.Character
{
    public class CharacterCreateDTO
    {
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(50)]
        public string Alias { get; set; }
        [MaxLength(20)]
        public string Gender { get; set; }
        [MaxLength(300)]
        public string PictureURL { get; set; }
    }
}
