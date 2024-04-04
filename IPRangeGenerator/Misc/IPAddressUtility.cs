namespace IPRangeGenerator.Misc
{
    public static class IPAddressUtility
    {
        public static bool IsValidIPv4(string IpAddressString)
        {

            string[] octets = IpAddressString.Split('.');


            if (octets.Length != 4) return false;

            foreach (var octet in octets)
            {
                int q;

                if (!int.TryParse(octet, out q)
                    || !q.ToString().Length.Equals(octet.Length)
                    || q < 0
                    || q > 255)
                    return false;

            }

            return true;
        }
    }
}
