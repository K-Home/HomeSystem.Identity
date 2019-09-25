﻿using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Messages.Commands;
using FinanceControl.Services.Users.Infrastructure;
using FinanceControl.Services.Users.Infrastructure.MediatR.Bus;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Services.Users.Api.Controllers
{
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IMediatRBus mediatRBus, AppOptions settings) : base(mediatRBus, settings)
        {
        }
        
        [HttpPost]
        [Route("sign-out")]
        public async Task<IActionResult> Post([FromBody] SignOutCommand command)
        {
            return await SendAsync(command, "authentication/sign-out");
        }
    }
}