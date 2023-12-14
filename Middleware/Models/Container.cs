using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Middleware.Models
{
    public class Container
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Res_type { get; set; }
        public DateTime Creation_dt { get; set; }
        public int Parent { get; set; }
        public List<Data> Data { get; set; }

    }
}