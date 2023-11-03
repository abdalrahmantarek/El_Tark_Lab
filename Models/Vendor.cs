using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string VendorName { get; set; } = null!;

    public string VendorAddress { get; set; } = null!;

    public int VendorTel { get; set; }

    public int? VendorTel2 { get; set; }

    public string? VendorWebsite { get; set; }

    public string? OfficialEmail { get; set; }

    public DateTime? CreationDate { get; set; }

    public int VendorCat { get; set; }

    public string? LastUpdateBy { get; set; }

    public string? LastUpdateFrom { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public virtual VendorCategory VendorCatNavigation { get; set; } = null!;

    public virtual ICollection<VendorContact> VendorContacts { get; set; } = new List<VendorContact>();

    public virtual ICollection<VendorContract> VendorContracts { get; set; } = new List<VendorContract>();

    public virtual ICollection<VisitService> VisitServices { get; set; } = new List<VisitService>();
}
