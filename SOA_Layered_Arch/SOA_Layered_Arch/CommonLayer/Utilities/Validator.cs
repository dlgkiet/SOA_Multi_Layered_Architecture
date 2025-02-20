namespace SOA_Layered_Arch.CommonLayer.Utilities
{
    public static class Validator
    {
        public static bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }
    }

}
