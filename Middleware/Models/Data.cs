﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Middleware.Models
{
    public class Data
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Creation_dt { get; set; }
        public int Parent { get; set; }

        public string Res_type { get; set; }
    }
}