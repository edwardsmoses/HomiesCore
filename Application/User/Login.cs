using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class Login
    {
        public class Query : IRequest<User>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(m => m.Email).NotEmpty();
                RuleFor(m => m.Email).EmailAddress();
                RuleFor(m => m.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, User>
        {
            private readonly UserManager<AppUser> userManager;
            private readonly SignInManager<AppUser> signInManager;
            private readonly IJWTGenerator jwtGenerator;

            public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJWTGenerator jwtGenerator)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.jwtGenerator = jwtGenerator;
            }

            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    throw new RestException(System.Net.HttpStatusCode.Unauthorized);

                var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (result.Succeeded)
                {
                    //TO-DO : Generate Token for the User
                    return new User()
                    {
                        DisplayName = user.DisplayName,
                        Image = string.Empty,
                        Token = jwtGenerator.CreateToken(user),
                        UserName = user.UserName
                    };
                }

                throw new RestException(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
