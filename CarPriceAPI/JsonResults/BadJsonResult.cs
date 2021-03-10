using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPriceAPI.JsonResults
{
    internal sealed record Error(int Id, string Message);

    public sealed class BadJsonResultBuilder
    {
        private static readonly Dictionary<int, string> _errors = new()
        {
            { 1, "User not found" },
            { 2, "Cars was null" }
        };

        public static bool TryBuildBadJsonResult(int errorId, out JsonResult badJsonResult)
        {
            badJsonResult = null;
            badJsonResult.StatusCode = 400;

            if (!_errors.TryGetValue(errorId, out var message)) return false;

            badJsonResult = new(new Error(errorId, message));

            return true;
        }
    }
}
