namespace InventoryManagement.Domain.Entities
{
    public class ProductBookMark
    {
        public ProductBookMark(string productId, string code, string name, string value, short count)
        {
            ProductId = productId;
            Code = code;
            Name = name;
            Value = value;
            Count = count;
        }

        public string ProductId { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Value { get; private set; }
        public short Count { get; private set; }

        public Product Product { get; private set; }
    }
}
