using System;
using System.Collections.Generic;

namespace PlancherExpert.Models;

public partial class Superviseur
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public string? Ville { get; set; }

    public string? Zip { get; set; }

    public string? Tel { get; set; }

    public string? Email { get; set; }

    public string? MotDePasse { get; set; }
}
