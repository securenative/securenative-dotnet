using System;

namespace SecureNative.SDK.Models
{
    public class ClientToken
    {
        public string Cid { get; set; }
        public string Vid { get; set; }
        public string Fp { get; set; }

        public ClientToken()
        {
        }

        public ClientToken(string cid, string vid, string fp)
        {
            this.Cid = cid;
            this.Fp = fp;
            this.Vid = vid;
        }
    }
}
