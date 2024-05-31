using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Users;

public class User : Entity {

    public int TeamMemberId { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string GoogleLoginId { get; set; }
    public bool TwoFactorAuthEnabled { get; set; }

    public User()
    {
        TeamMemberId = 0;
        Email = "user@example.com";
        PasswordHash = "hashed_password";
        GoogleLoginId = "google_user_id";
        TwoFactorAuthEnabled = false;
    }

    public User(int teamMemberId, string email, string passwordHash, string googleLoginId, bool twoFactorAuthEnabled)
    {
        TeamMemberId = teamMemberId;
        Email = email;
        PasswordHash = passwordHash;
        GoogleLoginId = googleLoginId;
        TwoFactorAuthEnabled = twoFactorAuthEnabled;
    }
}
