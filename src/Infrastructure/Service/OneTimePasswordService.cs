using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Service;



public sealed class OneTimePasswordService(ICacheProvider cache) : IOneTimePasswordService
{

    private string GenerateSecureOtp(int length = 6)
    {
        const string digits = "0123456789";
        var data = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(data);

        var result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = digits[data[i] % digits.Length];
        }

        return new string(result);
    }

    public string CreateAndStoreOtp(string email)
    {
        var code = GenerateSecureOtp();
        cache.SetCache(email, code,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3),
                SlidingExpiration = TimeSpan.FromMinutes(1)
            });
        return code;
    }

    public bool ValidateOtp(string email, string requestCode)
    {
        var storedCache=  cache.GetFromCache<string>(email);

        if (storedCache is null)
        {
            return false;
        }
        bool isValid = storedCache.Equals(requestCode);

        if (isValid) 
        {
            cache.ClearCache(email);
        }

        return isValid;
    }

}