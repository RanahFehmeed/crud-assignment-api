namespace ProductManagementAPI.Dto
{
    public class TagWithProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> ProductNames { get; set; }
    }

}
