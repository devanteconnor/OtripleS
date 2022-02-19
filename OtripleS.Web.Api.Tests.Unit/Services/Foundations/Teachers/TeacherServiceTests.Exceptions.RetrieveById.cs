﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using OtripleS.Web.Api.Models.Teachers;
using OtripleS.Web.Api.Models.Teachers.Exceptions;
using Xunit;

namespace OtripleS.Web.Api.Tests.Unit.Services.Foundations.Teachers
{
    public partial class TeacherServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveWhenSqlExceptionOccursAndLogItAsync()
        {
            // given
            Guid someTeacherId = Guid.NewGuid();
            SqlException sqlException = GetSqlException();

            var expectedTeacherDependencyException =
                new TeacherDependencyException(sqlException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectTeacherByIdAsync(someTeacherId))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Teacher> retrieveTeacherTask =
                this.teacherService.RetrieveTeacherByIdAsync(someTeacherId);

            // then
            await Assert.ThrowsAsync<TeacherDependencyException>(() =>
                retrieveTeacherTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedTeacherDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectTeacherByIdAsync(someTeacherId),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveWhenDbExceptionOccursAndLogItAsync()
        {
            // given
            Guid someTeacherId = Guid.NewGuid();
            var databaseUpdateException = new DbUpdateException();

            var expectedTeacherDependencyException =
                new TeacherDependencyException(databaseUpdateException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectTeacherByIdAsync(someTeacherId))
                    .ThrowsAsync(databaseUpdateException);

            // when
            ValueTask<Teacher> retrieveTeacherTask =
                this.teacherService.RetrieveTeacherByIdAsync(someTeacherId);

            // then
            await Assert.ThrowsAsync<TeacherDependencyException>(() =>
                retrieveTeacherTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTeacherDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectTeacherByIdAsync(someTeacherId),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveWhenExceptionOccursAndLogItAsync()
        {
            // given
            Guid someTeacherId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedTeacherServiceException =
                new FailedTeacherServiceException(serviceException);

            var expectedTeacherServiceException =
                new TeacherServiceException(failedTeacherServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectTeacherByIdAsync(someTeacherId))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Teacher> retrieveTeacherTask =
                this.teacherService.RetrieveTeacherByIdAsync(someTeacherId);

            // then
            await Assert.ThrowsAsync<TeacherServiceException>(() =>
                retrieveTeacherTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTeacherServiceException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectTeacherByIdAsync(someTeacherId),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
