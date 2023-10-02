using FluentValidation;
using TesteTecnicoIdealSoft.API.Interfaces.Settings;

namespace TesteTecnicoIdealSoft.API.Services.BaseServices;

public abstract class BaseService<TEntity>
    where TEntity : class
{
    protected readonly INotificationHandler _notificationHandler;
    protected readonly IValidator<TEntity> _validator;

    public BaseService(INotificationHandler notificationHandler, IValidator<TEntity> validator)
    {
        _notificationHandler = notificationHandler;
        _validator = validator;
    }

    protected async Task<bool> ValidateAsync(TEntity entity)
    {
        var validationResult = await _validator.ValidateAsync(entity);

        if (!validationResult.IsValid)
        {
            foreach(var error in validationResult.Errors) 
            {
                _notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);
            }

            return false;
        }

        return true;
    }
}
