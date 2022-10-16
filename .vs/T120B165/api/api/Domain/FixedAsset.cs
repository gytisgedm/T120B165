namespace api.Domain
{
    public class FixedAsset
    {
        public string Code { get; set; }
        public string Class { get; set; }
        public string? Description { get; set; }
        public string? ManagedBy { get; set; }
        public string? AssignedTo { get; set; }
        public string? AssignedBy { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime? BoughtAt { get; set; }
        public bool IsSold { get; set; } = false;

        public ICollection<FixedAssetEvent> Events { get; set; }
    }
}
