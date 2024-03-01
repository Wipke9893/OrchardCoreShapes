using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Users;
using OrchardCore.Users.Events;
using System.Threading.Tasks;

namespace OrchardCore.Module.Training.Events;

public class LoginGreeting : ILoginFormEvent
{
    private readonly INotifier _notifier;
    private readonly IHtmlLocalizer T;

    public LoginGreeting(INotifier notifier, IHtmlLocalizer<LoginGreeting> localizer)
    {
        _notifier = notifier;
        T = localizer;
    }


    public Task IsLockedOutAsync(IUser user)
    {
        // Notify that the user account is locked.
        _notifier.AddAsync(NotifyType.Warning, T["Your account is locked. Contact an administrator."]);
        return Task.CompletedTask;
    }


    public Task LoggedInAsync(IUser user)
    {
        // Welcome message upon successful login.
        _notifier.AddAsync(NotifyType.Success, T["Welcome back, {0}! Happy to see you again my Great Lord.", user.UserName]);
        return Task.CompletedTask;
    }


    public Task LoggingInAsync(string userName, Action<string, string> reportError)
    {
        // pre-login attempt
        return Task.CompletedTask;
    }


    public Task LoggingInFailedAsync(IUser user)
    {
        // failed login attempt.
        _notifier.AddAsync(NotifyType.Error, T["Login attempt failed for {0}. Please check your credentials my great Master", user.UserName]);
        return Task.CompletedTask;
    }


    public Task LoggingInFailedAsync(string userName)
    {
        // failed login attempt when the username is known, but not tied to a user.
        _notifier.AddAsync(NotifyType.Error, T["Login attempt failed for username: {0}. Please check your credentials and try again my Great Dog Friend.", userName]);
        return Task.CompletedTask;
    }

}
