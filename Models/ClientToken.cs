using System;

namespace SecureNative.SDK.Models
{
    public class ClientToken
    {
        private string Cid { get; set; }
        private string Vid { get; set; }
        private string Fp { get; set; }

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
