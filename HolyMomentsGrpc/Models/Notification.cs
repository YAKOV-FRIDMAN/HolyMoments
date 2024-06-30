using HolyMomentsGrpc.Data;

namespace HolyMomentsGrpc.Models
{
    public class Notification : DbBace
    {
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime NotificationDate { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }

}
