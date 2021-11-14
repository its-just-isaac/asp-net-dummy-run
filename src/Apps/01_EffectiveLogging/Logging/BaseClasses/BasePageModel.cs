﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Logging.BaseClasses
{
    public class BasePageModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IScopeInformation _scopeInfo;
        private readonly Stopwatch _timer;
        private IDisposable _userScope;
        private IDisposable _hostScope;

        public BasePageModel(ILogger logger, IScopeInformation scopeInfo)
        {
            _logger = logger;
            _scopeInfo = scopeInfo;
            _timer = new Stopwatch();
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var userDict = new Dictionary<string, string>
            {
                { "UserId", context.HttpContext.User.FindFirst("sub")?.Value },
                { "GivenName", context.HttpContext.User.FindFirst("given_name")?.Value },
                { "Email", MaskEmailAddress(context.HttpContext.User.FindFirst("email")?.Value) }
            };

            _userScope = _logger.BeginScope(userDict);
            _hostScope = _logger.BeginScope(_scopeInfo.HostScopeInfo);

            _timer.Start();
        }

        private static string MaskEmailAddress(string emailAddress)
        {
            int? atIndex = emailAddress?.IndexOf('@');
            return atIndex > 1 ? $"{emailAddress[0]}{emailAddress[1]}***{emailAddress[atIndex.Value..]}" : emailAddress;
        }

        public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            _timer.Stop();
            _logger.LogRoutePerformance(context.ActionDescriptor.RelativePath,
                context.HttpContext.Request.Method,
                _timer.ElapsedMilliseconds);

            _userScope?.Dispose();
            _hostScope?.Dispose();
        }
    }
}