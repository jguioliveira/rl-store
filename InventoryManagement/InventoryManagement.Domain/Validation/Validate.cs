using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.Validation
{
    public abstract class Validate
    {
        private readonly List<Error> _errors;

        protected Validate()
        {
            _errors = new List<Error>();
        }

        public IReadOnlyCollection<Error> Errors => _errors;

        public void AddError(string message)
        {
            _errors.Add(new Error(message));
        }

        public void AddError(Error error)
        {
            _errors.Add(error);
        }

        public void AddError(IEnumerable<Error> errors)
        {
            _errors.AddRange(errors);
        }

        public void AddError(Validate item)
        {
            AddError(item.Errors);
        }

        public void AddError(params Validate[] items)
        {
            foreach (var item in items)
                AddError(item);
        }

        public void Clear()
        {
            _errors.Clear();
        }

        public bool IsValid => !_errors.Any();
    }
}
