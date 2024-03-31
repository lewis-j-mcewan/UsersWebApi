namespace Zip.WebAPI.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
    }
}
