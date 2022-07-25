using System;
using System.Collections.Generic;

namespace API_TEST.Models
{
    public partial class RgManagerData
    {
        public int Id { get; set; }
        public string LaboratoryUse { get; set; } = null!;
        public string ManagerName { get; set; } = null!;
        public decimal ManagerNum { get; set; }

    }
}
