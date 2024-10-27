using System.Text.Json.Serialization;
using Ramos.eSocial.S1000.Application.Validators;
using Ramos.eSocial.S1000.Domain.Entities;
using Ramos.eSocial.S1000.Domain.ValueObjects;
using Ramos.eSocial.S1000.Shared.Commands;

namespace Ramos.eSocial.S1000.Application.Commands;

public class FetchDadosEmpregadorCommand : ICommand
{
    public FetchDadosEmpregadorCommand(string nrInsc)
    {
        NrInsc = nrInsc;
    }

    public string NrInsc { get; set; }

}