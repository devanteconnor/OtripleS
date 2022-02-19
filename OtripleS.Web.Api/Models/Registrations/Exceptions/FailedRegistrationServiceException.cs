﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Web.Api.Models.Registrations.Exceptions
{
    public class FailedRegistrationServiceException : Xeption
    {
        public FailedRegistrationServiceException(Exception innerException)
            : base(message: "Failed Registration service error occurred, contact support.", innerException)
        {
        }
    }
}
