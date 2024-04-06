using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPRangeCheckConsole.Attributes
{
    internal class FilePathValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string filePath = value as string;

            if (string.IsNullOrEmpty(filePath))
                return false; 

            try
            {
                FileInfo fileInfo = new FileInfo(filePath);

                if (!fileInfo.Exists)
                    return false;


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
