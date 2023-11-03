using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class PatientInvoice
{
    public int InvoiceId { get; set; }

    public DateTime CreationDate { get; set; }

    public string? InvoiceNotes { get; set; }

    public double InvoiceCost { get; set; }

    public string Currency { get; set; } = null!;

    public string PaymentType { get; set; } = null!;

    public int VisitId { get; set; }

    public string InvoiceStatus { get; set; } = null!;

    public string LastUpdateBy { get; set; } = null!;

    public string LastUpdateFrom { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public double? PaidValue { get; set; }

    public bool IsSupply { get; set; }

    public virtual ICollection<InvoicesPaying> InvoicesPayings { get; set; } = new List<InvoicesPaying>();

    public virtual ICollection<PatientInvoiceDetail> PatientInvoiceDetails { get; set; } = new List<PatientInvoiceDetail>();

    public virtual Visit Visit { get; set; } = null!;
}
