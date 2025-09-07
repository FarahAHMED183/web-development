using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthOtp
{
    public class OtpEntry
    {
        public string Otp { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    public interface IOtpService
    {
        Task<string> GenerateAndStoreOtpAsync(string email, TimeSpan ttl);
        Task<bool> ValidateOtpAsync(string email, string otp);
        string CreateSessionToken(string userId, TimeSpan ttl);
        (bool ok, string userId) ValidateSessionToken(string token);
    }

    public class OtpService : IOtpService
    {
        // In-memory OTP store. Replace with persistent store (DB/Redis) in production.
        private static ConcurrentDictionary<string, OtpEntry> _store = new ConcurrentDictionary<string, OtpEntry>();
        private readonly byte[] _key;

        public OtpService(string secretKey)
        {
            // secretKey must be a sufficiently long secret used to sign session tokens
            _key = Encoding.UTF8.GetBytes(secretKey ?? throw new ArgumentNullException(nameof(secretKey)));
        }

        public Task<string> GenerateAndStoreOtpAsync(string email, TimeSpan ttl)
        {
            var rng = RandomNumberGenerator.Create();
            var bytes = new byte[4];
            rng.GetBytes(bytes);
            var num = BitConverter.ToUInt32(bytes, 0) % 1000000;
            var otp = num.ToString("D6");
            var entry = new OtpEntry { Otp = otp, ExpiresAt = DateTime.UtcNow.Add(ttl) };
            _store[email.ToLowerInvariant()] = entry;
            return Task.FromResult(otp);
        }

        public Task<bool> ValidateOtpAsync(string email, string otp)
        {
            if (!_store.TryGetValue(email.ToLowerInvariant(), out var entry)) return Task.FromResult(false);
            if (DateTime.UtcNow > entry.ExpiresAt) return Task.FromResult(false);
            return Task.FromResult(entry.Otp == otp);
        }

        // Simple session token: base64(userId|expiry|hmac)
        public string CreateSessionToken(string userId, TimeSpan ttl)
        {
            var expiry = DateTime.UtcNow.Add(ttl).ToString("o");
            var payload = $"{userId}|{expiry}";
            var sig = ComputeHmac(payload);
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{payload}|{sig}"));
            return token;
        }

        public (bool ok, string userId) ValidateSessionToken(string token)
        {
            try
            {
                var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var parts = decoded.Split('|');
                if (parts.Length < 3) return (false, null);
                var userId = parts[0];
                var expiry = DateTime.Parse(parts[1], null, System.Globalization.DateTimeStyles.RoundtripKind);
                var sig = parts[2];
                var payload = $"{userId}|{parts[1]}";
                var expected = ComputeHmac(payload);
                if (sig != expected) return (false, null);
                if (DateTime.UtcNow > expiry) return (false, null);
                return (true, userId);
            }
            catch
            {
                return (false, null);
            }
        }

        private string ComputeHmac(string payload)
        {
            using var h = new HMACSHA256(_key);
            var sig = h.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return Convert.ToBase64String(sig);
        }
    }
}
