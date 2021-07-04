using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DublicateValuePair
/// </summary>
public class DublicateValuePair : List<KeyValuePair<string, string>>
{
    public DublicateValuePair()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void Add(string key, string value)
    {
        var element = new KeyValuePair<string, string>(key, value);
        this.Add(element);
    }
    
}
