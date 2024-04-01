using System.ComponentModel.DataAnnotations.Schema;

namespace Zip.WebAPI.Models
{
    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Expenses { get; set; }
        public decimal Salary { get; set; }

        public virtual Account Account { get; set; }
    }
}
