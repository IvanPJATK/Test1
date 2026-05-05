using APBD_TEST_TEMPLATE.DTO;

namespace APBD_TEST_TEMPLATE.Services
{
    public interface IVendorService
    {
       Task<List<VendorsDto>> GetVendors(string? name); 
    }
}
