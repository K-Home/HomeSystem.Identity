﻿using HomeSystem.Services.Identity.Application.Dtos;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using System;

namespace HomeSystem.Services.Identity.Application.Messages.Queries
{
    public class GetUserSession : IQuery<UserSessionDto>
    {
        public Guid Id { get; set; }
    }
}
