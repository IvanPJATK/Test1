namespace APBD_TEST_TEMPLATE.DTO
{
    public class VendorsDto
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public List<VendorProductsDto> productsList { get; set; } = null!;
    }
}
