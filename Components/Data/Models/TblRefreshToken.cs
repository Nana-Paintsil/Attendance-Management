namespace AttendanceManagement.Models
{
    public partial class TblRefreshtoken
    {
        public string UserId { get; set; } = null!;
        public string TokenId { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public bool? IsActive { get; set; }
    }
}
