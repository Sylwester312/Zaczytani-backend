using Zaczytani.Domain.Enums;

namespace Zaczytani.Domain.Entities;

public class Report
{
    public Guid Id { get; set; }

    public string Content { get; set; }

    public ReportCategory Category { get; set; }

    public virtual User User { get; set; }

    public virtual Review Review { get; set; }
}
