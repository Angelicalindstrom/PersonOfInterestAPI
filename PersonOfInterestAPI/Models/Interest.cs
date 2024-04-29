using System.ComponentModel.DataAnnotations;

namespace PersonOfInterestAPI.Models
{
    public class Interest
    {
        [Key]
        public int InterestId { get; set; }
        public string InterestTitle { get; set;}
        public string InterestDescription { get; set;}

    }
}
