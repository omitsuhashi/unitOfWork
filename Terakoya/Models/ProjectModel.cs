﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Terakoya.Models
{
    public class ProjectModel : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
