using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPriceAPI.BadJsonResults
{
    public enum ErrorId
    {
        UnknownError = 0,
        UserNotFound = 1,
        CarWasNull = 2
    };

    public sealed class BadJsonResultBuilder
    {
        private static readonly Dictionary<int, string> _errors = new()
        {
            { 0, "Unknown error" },
            { 1, "User not found" },
            { 2, "Cars was null" }
        };

        public static JsonResult BuildBadJsonResult(ErrorId errorId)
        {
            if (!_errors.TryGetValue((int)errorId, out var message)) throw new ArgumentException("Does not exist", nameof(errorId));

            return new BadJsonResult(new Error((int)errorId, message));
        }

        record Error(int Id, string Message);

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
