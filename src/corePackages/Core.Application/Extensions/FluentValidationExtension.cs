using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Extensions
{
    public static class FluentValidationExtension
    {
        public static IRuleBuilderOptions<T, string> FirstLetterMustBeUpperCase<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(strToCheck => Char.IsUpper(strToCheck[0])).WithMessage("The first letter is not uppercase");
        }
    }
}
