using System.Text;
using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Domain.Users;
using DDDTemplate.SharedKernel.Abstractions.Services;
using DDDTemplate.SharedKernel.Results;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DDDTemplate.Application.Users.Commands.InitUser;

public class InitUserCommandHandler(
    ILogger<InitUserCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository repository,
    IPasswordService passwordService
    ) : ICommandHandler<InitUserCommand>
{
    public async Task<Result> Handle(InitUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Databases",
                "Users.json");
            if (!File.Exists(path))
                return Result.Failure(UserErrors.NotFound);

            var records = JsonConvert.DeserializeObject<User[]>(
                await File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken));
            if (records is null || records.Length == 0)
                return Result.Success();

            foreach (var record in records)
            {
                var passwordResult = Password.Create(record.Password.Value, passwordService, true);
                if (passwordResult.IsFailure)
                    continue;

                var entity = await repository.GetByIdAsync(record.Id, cancellationToken);
                if (entity is null)
                {
                    var temp = User.Create(
                        record.Id,
                        record.Username,
                        passwordResult.Value,
                        record.IsBlocked,
                        record.ExpirationDate,
                        record.UserRole
                    );
                    repository.Insert(temp);
                }
                else
                    entity.Update(
                        record.Username,
                        passwordResult.Value,
                        record.IsBlocked,
                        record.ExpirationDate,
                        record.UserRole
                    );
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }

        return Result.Failure(UserErrors.ErrorInProcessing);
    }
}