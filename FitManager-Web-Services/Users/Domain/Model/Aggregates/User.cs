using System;

namespace FitManager_Web_Services.Users.Domain.Model
{
    public class User
    {
        public int Id { get; private set; }
        
        public string Email { get; private set; } = default!;
        
        public string Password { get; private set; } = default!;
        
        public string Icon { get; private set; } = default!;
        
        public string Subscription { get; private set; } = default!;
        
        // Constructor que asegura invariantes del agregado
        public User(int id, string email, string password, string icon, string subscription)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required.", nameof(email));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password is required.", nameof(password));
            if (string.IsNullOrWhiteSpace(icon)) throw new ArgumentException("Icon is required.", nameof(icon));
            if (string.IsNullOrWhiteSpace(subscription)) throw new ArgumentException("Subscription is required.", nameof(subscription));

            Id = id;
            Email = email;
            Password = password;
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

        public void ChangePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword)) throw new ArgumentException("Password is required.", nameof(newPassword));
            Password = newPassword;
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