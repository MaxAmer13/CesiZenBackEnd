using System;
using CesiZenBackEnd.Core.Entities;    
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CesiZenBackEnd.Core.Entities;

[Table("page_information")]
public partial class PageInformation: Entity<int>
{
    
    [Column("titre")]
    public string Titre { get; set; } = string.Empty;
    
    [Column("contenu")]
    public string Contenu { get; set; } = string.Empty;
}