using System.Text;
using VirtualizationService.Persistence;

namespace VirtualizationService.ContractExtensions
{
    public static class ResponseTransforms
    {
        public static Connection ConvertConnectionStringToBase64(this Connection connection)
        {
            if(connection is not null)
            {
                var bytes = Encoding.UTF8.GetBytes(connection.ConnectionString);
                string base64String = Convert.ToBase64String(bytes);
                return connection with { ConnectionString = base64String };

            }

            return connection with { ConnectionString = String.Empty };
        }
    }
}
