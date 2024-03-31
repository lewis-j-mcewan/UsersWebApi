namespace Zip.WebAPI.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
    }
}
