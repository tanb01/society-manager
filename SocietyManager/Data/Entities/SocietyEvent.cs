namespace SocietyManager.Data.Entities
{
    public class SocietyEvent
    {
        public int SocietyId { get; set; }
        public int EventId { get; set; }
        public virtual Society Society { get; set; }
        public virtual Event Event { get; set; }
    }
}
