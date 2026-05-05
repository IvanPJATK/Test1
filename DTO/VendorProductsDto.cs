namespace APBD_TEST_TEMPLATE.DTO
{
    public class VendorProductsDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; }  = null!;
        public decimal StickerPrice { get; set; }
        public ProductTypeDto typeDto { get; set; } = null!;
        public MakerDto makerDto { get; set; } = null!;
        public VendorOfferDto vendorOfferDto { get; set; } = null!;
    }
}
