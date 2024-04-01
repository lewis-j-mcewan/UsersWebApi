using System.ComponentModel.DataAnnotations.Schema;

namespace Zip.WebAPI.Models
{
    public partial class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
    }
}
