namespace API_TEST.DTO_Class
{
    public class FreezerSelectDTO
    {
        public decimal FreezerId { get; set; }

        public decimal? FloorId { get; set; }

        public string? FreezerName { get; set; }

        public string? FreezerDesc { get; set; }

        public DateTime CreativeDatetime { get; set; }

        public string? FREEZER_TOKEN { get; set; }

        public ICollection<RgManagerDataDTO>? rgManagerDataDTOs { get; set; }
    }
}
