namespace UserProtection.Application.Dtos.Security
{
    public class TrustedLinkDto
    {
        public int LinkId { get; set; }
        public string Domain { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
    }
}
