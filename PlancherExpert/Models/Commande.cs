using System;
using System.Collections.Generic;

namespace PlancherExpert.Models;

public partial class Commande
{
    public int Id { get; set; }

    public string? TypePlancher { get; set; }

    public double? Superficie { get; set; }

    public double? PrixMat { get; set; }

    public double? PrixMainOeuvre { get; set; }

    public int? IdClient { get; set; }

    public virtual Client? IdClientNavigation { get; set; }
}
