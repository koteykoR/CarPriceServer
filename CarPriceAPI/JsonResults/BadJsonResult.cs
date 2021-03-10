using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPriceAPI.JsonResults
{
    internal sealed record Error(int Id, string Message);

    internal sealed class BadJsonResult
    {
        //private const 
    }
}
