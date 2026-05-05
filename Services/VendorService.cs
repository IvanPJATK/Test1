using APBD_TEST_TEMPLATE.DTO;

namespace APBD_TEST_TEMPLATE.Services
{
    public class VendorService : IVendorService
    {
        private readonly string _connectionString;
        public VendorService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException();
        }
        public async Task<List<VendorsDto>> GetVendors(string? name)
        {
            throw new NotImplementedException();
        }
    }
}
