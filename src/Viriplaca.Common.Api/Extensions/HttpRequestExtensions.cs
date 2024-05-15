using System.Net;
using Microsoft.AspNetCore.Http;

namespace Viriplaca.Common.Api.Extensions;

public static class HttpRequestExtensions
{
    public static IPAddress GetAddress(this HttpRequest request)
    {
        try
        {
            var clientIP = request.Headers["Client-IP"].FirstOrDefault();
            if (IPAddress.TryParse(clientIP, out var address) && address is not null)
            {
                return address;
            }

            var xForwardedFor = request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!xForwardedFor.IsNullOrWhiteSpace())
            {
                var ipStrings = xForwardedFor.Split(',', StringSplitOptions.RemoveEmptyEntries);
                var ipString = ipStrings.Length > 2 ? ipStrings[^2] : ipStrings[0];
                if (IPAddress.TryParse(ipString, out address) && address is not null)
                {
                    return address;
                }
            }

            var remoteIpAddress = request.HttpContext.Connection.RemoteIpAddress;
            if (remoteIpAddress is not null)
            {
                return remoteIpAddress;
            }

            return IPAddress.Any;
        }
        catch (Exception)
        {
            return IPAddress.Any;
        }
    }
}
