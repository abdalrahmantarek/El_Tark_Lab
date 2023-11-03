using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class RefferedDoctor
{
    public int DrId { get; set; }

    public string? DoctorName { get; set; }

    public int? DoctorSpecialization { get; set; }

    public string? PhoneNumber { get; set; }

    public string? ClinicAddress { get; set; }

    public int? DoctorRules { get; set; }

    public int? EmployeeId { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string LastUpdateFrom { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
