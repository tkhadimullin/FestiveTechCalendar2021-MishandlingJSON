namespace MishandlingJson.Models
{
    internal class ItemData
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public List<Category> Category { get; set; }
    }

    internal class Category 
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
