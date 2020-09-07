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

        public string GetCid()
        {
            return this.Cid;
        }

        public void SetCid(string value)
        {
            this.Cid = value;
        }

        public string GetVid()
        {
            return this.Vid;
        }

        public void SetVid(string value)
        {
            this.Vid = value;
        }

        public string GetFp()
        {
            return this.Fp;
        }

        public void SetFp(string value)
        {
            this.Fp = value;
        }
    }
}
