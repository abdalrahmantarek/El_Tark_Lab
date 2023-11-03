using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class ContactsOccupation
{
    public int OccupationId { get; set; }

    public string OccupationName { get; set; } = null!;

    public virtual ICollection<VendorContact> VendorContacts { get; set; } = new List<VendorContact>();
}
