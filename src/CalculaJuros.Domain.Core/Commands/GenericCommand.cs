using CalculaJuros.Domain.Shared.Command;
using FluentValidation.Results;
using System.Collections.Generic;

namespace CalculaJuros.Domain.Core.Commands
{
    public abstract class GenericCommand<T> : ICommandResult<T> 
    {
        public abstract bool EhValido();

        protected GenericCommand()
        {
            ValidationResult = new ValidationResult();
        }

        protected ValidationResult ValidationResult { get; set; }

        public virtual IList<ValidationFailure> GetErrors()
        {
            return ValidationResult.Errors;
        }
    }
}
