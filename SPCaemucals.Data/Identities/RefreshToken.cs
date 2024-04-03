namespace SPCaemucals.Data.Identities;

public class RefreshToken
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Primary key
    public string Token { get; set; } // The refresh token string
    public DateTime Expires { get; set; } // Expiration time of the refresh token
    public bool IsExpired => DateTime.UtcNow >= Expires; // Checks if token is expired
    public DateTime Created { get; set; } // Creation time of the token
    public string CreatedByIp { get; set; } // IP address of the client that requested the token
    public DateTime? Revoked { get; set; } // The time the token was revoked
    public string? RevokedByIp { get; set; } // IP address of the client that revoked the token
    public bool IsActive => Revoked == null && !IsExpired; // Checks if token is active

    public string UserId { get; set; } // Foreign key to the ApplicationUser
    public ApplicationUser User { get; set; } // Navigation property to the ApplicationUser
}