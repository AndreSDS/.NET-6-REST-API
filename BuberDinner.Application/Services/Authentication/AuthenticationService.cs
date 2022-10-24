using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using FluentResults;
using OneOf;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);

            // validate the user does not exist
            if (user is not null)
            {
                return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
            }

            // create a user with unique id and persist to database
            var newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepository.Add(newUser);

            // generate JWT token
            var token = _jwtTokenGenerator.GenerateToken(newUser);

            return new AuthenticationResult(
                newUser,
                token
            );
        }

        public AuthenticationResult Login(string email, string password)
        {
            // validate the user exists
            var user = _userRepository.GetUserByEmail(email);

            if (user is not User)
            {
                throw new Exception("User with given email does not exist");
            }

            // validate the password is correct
            if (user?.Password != password)
            {
                throw new Exception("Password is incorrect");
            }

            // generate JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token
            );
        }
    }
}