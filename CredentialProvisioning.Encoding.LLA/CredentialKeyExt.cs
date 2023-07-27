using LibLogicalAccess.Card;
using LibLogicalAccess;
using Leosac.CredentialProvisioning.Core.Models;
using System.Collections;
using Leosac.CredentialProvisioning.Encoding.Key;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using System;

namespace Leosac.CredentialProvisioning.Encoding.LLA
{
    public static class CredentialKeyExt
    {
        public static LibLogicalAccess.Key? CreateKey(this CredentialKey k, LLACardContext? cardCtx = null, Key.KeyDiversification? div = null)
        {
            LibLogicalAccess.Key? key = null;
            if (k.KeyType == "aes128")
            {
                var dkey = new DESFireKey();
                dkey.setKeyType(DESFireKeyType.DF_KEY_AES);
                key = dkey;
            }
            else if (k.KeyType == "2k3des")
            {
                var dkey = new DESFireKey();
                dkey.setKeyType(DESFireKeyType.DF_KEY_DES);
                key = dkey;
            }
            else if (k.KeyType == "3k3des")
            {
                var dkey = new DESFireKey();
                dkey.setKeyType(DESFireKeyType.DF_KEY_3K3DES);
                key = dkey;
            }

            if (key != null)
            {
                if (k.KeyStoreType == "hsm")
                {
                    var ks = new PKCSKeyStorage();
                    ks.set_key_id(new ByteVector(System.Text.Encoding.UTF8.GetBytes(k.KeyStoreReference)));
                    // TODO: set here PKCS#11 library path and password from application configuration file
                    key.setKeyStorage(ks);
                }
                else if (k.KeyStoreType == "sam")
                {
                    var ks = new SAMKeyStorage();
                    byte slot = 0;
                    byte.TryParse(k.KeyStoreReference, out slot);
                    ks.setKeySlot(slot);
                }
                else
                {
                    if (!string.IsNullOrEmpty(k.Value))
                    {
                        var fvalue = Regex.Replace(k.Value, ".{2}", "$0 ").Trim();
                        key.fromString(fvalue);
                    }
                }

                if (div != null)
                {
                    if (div.Algorithm == "an0945")
                    {
                        var kd = new NXPAV1KeyDiversification();
                        key.setKeyDiversification(kd);
                    }
                    else if (div.Algorithm == "an10922")
                    {
                        var kd = new NXPAV2KeyDiversification();
                        if (div.Input != null && div.Input.Length > 0)
                        {
                            kd.setDivInput(new ByteVector(ComputeDivInput(cardCtx?.Credential?.Data, div.Input)));
                        }
                        else
                        {
                            if (div.RevertAID != null)
                            {
                                kd.setRevertAID(div.RevertAID.Value);
                            }
                            if (div.ForceK2Use != null)
                            {
                                kd.setForceK2Use(div.ForceK2Use.Value);
                            }
                            if (div.SystemIdentifier != null)
                            {
                                kd.setSystemIdentifier(new ByteVector(Convert.FromHexString(div.SystemIdentifier)));
                            }
                        }
                        key.setKeyDiversification(kd);
                    }
                    else if (div.Algorithm == "sagem")
                    {
                        var kd = new SagemKeyDiversification();
                        key.setKeyDiversification(kd);
                    }
                    else if (div.Algorithm == "omnitech")
                    {
                        var kd = new OmnitechKeyDiversification();
                        key.setKeyDiversification(kd);
                    }
                }
            }

            return key;
        }

        /// <summary>
        /// Compute div input using credential data and input fragments
        /// </summary>
        /// <param name="data">The credential data</param>
        /// <param name="input">The input fragments</param>
        /// <returns>The div input</returns>
        /// <remarks>It is assumed that all fragment values are hexstring.</remarks>
        private static byte[] ComputeDivInput(IDictionary<string, object> data, DivInputFragment[] input)
        {
            var ret = new List<byte>();
            foreach(var i in input)
            {
                if (i.Type == DivInputFragmentType.DataField)
                {
                    if (data != null && data.ContainsKey(i.Value))
                    {
                        var v = data[i.Value]?.ToString();
                        if (!string.IsNullOrEmpty(v))
                        {
                            ret.AddRange(Convert.FromHexString(v));
                        }
                    }
                }
                else
                {
                    ret.AddRange(Convert.FromHexString(i.Value));
                }
            }
            return ret.ToArray();
        }
    }
}
