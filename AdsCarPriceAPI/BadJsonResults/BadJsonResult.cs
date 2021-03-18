﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AdsCarPriceAPI.BadJsonResults
{
    public record Error(int Id, string Message);

    public static class Errors
    {
        public static readonly Error UnknownError = new(0, "Unknown error");

        public static readonly Error UserNotFound = new(1, "User not found");

        public static readonly Error CarWasNull = new(2, "Cars was null");
    }

    public sealed class BadJsonResultBuilder
    {
        public static JsonResult BuildBadJsonResult(Error error)
        {
            if (error is null) throw new ArgumentNullException(nameof(error));

            return new BadJsonResult(error);
        }

        class BadJsonResult : JsonResult
        {
            public BadJsonResult(object value) : base(value) { }

            public BadJsonResult(object value, object serializerSettings) : base(value, serializerSettings) { }

            public override void ExecuteResult(ActionContext context)
            {
                context.HttpContext.Response.StatusCode = 400;
                base.ExecuteResult(context);
            }

            public override Task ExecuteResultAsync(ActionContext context)
            {
                context.HttpContext.Response.StatusCode = 400;
                return base.ExecuteResultAsync(context);
            }
        }
    }
}
