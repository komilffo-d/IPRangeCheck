using FluentValidation;
using IPRangeCheckConsole.Misc;

namespace IPRangeCheckConsole.Validators
{
    public class CLIOptionsValidator : AbstractValidator<CLIOptions>
    {
        public CLIOptionsValidator()
        {
            RuleFor(opts => opts.FileLog).NotNull().ValidateFilePathAddress();

            RuleFor(opts => opts.FileOutput).NotNull().ValidateFilePathAddress();

            RuleFor(opts => opts.AddressStart).NotNull().ValidateIPAddress();

            RuleFor(opts => opts.AddressMask).NotNull().ValidateIPAddress();

            RuleFor(opts => opts.TimeStart).NotNull().ValidateDateTime();

            RuleFor(opts => opts.TimeEnd).NotNull().ValidateDateTime();
        }
    }
}
