using SimpleTrader.Domain.Exceptions;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private LoginViewModel loginViewModel;
        private readonly IAuthenticator authenticator;
        private readonly IRenavigator renavigator;

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            this.authenticator = authenticator;
            this.loginViewModel = loginViewModel;
            this.renavigator = renavigator;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            loginViewModel.ErrorMessage = string.Empty;
            try
            {
                string username = loginViewModel.Username;
                string password = parameter?.ToString() ?? "";

                await authenticator.Login(username, password);
                renavigator.Renavigate();
            }
            catch(UserNotFoundException)
            {
                loginViewModel.ErrorMessage = "Username does not exist";
            }
            catch (InvalidPasswordException)
            {
                loginViewModel.ErrorMessage = "Password is incorrect";
            }
            catch (Exception)
            {
                loginViewModel.ErrorMessage = "Login failed";
            }
        }
    }
}
