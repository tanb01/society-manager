namespace SocietyManager.Data.Entities
{
    public class SocietyStudent
    {
        public int SocietyId { get; set; }
        public int StudentId { get; set; }
        public virtual Society Society { get; set; }
        public virtual Student Student { get; set; }
    }
}
