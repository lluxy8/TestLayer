﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Application.Helpers.HttpResponseExceptions
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public CustomException(HttpStatusCode statusCode, string message): base(message)
        {
            StatusCode = statusCode;
        }
    }
}
