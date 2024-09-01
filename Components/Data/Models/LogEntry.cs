namespace AttendanceManagement.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string Initiator { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string? TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string OldValues { get; set; } = default!;
        public string NewValues { get; set; } = default!;
        public string AffectedColumns { get; set; } = default!;

    }

}
