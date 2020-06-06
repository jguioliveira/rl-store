using InventoryManagement.Domain.Validation;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.Commands
{
    public class UpdateBookMarksCommand : Validate, ICommand
    {
        public string ProductId { get; set; }
        public IEnumerable<BookMark> BookMarks { get; set; }

        public void Validate()
        {
            if (!(BookMarks is null) && BookMarks.Any())
            {
                for (int i = 0; i < BookMarks.Count(); i++)
                {
                    if (string.IsNullOrEmpty(BookMarks.ElementAt(i).Code))
                        AddError($"Invalid bookmark code [line {i}].");

                    if (string.IsNullOrEmpty(BookMarks.ElementAt(i).Name))
                        AddError($"Invalid bookmark name [line {i}].");

                    if (string.IsNullOrEmpty(BookMarks.ElementAt(i).Value))
                        AddError($"Invalid bookmark name [line {i}].");

                    if (BookMarks.ElementAt(i).Count < 0)
                        AddError($"Invalid bookmark count [line {i}].");
                }
            }
        }

        public class BookMark
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public short Count { get; set; }
        }
    }
}
