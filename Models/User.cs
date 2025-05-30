﻿namespace Backend.Models;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Department { get; set; }  // renamed from Role
    public string? Password { get; set; }
    public string? Role { get; set; }
}
