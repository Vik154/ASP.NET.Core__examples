namespace Shop.Data.Model;

public class Car {

    public int    Id { get; set; }
    public string Name { get; set; }
    public string shortDesc          { get; set; }
    public string longDesc           { get; set; }
    public string img                { get; set; }
    public ushort price              { get; set; }
    public bool   isFavourite        { get; set; }
    public bool   available          { get; set; }
    public int    categoryID         { get; set; }
    public virtual Category Category { get; set; }
}
