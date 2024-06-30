namespace HolyMomentsGrpc.Models
{
    public abstract class DbBace : IDbBace
    {
        public int Id { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedByUserID { get; set; }
    }
    public interface IDbBace
    {
        public DateTime ModifiedDate { get; set; }
        public string ModifiedByUserID { get; set; }
    }
}
