﻿using BookStore.Core.Contexts.EmployeeContext.ValueObjects;
using BookStore.Core.Contexts.SharedContext.Entities;
using BookStore.Core.Contexts.SharedContext.ValueObjects;

namespace BookStore.Core.Contexts.EmployeeContext.Entities;

public class Employee : Entity
{
    protected Employee() { }
    public Employee(Name name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public Employee(Name name, string email, string? password = null)
    {
        Name = name;
        Email = email;
        Password = new Password(password);
    }

    public Name Name { get; private set; } = null!;
    public DateTime BirthDate { get; private set; }
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public List<Role> Roles { get; set; } = new();

    public void ChangeName(string firstName,  string lastName)
    {
        Name.FirstName = firstName;
        Name.LastName = lastName;
    }

    public void UpdateEmail(Email email) => Email = email;

    public void ChangePassword(string plainTextPassword) => Password = new Password(plainTextPassword);

    public void AddRole(Role role) => Roles.Add(role);
    public void RemoveRole(Role role) => Roles.Remove(role);
}
