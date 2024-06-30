using HolyMomentsGrpc.Data;

namespace HolyMomentsGrpc.Models
{
    public class Video : DbBace
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public DateTime UploadDate { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }

}
