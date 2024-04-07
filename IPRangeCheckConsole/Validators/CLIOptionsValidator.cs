﻿using FluentValidation;
using IPRangeCheckConsole.Misc;

namespace IPRangeCheckConsole.Validators
{
    public class CLIOptionsValidator : AbstractValidator<CLIOptions>
    {
        public CLIOptionsValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(opts => opts.FileLog).NotEmpty().ValidateFilePath().NotEqual(opts => opts.FileOutput);

            RuleFor(opts => opts.FileOutput).NotEmpty().ValidateDirectoryPath().NotEqual(opts => opts.FileLog);

            RuleFor(opts => opts.AddressStart).ValidateIPAddress().When(opts => !string.IsNullOrEmpty(opts.AddressStart));

            RuleFor(opts => opts.AddressMask).ValidateIPAddress().When(opts => !string.IsNullOrEmpty(opts.AddressMask));

            RuleFor(opts => opts.TimeStart).NotEmpty().ValidateDateTime().LessThanOrEqualTo(opts => opts.TimeEnd);

            RuleFor(opts => opts.TimeEnd).NotEmpty().ValidateDateTime().GreaterThanOrEqualTo(opts => opts.TimeStart);
        }
    }
}
