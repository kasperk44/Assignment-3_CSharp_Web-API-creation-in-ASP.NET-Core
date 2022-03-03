using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.DTOs.Franchise
{
    public class FranchiseReadDTO
    {
        public int FranchiseId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        public List<int> Movies { get; set; }
    }
}
