using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


[System.Serializable]
public class Account_Data
{
    public AccountsData[] Accounts;
}


[System.Serializable]
public class AccountsData
{
    public string ID;
    public string PW;
}