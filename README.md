# 🧭 Guia Interno de Arquitetura — Projeto eSocial Modular

Este guia define padrões e boas práticas para manter a arquitetura do projeto organizada, escalável e fácil de manter.

---

## 📦 Estrutura de Módulos

Cada evento (ex: S1000, S1020) é um módulo independente com a seguinte estrutura:

```
src/
├── Ramos.eSocial.S1000/
│   ├── Application/
│   ├── Domain/
│   ├── Infrastructure/
│   ├── IoC/
│   └── ...
├── Ramos.eSocial.S1020/
│   └── ...
├── Ramos.eSocial.Shared/
│   ├── Domain/
│   ├── Util/
│   ├── IoC/
│   └── ...
```

---

## 🧩 Módulo Shared
O módulo Ramos.eSocial.Shared contém:
- Modelos de domínio genéricos (ex: IdeEmpregador, Periodo)
- Validadores reutilizáveis (ex: CnpjValidator, DateValidator)
- Helpers utilitários (ex: CnpjHelper, XmlHelper)
- Serviços transversais (ex: IEventIdGenerator)
- IoC próprio para registrar dependências compartilhadas

## 📦 Estrutura
```
Ramos.eSocial.Shared/
├── Domain/
│   └── Validation/
├── Util/
│   └── Helpers/
├── IoC/
│   └── DependencyInjection.cs
```

## ✅ Registro no IoC
```csharp
services.AddSharedServices();
```


## 📘 Convenções (atualizado)
| Item | Padrão |
| --- | --- |
| DTOs | record class, init, no módulo |
| Validadores | FluentValidation, genéricos no Shared |
| Helpers | Estáticos, em Shared.Util |
| IoC | Por módulo + centralizador na API |
| Shared | Contém utilitários, validadores e serviços |
| Handlers | Validam domínio, não DTO |
| Repositórios | Interfaces no módulo, implementação no Infrastructure |




## 🧱 DTOs

- Devem ficar dentro de `Application/Dtos` do módulo
- Usar `record class` com propriedades `init` para imutabilidade e suporte ao operador `with`
- Representam contratos de entrada e saída, não lógica de negócio

```csharp
public record class IdeEmpregadorDto
{
    public int TpInsc { get; init; }
    public string NrInsc { get; init; } = string.Empty;
}
```

## ✅ Validação
- Validação básica no CommandValidator (ex: campos obrigatórios)
- Validação de regras de negócio no modelo de domínio, usando FluentValidation
- Validadores genéricos (ex: CnpjValidator) ficam em Shared.Domain.Validation

```csharp
RuleFor(x => x.EventoDto.IdeEmpregador.NrInsc)
    .SetValidator(new CnpjValidator());
```


## 🔄 Mapeamento
- Usar mappers para transformar DTO → Domínio e vice-versa
- Separar em Application/Mappers
- Evita lógica de transformação espalhada

## 🧩 Inversão de Controle (IoC)
- Cada módulo tem seu próprio DependencyInjection.cs
- A API centraliza tudo com AddModules() no Ramos.eSocial.Api.IoC
- O Shared também tem seu IoC para utilitários e serviços comuns

```csharp
builder.Services.AddModules();
```


## 🛠️ Utilitários
- Helpers como CnpjHelper, DateHelper, XmlHelper ficam em Shared.Util
- Devem ser estáticos, puros e testáveis
- Evitam duplicação de lógica comum

```csharp
var formatado = CnpjHelper.Format("12345678000195");
```


## 🧪 Testes
- Testes unitários por módulo
- Testar handlers, mappers, validadores e helpers
- Usar xUnit + Moq ou NSubstitute

## 📘 Convenções

| Item | Padrão | 
| --- | --- |
| DTOs | record class, init, no módulo | 
| Validadores | FluentValidation, genéricos no Shared | 
| Helpers | Estáticos, em Shared.Util | 
| IoC | Por módulo + centralizador na API | 
| Handlers | Validam domínio, não DTO | 
| Repositórios | Interfaces no módulo, implementação no Infrastructure | 



## 🚀 Futuro
- Adicionar AlterarEventoSXXXXHandler e ExcluirEventoSXXXXHandler
- Criar XmlMapper para gerar XML conforme layout eSocial
- Adicionar testes de integração com banco ou simulador
- Evoluir o Shared com validadores e helpers reutilizáveis


Este guia é vivo. À medida que o projeto cresce, novas práticas e padrões serão incorporados.


