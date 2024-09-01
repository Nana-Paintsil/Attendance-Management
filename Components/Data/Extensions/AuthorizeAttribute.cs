using AttendanceManagement.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;


namespace AttendanceManagement.Extensions
{


    public class AllowAnonymousPolicyRequirement : IAuthorizationRequirement
    {
    }

    public class AllowAnonymousPolicyHandler : AuthorizationHandler<AllowAnonymousPolicyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AllowAnonymousPolicyRequirement requirement)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

}
