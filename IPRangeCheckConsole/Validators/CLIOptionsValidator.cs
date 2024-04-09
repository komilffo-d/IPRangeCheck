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
                                        .ValidateFilePath()
                                        .NotEqual(opts => opts.FileOutput)
                                        .WithMessage("Свойство '{PropertyName}' не должно быть равна выходному пути.");

            RuleFor(opts => opts.FileOutput).NotEmpty()
                                            .WithMessage("Назначьте свойству '{PropertyName}' значение.")
                                            .ValidateDirectoryPath()
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
                                            .Must((obj, value) =>
                                            {

                                                return DateOnly.Parse(value) <= DateOnly.Parse(obj.TimeEnd);
                                            }).WithMessage("Свойство '{PropertyName}' должно быть меньше.")
                                            .When(opts => !string.IsNullOrEmpty(opts.TimeEnd), ApplyConditionTo.CurrentValidator);

            RuleFor(opts => opts.TimeEnd).NotEmpty()
                                            .WithMessage("Назначьте свойству '{PropertyName}' значение.")
                                            .ValidateDateOnly()
                                            .Must((obj, value) =>
                                            {

                                                return DateOnly.Parse(value) >= DateOnly.Parse(obj.TimeStart);
                                            })
                                            .WithMessage("Свойство '{PropertyName}' должно быть больше.")
                                            .When(opts => !string.IsNullOrEmpty(opts.TimeStart), ApplyConditionTo.CurrentValidator);

        }
    }
}
