namespace eCommerce_backend.DTOs
{
    public class CreateBrandDto
    {
        public string Name { get; set; } = "";
        public string Country { get; set; } = "";
        public string Description { get; set; } = "";
        public string Website { get; set; } = "";
    }

    public class BrandDto : CreateBrandDto
    {
        public int Id { get; set; }
    }

    public class BrandSelectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

}
