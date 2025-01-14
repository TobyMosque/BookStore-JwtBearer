﻿using BookStore.Core.Contexts.EmployeeContext.Entities;
using BookStore.Core.Contexts.EmployeeContext.UseCases.Create.Contracts;
using BookStore.Core.Contexts.EmployeeContext.ValueObjects;
using BookStore.Core.Contexts.SharedContext.ValueObjects;
using MediatR;

namespace BookStore.Core.Contexts.EmployeeContext.UseCases.Create;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region Request Validation
        try
        {
            var response = Specification.Validate(request);
            if (!response.IsValid)
                return new Response("Invalid Request", 400, response.Notifications);
        }
        catch
        {
            return new Response("Unable to validate request", 500);
        }
        #endregion

        #region Create Object
        Name name;
        Email email;
        Password password;
        Employee employee;

        try
        {
            name = new(request.FirstName, request.LastName);
            email = new(request.Email);
            password = new(request.Password);
            employee = new(name, email, password);            
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }
        #endregion

        #region Verify Employee
        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);
            if (exists)
                return new Response("Email already exists", 400);
        }
        catch
        {
            return new Response("Failed to verify registred email", 500);
        }
        #endregion

        #region Data Persistence
        try
        {
            await _repository.SaveAsync(employee, cancellationToken);
        }
        catch
        {
            return new Response("Failed to persist data", 500);
        }
        #endregion

        #region Response
        return new Response("Employee created successfully", new ResponseData(employee.Id, employee.Name.FirstName, employee.Email));
        #endregion
    }
}
