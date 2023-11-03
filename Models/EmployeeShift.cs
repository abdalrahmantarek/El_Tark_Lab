using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class EmployeeShift
{
    public int ShiftId { get; set; }

    public int EmpId { get; set; }

    public int ShiftDay { get; set; }

    public TimeSpan ShiftStart { get; set; }

    public TimeSpan ShiftEnd { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string LastUpdateFrom { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public int? RoomId { get; set; }

    public int? Floor { get; set; }

    public int? DepId { get; set; }
}
