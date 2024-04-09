using FluentValidation;
using FluentValidation.Results;
using IPRangeCheckConsole.Misc;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace IPRangeCheckConsole.Facade
{
    internal class OptionsSubSystem
    {
        private readonly IValidator<CLIOptions> _validator;

        public OptionsSubSystem(IValidator<CLIOptions> validator)
        {
            _validator = validator;
        }

        public CLIOptions GetCLIOptions(IConfiguration configuration)
        {
            return new CLIOptions
            {
                FileLog = configuration["FILE_LOG"],
                FileOutput = configuration["FILE_OUTPUT"],
                AddressStart = configuration["ADDRESS_START"],
                AddressMask = configuration["ADDRESS_MASK"],
                TimeStart = configuration["TIME_START"],
                TimeEnd = configuration["TIME_END"]
            };
        }

        public ValidationResult ValidateOptions(CLIOptions cliOptions) => _validator.Validate(cliOptions);


        public void LogValidationErrors(IEnumerable<ValidationFailure> errors)
        {
            Log.Error("ПЕРЕДАНЫ НЕ ВСЕ АРГУМЕНТЫ ИЛИ ПЕРЕДАНЫ В НЕВЕРНОМ ФОРМАТЕ:");
            foreach (var error in errors)
                Log.Error($"--> {error.ErrorMessage} <--");
        }
    }
}
