using System;

namespace Authentication
{
    [Serializable]
    public struct DataUser
    {
        public string name;
        public string password;
        public uint coin;
        public uint score;
        public string imgProfile;
        public bool character1;
        public bool character2;

        public DataUser(string name, string pwd, uint coin, uint score, string imgProfile, bool character1,
            bool character2)
        {
            this.name = name;
            this.password = pwd;
            this.coin = coin;
            this.score = score;
            this.imgProfile = imgProfile;
            this.character1 = character1;
            this.character2 = character2;
        }
    }
}