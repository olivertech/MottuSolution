using Microsoft.AspNetCore.Http;
using Mottu.CrossCutting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.CrossCutting.Services
{
    public static class ServiceResponse<T>
    {
        public static EnumStatusCode StatusCode { get; set; }
        public static string? Message { get; set; }
        public static T? Response { get; set; }
    }
}
