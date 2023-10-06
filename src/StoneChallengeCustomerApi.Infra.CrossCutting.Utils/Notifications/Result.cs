﻿using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Extensions;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Enums;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;

public static class Result
{
    public static IOperation CreateSuccess()
        => new SuccessOperation();
    
    public static IOperation<T> CreateSuccess<T>(T data)
        => new SuccessOperation<T>(data);

    public static IOperation CreateFailure(EErrorType type, ResultError error)
        => new FailedOperation(error, type.GetDescription());

    public static IOperation CreateFailure(EErrorType type, IEnumerable<ResultErrorField> fields)
        => new FailedOperation(type, fields, type.GetDescription());

    // ==================

    public static IOperation<T> CreateFailure<T>(EErrorType type, IEnumerable<ResultErrorField> fields)
        => new FailedOperation<T>(type, fields, type.GetDescription());
    
    public static IOperation<T> CreateFailure<T>(EErrorType type)
        => new FailedOperation<T>(type, type.GetDescription());
}