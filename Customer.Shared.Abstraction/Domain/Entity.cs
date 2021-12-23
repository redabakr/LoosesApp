namespace Customer.Shared.Abstraction.Domain;

public class Entity<TId>
{
    public TId Id { get; protected set; }
}