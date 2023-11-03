using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class VendorContact
{
    public int ContactId { get; set; }

    public string ContactName { get; set; } = null!;

    public string ContactTel { get; set; } = null!;

    public string? ContactTel2 { get; set; }

    public string? ContactEmail { get; set; }

    public int ContactVendorId { get; set; }

    public int Occuption { get; set; }

    public string? ContactNotes { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string LastUpdateFrom { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public virtual Vendor ContactVendor { get; set; } = null!;

    public virtual ContactsOccupation OccuptionNavigation { get; set; } = null!;
}
