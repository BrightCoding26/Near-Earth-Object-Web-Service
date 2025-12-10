using System.Security.Cryptography;
using System.Text;

namespace NearEarthObjectsWebServiceTests.TestUtility;

public static class NumberGenerator
{
    public static int UniqueNumber(string? input = null)
    {
        input ??= Guid.NewGuid().ToString();
        byte[] hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(input));
        int hashInt = BitConverter.ToInt32(hashBytes, 0);
        return Math.Abs(hashInt) % 9000 + 1000;
    }
}
