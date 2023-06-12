using FluentValidation;

namespace Timekeeper.Application.Common.Validations;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, string?> NullOrNotEmpty<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder.Must(value => value == null || !string.IsNullOrEmpty(value)).WithMessage("Field cannot be empty.");
    }

    public static IRuleBuilderOptions<T, DateTime?> NullOrMustBeValidDate<T>(this IRuleBuilder<T, DateTime?> ruleBuilder)
    {
        return ruleBuilder.Must(value => value == null || IsValidDate(value)).WithMessage("Field must be a valid date.");
    }

    private static bool IsValidDate(DateTime? value)
    {
        return value != null && value.Value > DateTime.MinValue && value.Value < DateTime.MaxValue;
    }
}