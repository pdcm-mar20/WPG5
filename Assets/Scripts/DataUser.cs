using System;

[Serializable]
public struct DataUser
{
    public string name;
    public string pwd;
    public uint coin;
    public uint score;

    public DataUser(string name, string pwd, uint coin, uint score)
    {
        this.name = name;
        this.pwd = pwd;
        this.coin = coin;
        this.score = score;
    }
}