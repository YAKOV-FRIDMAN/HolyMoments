using HolyMomentsGrpc.Data;

namespace HolyMomentsGrpc.Models
{
    public class Like : DbBace
    {
        public DateTime LikeDate { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }
    }

}
