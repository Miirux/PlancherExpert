using System;
using System.Collections.Generic;

namespace PlancherExpert.Models;

public partial class CouvrePlancher
{
    public int Id { get; set; }

    public string? TypePlancher { get; set; }

    public double? PrixMat { get; set; }

    public double? PrixMainOeuvre { get; set; }

    public int? Promotion { get; set; }
}
