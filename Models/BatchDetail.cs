using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class BatchDetail
{
    public int Id { get; set; }

    public int BatchId { get; set; }

    public int VisitId { get; set; }

    public virtual Batsh Batch { get; set; } = null!;

    public virtual Visit Visit { get; set; } = null!;
}
