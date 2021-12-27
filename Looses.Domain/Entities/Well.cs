namespace Looses.Domain.Entities;

public class Well
{
    public Well(string name)
    {
        Name = name;
    }

    public string Id { get; private set; }
    public string Name { get; private set; }
    public ICollection<Looses> Looses { get; private set; }
}