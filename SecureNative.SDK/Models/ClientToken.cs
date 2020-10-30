namespace SecureNative.SDK.Models
{
    public class ClientToken
    {
        private string Cid { get; set; }
        private string Vid { get; set; }
        private string Fp { get; set; }

        public ClientToken(string cid, string vid, string fp)
        {
            Cid = cid;
            Fp = fp;
            Vid = vid;
        }

        public string GetCid()
        {
            return Cid;
        }

        public void SetCid(string value)
        {
            Cid = value;
        }

        public string GetVid()
        {
            return Vid;
        }

        public void SetVid(string value)
        {
            Vid = value;
        }

        public string GetFp()
        {
            return Fp;
        }

        public void SetFp(string value)
        {
            Fp = value;
        }
    }
}
