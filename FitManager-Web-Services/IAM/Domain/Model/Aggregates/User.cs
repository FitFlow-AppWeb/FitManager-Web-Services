using System;
using System.Text.Json.Serialization;

namespace FitManager_Web_Services.Users.Domain.Model
{
    public class User
    {
        public int Id { get; private set; }
        
        public string Email { get; private set; } = default!;
        
        [JsonIgnore]
        public string PasswordHash { get; private set; } = default!;
        
        public string Icon { get; private set; } = default!;
        
        public string Subscription { get; private set; } = default!;
        
        // Constructor que asegura invariantes del agregado
        public User(int id, string email, string passwordHash, string icon, string subscription)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required.", nameof(email));
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("PasswordHash is required.", nameof(passwordHash));
            if (string.IsNullOrWhiteSpace(icon)) throw new ArgumentException("Icon is required.", nameof(icon));
            if (string.IsNullOrWhiteSpace(subscription)) throw new ArgumentException("Subscription is required.", nameof(subscription));

            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            Icon = icon;
            Subscription = subscription;
        }

        // Constructor para EF Core
        protected User() { }

        // Métodos de dominio
        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail)) throw new ArgumentException("Email is required.", nameof(newEmail));
            Email = newEmail;
        }

        /// <summary>
        /// Update the password hash
        /// </summary>
        /// <param name="passwordHash">The new password hash</param>
        /// <returns>The updated user</returns>
        public User UpdatePasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("PasswordHash is required.", nameof(passwordHash));
            PasswordHash = passwordHash;
            return this;
        }

        public void UpdateIcon(string newIcon)
        {
            if (string.IsNullOrWhiteSpace(newIcon)) throw new ArgumentException("Icon is required.", nameof(newIcon));
            Icon = newIcon;
        }

        public void ActivateSubscription(string newSubscription)
        {
            if (string.IsNullOrWhiteSpace(newSubscription)) throw new ArgumentException("Subscription is required.", nameof(newSubscription));
            Subscription = newSubscription;
        }

        public void CancelSubscription()
        {
            Subscription = string.Empty;
        }
    }
}