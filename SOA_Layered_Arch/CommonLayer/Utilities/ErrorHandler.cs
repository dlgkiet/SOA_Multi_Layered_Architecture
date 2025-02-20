namespace SOA_Layered_Arch.CommonLayer.Utilities
{
    public static class ErrorHandler
    {
        public static string GetErrorMessage(Exception ex)
        {
            return ex.Message;
        }
    }

}
