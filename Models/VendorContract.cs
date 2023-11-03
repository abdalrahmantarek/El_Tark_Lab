using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class VendorContract
{
    public int ContractId { get; set; }

    public string ContractName { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int VendorId { get; set; }

    public string? ExcutedBy { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string LastUpdateFrom { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public int BranchId { get; set; }

    public int? ContractType { get; set; }

    public double? ContractValue { get; set; }

    public double? IncreasePer { get; set; }

    public int? CurrencyId { get; set; }

    public virtual VendorContractType? ContractTypeNavigation { get; set; }

    public virtual Currency? Currency { get; set; }

    public virtual ICollection<ServicesPricelist> ServicesPricelists { get; set; } = new List<ServicesPricelist>();

    public virtual Vendor Vendor { get; set; } = null!;
}
