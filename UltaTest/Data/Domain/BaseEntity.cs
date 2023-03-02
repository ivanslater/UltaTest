using System.ComponentModel.DataAnnotations;

namespace UltaTest.Data.Domains
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
