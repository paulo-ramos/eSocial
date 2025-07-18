using System.Security.Cryptography;
using System.Text;
using Ramos.eSocial.Shared.Util.Interface;

namespace Ramos.eSocial.Shared.Util;

public class EventIdGenerator : IEventIdGenerator
{
    public string Gerar(string numeroInscricao, string codigoEvento)
    {
        if (string.IsNullOrWhiteSpace(numeroInscricao))
            throw new ArgumentException("Número de inscrição inválido.", nameof(numeroInscricao));

        if (string.IsNullOrWhiteSpace(codigoEvento))
            throw new ArgumentException("Código do evento inválido.", nameof(codigoEvento));

        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");

        var raw = $"{numeroInscricao}{codigoEvento}{timestamp}";

        return raw.Length >= 36
            ? raw.Substring(0, 36)
            : raw.PadLeft(36, '0');


    }
}