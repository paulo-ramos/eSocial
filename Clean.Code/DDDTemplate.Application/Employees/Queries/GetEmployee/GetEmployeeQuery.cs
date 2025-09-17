using DDDTemplate.Application.Abstractions.Messaging;

namespace DDDTemplate.Application.Employees.Queries.GetEmployee;

public record GetEmployeeQuery(Guid Id) : IQuery<GetEmployeeQueryResult>;