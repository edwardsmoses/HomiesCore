using Application.Errors;
using Application.Interfaces;
using Application.Validators;
using DataPersist;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class Register
    {
        public class Command : IRequest<User>
        {
            public string DisplayName { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(m => m.DisplayName).NotEmpty();
                RuleFor(m => m.Username).NotEmpty();
                RuleFor(m => m.Email).NotEmpty().EmailAddress();
                RuleFor(m => m.Password).Password();
            }
        }

        public class Handler : IRequestHandler<Command, User>
        {
            private readonly DataContext context;
            private readonly UserManager<AppUser> userManager;
            private readonly IJWTGenerator jWTGenerator;

            public Handler(DataContext context,
                           UserManager<AppUser> userManager,
                           IJWTGenerator jWTGenerator)
            {
                this.context = context;
                this.userManager = userManager;
                this.jWTGenerator = jWTGenerator;
            }


            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await context.Users.Where(m => m.Email == request.Email).AnyAsync())
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Email = "A User already exists with this email." });
                if (await context.Users.Where(m => m.UserName == request.Username).AnyAsync())
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Username = "A User account with this Username already exists." });

                var user = new AppUser()
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    UserName = request.Username,
                };

                var result = await userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new User()
                    {
                        DisplayName = user.DisplayName,
                        Token = jWTGenerator.CreateToken(user),
                        UserName = user.UserName,
                        Image = string.Empty
                    };
                }

                throw new Exception("Problem creating a New User");

            }
        }

    }
}
