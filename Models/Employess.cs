using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class Employess
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string EmployeeNid { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string EmployeeAddress { get; set; } = null!;

    public string? EmployeeEmail { get; set; }

    public string EmployeeTel { get; set; } = null!;

    public string? EmployeeTel2 { get; set; }

    public int DepartId { get; set; }

    public DateTime HiringDate { get; set; }

    public int BranchId { get; set; }
}
