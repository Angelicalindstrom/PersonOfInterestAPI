using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonOfInterestAPI.Models
{
    public class Link
    {
        [Key]
        public int LinkId { get; set; }
        public string LinkUrl { get; set; }


        [ForeignKey("PersonInterest")]
        public int FkPersonInterestId { get; set; }
        public PersonInterest? PersonInterest { get; set; }
    }
}
