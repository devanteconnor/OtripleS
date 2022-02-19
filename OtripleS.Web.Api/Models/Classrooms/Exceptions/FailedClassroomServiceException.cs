﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Web.Api.Models.Classrooms.Exceptions
{
    public class FailedClassroomServiceException : Xeption
    {
        public FailedClassroomServiceException(Exception innerException)
            : base(message: "Failed classroom service error occured, contact support.",
                 innerException)
        { }
    }
}
