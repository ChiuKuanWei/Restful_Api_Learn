using API_TEST.DTO_Class;
using System;
using System.Collections.Generic;

namespace API_TEST.Models
{
    public partial class RgFreezer
    {
        public decimal FreezerId { get; set; }
        public decimal? FloorId { get; set; }
        public string? FreezerName { get; set; }
        public string? FreezerDesc { get; set; }
        public decimal? CreativeUserId { get; set; }
        public DateTime CreativeDatetime { get; set; }
        public string? Note { get; set; }

    }
}
