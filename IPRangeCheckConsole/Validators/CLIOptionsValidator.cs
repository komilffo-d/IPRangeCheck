using FluentValidation;
using IPRangeCheckConsole.Misc;

namespace IPRangeCheckConsole.Validators
{
    public class CLIOptionsValidator : AbstractValidator<CLIOptions>
    {
        public CLIOptionsValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(opts => opts.FileLog).NotEmpty()
                                        .WithMessage("Назначьте свойству '{PropertyName}' значение.")
                                        .ValidateExistFilePath()
                                        .NotEqual(opts => opts.FileOutput)
                                        .WithMessage("Свойство '{PropertyName}' не должно быть равна выходному пути.");

            RuleFor(opts => opts.FileOutput).NotEmpty()
                                            .WithMessage("Назначьте свойству '{PropertyName}' значение.")
                                            .ValidateFilePath()
                                            .NotEqual(opts => opts.FileLog)
                                            .WithMessage("Свойство '{PropertyName}' не должно быть равна входному пути.");

            RuleFor(opts => opts.AddressStart).ValidateIPAddress()
                                            .When(opts => !string.IsNullOrEmpty(opts.AddressStart));

            RuleFor(opts => opts.AddressMask)
                                            .Empty()
                                            .WithMessage("Так как не назначено свойство 'AddressStart' значение для '{PropertyName}' должно отсутствовать.")
                                            .When(opts => opts.AddressStart is null)
                                            .ValidateIPAddress()
                                            .ValidateMaskIPAddress()
                                            .When(opts => !string.IsNullOrEmpty(opts.AddressMask));

            RuleFor(opts => opts.TimeStart).NotEmpty()
                                            .WithMessage("Назначьте свойству '{PropertyName}' значение.")
                                            .ValidateDateOnly()
                                            .Must((opts, value) => DateOnly.Parse(value) <= DateOnly.Parse(opts.TimeEnd))
                                            .WithMessage("Свойство '{PropertyName}' должно быть меньше.")
                                            .When((opts, value) => !string.IsNullOrEmpty(opts.TimeEnd) && DateOnly.TryParseExact(opts.TimeEnd, "dd.MM.yyyy", out DateOnly dateOnly), ApplyConditionTo.CurrentValidator);
            
            RuleFor(opts => opts.TimeEnd).NotEmpty()
                                            .WithMessage("Назначьте свойству '{PropertyName}' значение.")
                                            .ValidateDateOnly()
                                            .Must((opts, value) => DateOnly.Parse(value) >= DateOnly.Parse(opts.TimeStart))
                                            .WithMessage("Свойство '{PropertyName}' должно быть больше.")
                                            .When((opts, value) => !string.IsNullOrEmpty(opts.TimeStart) && DateOnly.TryParseExact(opts.TimeStart, "dd.MM.yyyy", out DateOnly dateOnly), ApplyConditionTo.CurrentValidator);

        }
    }
}
