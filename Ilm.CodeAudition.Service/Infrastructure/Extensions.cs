using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Ilm.CodeAudition.Service.Infrastructure
{
    public static class Extensions
    {
        public static void ValidateAndThrow<T>(this IValidator validator, T instance)
        {
            var result = validator.Validate(instance);

            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }
    }
}
