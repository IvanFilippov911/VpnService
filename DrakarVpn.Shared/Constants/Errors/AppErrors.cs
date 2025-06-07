

using System.Net;

namespace DrakarVpn.Shared.Constants.Errors;

public static class AppErrors
{
    public static readonly AppError UserAlreadyExists =
        new AppError("USER_ALREADY_EXISTS", "Пользователь уже существует", HttpStatusCode.BadRequest);

    public static readonly AppError LoginError =
        new AppError("LOGIN_ERROR", "Ошибка входа", HttpStatusCode.BadRequest);

    public static readonly AppError RegisterError =
        new AppError("REGISTER_ERROR", "Ошибка регистрации", HttpStatusCode.BadRequest);

    public static readonly AppError InvalidModel =
        new AppError("INVALID_MODEL", "Модель данных не подходит", HttpStatusCode.BadRequest);

    public static readonly AppError ObjectIsNull =
        new AppError("OBJECT_IS_NULL", "Объект не существует", HttpStatusCode.BadRequest);

    public static AppError Exception(string ex) =>
        new AppError("EXCEPTION", $"Что-то поломалось: {ex}", HttpStatusCode.BadRequest);
}
