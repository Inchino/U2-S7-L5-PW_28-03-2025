﻿namespace GestionaleConcerti.Settings
{
    public class IdentitySettings
    {
        public bool SignInRequireConfirmedAccount { get; set; }
        public int RequiredLength { get; set; }
        public bool RequireDigit { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireUppercase { get; set; }
    }
}
