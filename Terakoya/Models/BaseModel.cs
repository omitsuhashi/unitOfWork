using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Terakoya.Models
{
    public class BaseModel<T> where T: IEquatable<T>
    {
        [Key]
        public T Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public IdentityUser CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IdentityUser UpdatedBy { get; set; }
    }
}
