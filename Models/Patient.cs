using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public int? PatientNidType { get; set; }

    public long? PatientNid { get; set; }

    public string PatientName { get; set; } = null!;

    public string? PatientAddress { get; set; }

    public string? PatientTel1 { get; set; }

    public string? PatientTel2 { get; set; }

    public string? PatientEmail { get; set; }

    public string? PatientGender { get; set; }

    public DateTime CreationDate { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? PatientBirthdate { get; set; }

    public string? PatientCountry { get; set; }

    public string? LastUpdateBy { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public string? LastUpdateFrom { get; set; }

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
