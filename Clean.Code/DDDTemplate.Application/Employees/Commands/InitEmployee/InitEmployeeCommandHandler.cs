using System.Text;
using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Domain.Employees;
using DDDTemplate.SharedKernel.Results;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DDDTemplate.Application.Employees.Commands.InitEmployee;

public class InitEmployeeCommandHandler(
    ILogger<InitEmployeeCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IEmployeeRepository repository
) : ICommandHandler<InitEmployeeCommand>
{
    public async Task<Result> Handle(InitEmployeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Databases",
                "Employees.json");
            if (!File.Exists(path))
                return Result.Failure(EmployeeErrors.NotFound);

            var records = JsonConvert.DeserializeObject<Employee[]>(
                await File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken));
            if (records is null || records.Length == 0)
                return Result.Success();

            foreach (var record in records)
            {
                var entity = record.IdUser == null
                    ? await repository.GetByIdAsync(record.Id, cancellationToken)
                    : await repository.GetByIdUserAsync((Guid)record.IdUser, cancellationToken);
                
                if (entity is null)
                    repository.Insert(record);
                else
                    entity.Update(
                        record.Code,
                        record.AvatarUrl,
                        record.Fullname,
                        record.EmailAddress,
                        record.MobileNumber,
                        record.Address,
                        record.IsBlocked,
                        record.IdUser
                    );
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }

        return Result.Failure(EmployeeErrors.ErrorInProcessing);
    }
}