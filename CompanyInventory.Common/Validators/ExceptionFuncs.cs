using System;
using System.Net;
using LaYumba.Functional;
using LaYumba.Functional.Option;

namespace CompanyInventory.Common.Validators
{
    public static class ExceptionFuncs
    {
        public static void IsNull(Option<object> obj, string message, HttpStatusCode statusCode)
        {
            obj.Match(() => throw new Exception($"{(int)statusCode};{message}"), value => new None());
        }
    }
}