namespace Ramos.eSocial.Shared.Util.Interface;

public interface IEventIdGenerator
{
    string Gerar(string numeroInscricao, string codigoEvento);
    
}