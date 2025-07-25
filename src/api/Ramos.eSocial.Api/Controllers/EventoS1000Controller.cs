using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Application.Dtos;
using Ramos.eSocial.S1000.Application.Handlers;

namespace Ramos.eSocial.S1000.Api.Controllers;

[ApiController]
[Route("api/eventos/s1000")]
public class EventoS1000Controller : ControllerBase
{
    private readonly IncluirEventoS1000Handler _incluirHandler;
    private readonly AlterarEventoS1000Handler _alterarHandler;
    private readonly ExcluirEventoS1000Handler _excluirHandler;

    public EventoS1000Controller(
        IncluirEventoS1000Handler incluirHandler, 
        AlterarEventoS1000Handler alterarHandler, 
        ExcluirEventoS1000Handler excluirHandler)
    {
        _incluirHandler = incluirHandler;
        _alterarHandler = alterarHandler;
        _excluirHandler = excluirHandler;
    }

    [HttpPost("incluir")]
    public async Task<IActionResult> Incluir([FromBody] EventoS1000InclusaoDto dto)
    {
        try
        {
            var comando = new IncluirEventoS1000Command(dto);
            var resultado = await _incluirHandler.HandleAsync(comando);
            return Ok(resultado);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Erros = ex.Errors.Select(e => e.ErrorMessage) });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Erro = ex.Message });
        }
    }
    
    [HttpPost("alterar")]
    public async Task<IActionResult> Alterar([FromBody] EventoS1000AlteracaoDto dto)
    {
        try
        {
            var comando = new AlterarEventoS1000Command(dto);
            var resultado = await _alterarHandler.HandleAsync(comando);
            return Ok(resultado);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Erros = ex.Errors.Select(e => e.ErrorMessage) });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Erro = ex.Message });
        }
    }
    
    [HttpPost("excluir")]
    public async Task<IActionResult> Excluir([FromBody] EventoS1000ExclusaoDto dto)
    {
        try
        {
            var comando = new ExcluirEventoS1000Command(dto);
            var resultado = await _excluirHandler.HandleAsync(comando);
            return Ok(resultado);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Erros = ex.Errors.Select(e => e.ErrorMessage) });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Erro = ex.Message });
        }
    }

}