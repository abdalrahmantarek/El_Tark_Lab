using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class VisitService
{
    public int ServiceId { get; set; }

    public int VisitId { get; set; }

    public int ServiceSerial { get; set; }

    public string? ApprovalId { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public DateTime ServiceDate { get; set; }

    public string? ServiceBy { get; set; }

    public int? Lab2lab { get; set; }

    public string? RequestedDoctor { get; set; }

    public string? RequestedDepartment { get; set; }

    public string? ServiceNotes { get; set; }

    public virtual Vendor? Lab2labNavigation { get; set; }

    public virtual ServicesPricelist Service { get; set; } = null!;

    public virtual Visit Visit { get; set; } = null!;
}
