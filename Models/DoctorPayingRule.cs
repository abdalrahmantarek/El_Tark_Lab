using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class DoctorPayingRule
{
    public int RuleId { get; set; }

    public string DocContractId { get; set; } = null!;

    public int DocId { get; set; }

    public int Serial { get; set; }

    public int? ServiceId { get; set; }

    public int? CategoryId { get; set; }

    public string Type { get; set; } = null!;

    public double Count { get; set; }

    public int Repeatation { get; set; }

    public string RuleStatus { get; set; } = null!;

    public bool ReferredDoc { get; set; }

    public int Range { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string LastUpdateFrom { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }
}
