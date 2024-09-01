using AttendanceManagement.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceManagement.Models
{

    public class TenantUser
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = default!;

        [ForeignKey(nameof(TenantId))]
        public Tenant Tenant { get; set; } = default!;
    }

}
