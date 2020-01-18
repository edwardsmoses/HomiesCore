using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class CurrentUser
    {
        public class Query : IRequest<User> { }

        public class Handler : IRequestHandler<Query, User>
        {
            private readonly UserManager<AppUser> userManager;
            private readonly IJWTGenerator jWTGenerator;
            private readonly IUserAccessor userAccessor;

            public Handler(UserManager<AppUser> userManager, IJWTGenerator jWTGenerator, IUserAccessor userAccessor)
            {
                this.userManager = userManager;
                this.jWTGenerator = jWTGenerator;
                this.userAccessor = userAccessor;
            }

            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {

                var user = await userManager.FindByNameAsync(userAccessor.GetCurrentUsername());
                return new User
                {
                    DisplayName = user.DisplayName,
                    UserName = user.UserName,
                    Token = jWTGenerator.CreateToken(user),
                    Image = string.Empty
                };
            }
        }
    }
}
