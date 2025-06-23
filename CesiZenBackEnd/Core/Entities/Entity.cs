namespace CesiZenBackEnd.Core.Entities;

public abstract class Entity<TId> 
{
    public TId Id { get; set; }
}