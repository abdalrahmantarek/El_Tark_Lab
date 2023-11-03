using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class ServicesPricelist
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public string? ServiceNameAr { get; set; }

    public double ServicePrice { get; set; }

    public int ServiceCat { get; set; }

    public int ContractId { get; set; }

    public int? StandRef { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string LastUpdateFrom { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public virtual VendorContract Contract { get; set; } = null!;

    public virtual ICollection<PatientInvoiceDetail> PatientInvoiceDetails { get; set; } = new List<PatientInvoiceDetail>();

    public virtual ICollection<ServiceBarcode> ServiceBarcodes { get; set; } = new List<ServiceBarcode>();

    public virtual ServicesCategory ServiceCatNavigation { get; set; } = null!;

    public virtual StandardService? StandRefNavigation { get; set; }

    public virtual ICollection<VisitService> VisitServices { get; set; } = new List<VisitService>();

    public virtual ICollection<StandardService> Services { get; set; } = new List<StandardService>();
}
