namespace AttendanceManagement.Models
{
    public partial class UserAuth
    {

        public Guid TenantId { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; } = default!; 
        public string Username { get; set; } = default!; 
        public string PasswordHash { get; set; } = default!;

    }


}
