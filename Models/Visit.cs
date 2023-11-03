using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class Visit
{
    public int VisitId { get; set; }

    public int? PatientId { get; set; }

    public DateTime? VisitStartDate { get; set; }

    public DateTime? VisitEndDate { get; set; }

    public string? VisitNote { get; set; }

    public bool VisitInsurred { get; set; }

    public int? VisitInsurredCompanyId { get; set; }

    public int? RelativeId { get; set; }

    public string VisitStatus { get; set; } = null!;

    public int BranchId { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string LastUpdateFrom { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public string? InvestigationNotes { get; set; }

    public string VisitType { get; set; } = null!;

    public double? Copayment { get; set; }

    public string? PatientInssurredId { get; set; }

    public string? PatientApprovalNum { get; set; }

    public double? ApprovalMaxLimit { get; set; }

    public DateTime? InssuredExpireDate { get; set; }

    public double? DiscountCard { get; set; }

    public int? RelatedVisit { get; set; }

    public int? RefererDoctor { get; set; }

    public int? VisitSource { get; set; }

    public int? OrderId { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual ICollection<PatientInvoiceDetail> PatientInvoiceDetails { get; set; } = new List<PatientInvoiceDetail>();

    public virtual ICollection<PatientInvoice> PatientInvoices { get; set; } = new List<PatientInvoice>();

    public virtual RefferedDoctor? RefererDoctorNavigation { get; set; }

    public virtual ICollection<ServiceBarcode> ServiceBarcodes { get; set; } = new List<ServiceBarcode>();

    public virtual ICollection<VisitService> VisitServices { get; set; } = new List<VisitService>();
}
