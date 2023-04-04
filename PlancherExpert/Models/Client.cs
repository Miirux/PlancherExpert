using System;
using System.Collections.Generic;

namespace PlancherExpert.Models;

public partial class Client
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public string? Email { get; set; }

    public string? Adresse { get; set; }

    public string? Tel { get; set; }

    public virtual ICollection<Commande> Commandes { get; } = new List<Commande>();
}
