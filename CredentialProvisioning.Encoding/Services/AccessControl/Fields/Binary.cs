﻿namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl.Fields
{
    /// <summary>
    /// Binary data field.
    /// </summary>
    public class Binary : ValueDataField
    {
        /// <summary>
        /// The hexstring representing the binary data.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// The padding hex char.
        /// </summary>
        public byte PaddingChar { get; set; }
    }
}
