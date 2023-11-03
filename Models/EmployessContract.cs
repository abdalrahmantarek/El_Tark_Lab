using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class EmployessContract
{
    public string ContractId { get; set; } = null!;

    public int EmployeeId { get; set; }

    public int ContractTypeId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int JobId { get; set; }

    public int? JobCat { get; set; }

    public int? SecializationId { get; set; }

    public int? SpecializationCat { get; set; }

    public string? Note { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string LastUpdateFrom { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }
}
