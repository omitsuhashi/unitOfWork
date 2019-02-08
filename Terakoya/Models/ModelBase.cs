using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Terakoya.Data;

namespace Terakoya.Models
{
    public class ModelBase
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public ApplicationUser UpdatedUser { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        public ApplicationUser CreatedUser { get; set; }
    }
}
