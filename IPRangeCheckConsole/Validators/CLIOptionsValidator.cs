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
                                            .ValidateDirectoryPath().NotEqual(opts => opts.FileLog)
                                            .WithMessage("Свойство '{PropertyName}' не должно быть равна входному пути.");

            RuleFor(opts => opts.AddressStart).ValidateIPAddress().When(opts => !string.IsNullOrEmpty(opts.AddressStart));

            RuleFor(opts => opts.AddressMask).ValidateIPAddress().ValidateMaskIPAddress().When(opts => !string.IsNullOrEmpty(opts.AddressMask));

            RuleFor(opts => opts.TimeStart).NotEmpty()
                                            .WithMessage("Назначьте свойству '{PropertyName}' значение.");

            RuleFor(opts => opts.TimeStart).LessThanOrEqualTo(opts => opts.TimeEnd)
                                            .WithMessage("Свойство '{PropertyName}' должно быть меньше.")
                                            .When(opts => opts.TimeEnd is not null);

            RuleFor(opts => opts.TimeEnd).NotEmpty()
                                            .WithMessage("Назначьте свойству '{PropertyName}' значение");

            RuleFor(opts => opts.TimeEnd).GreaterThanOrEqualTo(opts => opts.TimeStart)
                                        .WithMessage("Свойство '{PropertyName}' должно быть больше.").When(opts => opts.TimeStart is not null);
        }
    }
}
