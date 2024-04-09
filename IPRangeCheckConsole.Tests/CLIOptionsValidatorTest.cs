using FluentValidation;
using FluentValidation.Results;
using IPRangeCheckConsole.Misc;
using IPRangeCheckConsole.Validators;

namespace IPRangeCheckConsole.Tests
{
    public class CLIOptionsValidatorTest
    {
        private CLIOptionsValidator validator;

        public CLIOptionsValidatorTest()
        {
            validator = new CLIOptionsValidator();
        }
        [Fact]
        public void Should_have_error_when_file_log_is_null()
        {
            CLIOptions cLIOptions = new CLIOptions() { FileLog = null };
            var result=validator.Validate(cLIOptions, options =>
            {
                options.IncludeProperties(x => x.FileLog);

            });
            Assert.NotEmpty(result.Errors);
        }

    }
}
