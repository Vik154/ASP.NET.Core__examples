﻿namespace _11_Entity_Framework.Models;

public class User {
    public int Id { get; set; }
    public int Age { get; set; }
    public int? CompanyId { get; set; }
    public string? Name { get; set; }
    public Company? company { get; set; }
}
