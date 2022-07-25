using System;
using System.Collections.Generic;

namespace API_TEST.Models
{
    public partial class Group
    {
        public int Id { get; set; }
        public string FreezerName { get; set; } = null!;
        public string FreezerToken { get; set; } = null!;
    }
}
