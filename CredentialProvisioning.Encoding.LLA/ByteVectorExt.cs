namespace Leosac.CredentialProvisioning.Encoding.LLA
{
    public static class ByteVectorExt
    {
        public static LibLogicalAccess.ByteVector ToByteVector(this string str)
        {
            return new LibLogicalAccess.ByteVector(System.Text.Encoding.Default.GetBytes(str));
        }
    }
}
