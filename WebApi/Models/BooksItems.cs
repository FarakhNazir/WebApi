namespace WebApi.Models
{
    public class BooksItems
    {
        private Guid _id;
        private string _name;
        private string _description;
        private string _price;

        public Guid Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public string Price { get => _price; set => _price = value; }
    }
}
