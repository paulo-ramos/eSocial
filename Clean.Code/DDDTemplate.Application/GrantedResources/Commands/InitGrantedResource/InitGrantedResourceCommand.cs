using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Application.Attributes;

namespace DDDTemplate.Application.GrantedResources.Commands.InitGrantedResource;

[InitDataCommand(1)]
public class InitGrantedResourceCommand : ICommand;