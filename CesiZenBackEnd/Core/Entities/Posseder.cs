using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CesiZenBackEnd.Core.Entities;

[Table("posseder")]
public partial class Posseder : Entity<int>
{
    public int IdDiagnostic { get; set; }
    public Diagnostic Diagnostic { get; set; }

    public int IdEvenement { get; set; }
    public EvenementStress EvenementStress { get; set; }
}