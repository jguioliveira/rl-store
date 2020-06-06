namespace InventoryManagement.Domain.Entities
{
    public class ProductBookMark
    {
        public ProductBookMark(Product product, string code, string name, string value, short count)
        {
            Product = product;
            Code = code;
            Name = name;
            Value = value;
            Count = count;
        }

        public Product Product { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Value { get; private set; }
        public short Count { get; private set; }
    }
}
