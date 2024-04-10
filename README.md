# Hi, I'm Daniil! 👋
This is software that allows you to extract the number of requests from an IP address in a specified time interval from a log file with an IP address and date time.

## Features
- Getting parameters from 4 sources (Command line arguments, Environment variables, appsettings.json, config.ini)
- All possible validation of the received arguments
- A well-built application architecture that is ready to scale
- Lazy Generation of IP addresses with a given range, count and inclusion
- Lazy Generation of DateTime with a given range, count and inclusion
- Output of work results to a file
- Logging of all operations, errors, and exceptions that occur
- CLIUtility
  - Selecting the application to run
  - Assigning command line arguments
  - Selecting methods for additional parameter sources (Enviroment var., appsettings.json, config.ini)
  - A combination of methods among themselves
  - Validation of input files
- The system is filled with unit tests (xUnit)

## Tech Stack

**Client:** .NET 8 C#, WinForms, xUnit, FluentValidation,N JsonSchema, Newtonsoft.Json,Serilog,  IPAddressPange, CommandLineParser, IHostBuilder Microsoft, Facade/Singleton/Decorator patterns


## Screenshots


![image](https://github.com/komilffo-d/IPRangeCheck/assets/74680206/42d268c2-4371-40c1-9270-0995561fb45a)
<p align="center">
Architecture for obtaining parameters from different sources
</p>

![image](https://github.com/komilffo-d/IPRangeCheck/assets/74680206/d298e0bb-1b74-4994-babb-3299b843a1e4)
<p align="center">
The order of initialization of the system elements (Facade)
</p>

![image](https://github.com/komilffo-d/IPRangeCheck/assets/74680206/db39f8e3-18f7-4d9e-9463-ab5fcaed8673)
<p align="center">
CLIUtility
</p>

![image](https://github.com/komilffo-d/IPRangeCheck/assets/74680206/d612e4c6-aee8-4ad3-95d0-bce334b9d5ca)
<p align="center">
CLIUtility with CMD argument and ENV VAR
</p>


![image](https://github.com/komilffo-d/IPRangeCheck/assets/74680206/c9944b52-b1f4-402c-9c8e-94a56b19df99)
<p align="center">
CLIUtility with CMD argument and ENV VAR and Valid config files
</p>

![image](https://github.com/komilffo-d/IPRangeCheck/assets/74680206/c35909ac-1ee0-4ea8-a923-1014a7099ef2)
<p align="center">
CLIUtility with CMD argument and ENV VAR and NonValid config files
</p>

![image](https://github.com/komilffo-d/IPRangeCheck/assets/74680206/0dc55951-2a0d-4324-86bb-5b5ff3f7c679)
<p align="center">
Input Data
</p>

![image](https://github.com/komilffo-d/IPRangeCheck/assets/74680206/ee7fce9b-97d6-4a15-a51e-b0d0187b2cce)
<p align="center">
Transmission without parameters from all sources
</p>

![image](https://github.com/komilffo-d/IPRangeCheck/assets/74680206/97816c84-f857-4ca4-8484-fe6861c4039f)
<p align="center">
Transfer with some parameters from the console and environment (Without appsetting,.json and config.ini)
</p>

![image](https://github.com/komilffo-d/IPRangeCheck/assets/74680206/42660352-c813-4864-8b3e-7647ccebce8d)
<p align="center">
Transfer with some parameters from the console and environment, appsetting,.json and config.ini
</p>

![image](https://github.com/komilffo-d/IPRangeCheck/assets/74680206/55b49b02-df4b-4b3a-83d7-85c55f1bec4c)
<p align="center">
Transmitting all valid parameters from the console and environment, appsdings,.json and config.ini
</p>

![image](https://github.com/komilffo-d/IPRangeCheck/assets/74680206/66b29e1b-ea00-40b2-86f6-09923655ec75)
<p align="center">
Output Data
</p>

