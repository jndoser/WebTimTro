namespace WebTimTro.Helpers
{
    public static class LocationHelper
    {
        public static string GetDistrictFromAddress(string address)
        {
            string[] compound = address.Split(',');
            return compound[compound.Length - 2].Trim();
        }
    }
}
