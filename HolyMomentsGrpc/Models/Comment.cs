using HolyMomentsGrpc.Data;

namespace HolyMomentsGrpc.Models
{
    public class Comment : DbBace
    {
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }
    }

}
