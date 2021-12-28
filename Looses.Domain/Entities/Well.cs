namespace Looses.Domain.Entities;

public class Well
{
    public Well(int id, string name)
    {
        Id = id;
        Name = name;
        Looses = new List<Looses>();
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public ICollection<Looses> Looses { get; private set; }
}