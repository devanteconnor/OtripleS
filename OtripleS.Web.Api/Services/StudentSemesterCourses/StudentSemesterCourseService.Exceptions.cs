﻿using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OtripleS.Web.Api.Models.StudentSemesterCourses;
using OtripleS.Web.Api.Models.StudentSemesterCourses.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtripleS.Web.Api.Services.StudentSemesterCourses
{
    public partial class StudentSemesterCourseService
    {
        private delegate ValueTask<StudentSemesterCourse> ReturningStudentSemesterCourseFunction();
        private async ValueTask<StudentSemesterCourse> TryCatch(
            ReturningStudentSemesterCourseFunction returningStudentStudentSemesterCourseFunction)
        {
            try
            {
                return await returningStudentStudentSemesterCourseFunction();
            }
            catch (NullStudentSemesterCourseException nullStudentStudentSemesterCourseException)
            {
                throw CreateAndLogValidationException(nullStudentStudentSemesterCourseException);
            }
            catch (InvalidStudentSemesterCourseException invalidStudentStudentSemesterCourseInputException)
            {
                throw CreateAndLogValidationException(invalidStudentStudentSemesterCourseInputException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsStudentStudentSemesterCourseException =
                    new AlreadyExistsStudentSemesterCourseException(duplicateKeyException);

                throw CreateAndLogValidationException(alreadyExistsStudentStudentSemesterCourseException);
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private StudentSemesterCourseServiceException CreateAndLogServiceException(Exception exception)
        {
            var studentStudentSemesterCourseServiceException = new StudentSemesterCourseServiceException(exception);
            this.loggingBroker.LogError(studentStudentSemesterCourseServiceException);

            return studentStudentSemesterCourseServiceException;
        }

        private StudentSemesterCourseValidationException CreateAndLogValidationException(Exception exception)
        {
            var studentStudentSemesterCourseValidationException = new StudentSemesterCourseValidationException(exception);
            this.loggingBroker.LogError(studentStudentSemesterCourseValidationException);

            return studentStudentSemesterCourseValidationException;
        }

        private StudentSemesterCourseDependencyException CreateAndLogCriticalDependencyException(Exception exception)
        {
            var studentStudentSemesterCourseDependencyException = new StudentSemesterCourseDependencyException(exception);
            this.loggingBroker.LogCritical(studentStudentSemesterCourseDependencyException);

            return studentStudentSemesterCourseDependencyException;
        }

        private StudentSemesterCourseDependencyException CreateAndLogDependencyException(Exception exception)
        {
            var studentStudentSemesterCourseDependencyException = new StudentSemesterCourseDependencyException(exception);
            this.loggingBroker.LogError(studentStudentSemesterCourseDependencyException);

            return studentStudentSemesterCourseDependencyException;
        }
    }
}
