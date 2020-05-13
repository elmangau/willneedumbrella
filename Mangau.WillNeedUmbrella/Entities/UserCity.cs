using System.ComponentModel.DataAnnotations.Schema;

namespace Mangau.WillNeedUmbrella.Entities
{
    [Table("wnuusercity")]
    public class UserCity
    {
        public long UserId { get; set; }

        public User User { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }
    }
}
