using System.ComponentModel.DataAnnotations;

namespace PersonOfInterestAPI.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string PersonEmail { get; set; }
        public string PersonPhoneNumber { get; set; }

    }
}
