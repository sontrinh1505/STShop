using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Model.Abstract;

namespace TeduShop.Model.Models
{
    [Table("Address")]
    public class Address : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public int StudentId { set; get; }

        [ForeignKey("StudentId")]
        public virtual Student Student { set; get; }
    }
}