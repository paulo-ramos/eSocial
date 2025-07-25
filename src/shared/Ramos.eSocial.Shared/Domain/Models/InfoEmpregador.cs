namespace Ramos.eSocial.Shared.Domain.Models;

public class InfoEmpregador
{
    public Inclusao? Inclusao { get; set; }
    public Alteracao? Alteracao { get; set; }
    public Exclusao? Exclusao { get; set; }
    
    public IdePeriodo? GetVigencia() =>
        Inclusao?.IdePeriodo ?? Alteracao?.IdePeriodo ?? Exclusao?.IdePeriodo;

    public void FecharVigencia(DateTime fimValid)
    {
        var periodo = GetVigencia();
        if (periodo is null) return;

        if (Inclusao is not null)
            Inclusao.IdePeriodo.SetFimValidade(fimValid);
        else if (Alteracao is not null)
            Alteracao.IdePeriodo.SetFimValidade(fimValid);
        else if (Exclusao is not null)
            Exclusao.IdePeriodo.SetFimValidade(fimValid);
    }
}