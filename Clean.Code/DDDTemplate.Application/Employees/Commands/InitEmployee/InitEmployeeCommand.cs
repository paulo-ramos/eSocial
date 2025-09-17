using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Application.Attributes;

namespace DDDTemplate.Application.Employees.Commands.InitEmployee;

[InitDataCommand(1)]
public class InitEmployeeCommand : ICommand;