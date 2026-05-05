using APBD_TEST_TEMPLATE.DTO;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace APBD_TEST_TEMPLATE.Services
{
    public class VendorService : IVendorService
    {
        private readonly string _connectionString;
        public VendorService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException();
        }

        public async Task AddVendor(VendorsDto vendor)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync();
            try
            {

            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public async Task<List<VendorsDto>> GetVendors(string? name)
        {
            var vendorsDict = new Dictionary<string, VendorsDto>();
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            const string select_sql = """
                                    select v.Code, v.Name as VendorName, 
                vp.ProductId as ProductId, vp.Amount as ProductAmount, vp.PricePerUnit, 
                p.Name as ProductName, p.Description, p.StickerPrice,
                pt.Id as TypeId, pt.Name as TypeName,
                m.Id as MakerId, m.Name as MakerName
                from Vendors v
                left join VendorProducts vp on vp.VendorCode = v.Code
                left join Products p on p.Id = vp.ProductId
                join ProductTypes pt on pt.Id = p.ProductTypeId
                join Makers m on m.Id = p.MakerId
                """;

            await using var command = new SqlCommand(select_sql, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while(await reader.ReadAsync())
            {
                var products_dict = new Dictionary<string, VendorProductsDto>();
                //string VendorCode = reader.GetString(reader.GetOrdinal("Code"));
                //string VendorName = reader.GetString(reader.GetOrdinal("VendorName"));
                if (reader.IsDBNull(reader.GetOrdinal("Code"))) continue;
                string VendorCode = reader.GetString(reader.GetOrdinal("Code"));
                if (!vendorsDict.TryGetValue(VendorCode, out var vendor))
                {
                    vendor = new VendorsDto
                    {
                        Code = reader.GetString(reader.GetOrdinal("Code")),
                        Name = reader.GetString(reader.GetOrdinal("VendorName")),
                        productsList = new List<VendorProductsDto>()
                    };
                }
                if (!products_dict.TryGetValue(VendorCode, out var product))
                {
                    product = new VendorProductsDto
                    {
                        Id = (int)reader.GetDecimal(reader.GetOrdinal("")),
                        ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                        ProductDescription = reader.GetString(reader.GetOrdinal("ProductDescription")),
                        StickerPrice = reader.GetDecimal(reader.GetOrdinal("StickerPrice")),
                        typeDto = new ProductTypeDto { Id = (int)reader.GetDecimal(reader.GetOrdinal("TypeId")), Name = reader.GetString(reader.GetOrdinal("TypeName")) },
                        makerDto = new MakerDto { Id = (int)reader.GetDecimal(reader.GetOrdinal("MakerId")), Name = reader.GetString(reader.GetOrdinal("MakerName")) },
                        vendorOfferDto = new VendorOfferDto { amount = (int)reader.GetDecimal(reader.GetOrdinal("ProductAmount")), pricePerUnit = reader.GetDecimal(reader.GetOrdinal("PricePerUnit")) }

                    };
                }
                vendor.productsList = (products_dict.Values.ToList());
            }
            return vendorsDict.Values.ToList();

        }
    }
}
