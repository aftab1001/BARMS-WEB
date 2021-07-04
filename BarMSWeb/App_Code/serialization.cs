using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Summary description for serialization
/// </summary>
[Serializable]
public class Serialization
{
    public static MemoryStream gStream;
    public Serialization()
    {

    }
    public MemoryStream Serialize(DataTable obj)
    {
        MemoryStream ms = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(ms, obj);
        ms.Position = 0;
        return ms;
    }
    public DataTable Deserialize(MemoryStream ms)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        ms.Position = 0;
        return (DataTable)formatter.Deserialize(ms);
    }

    public MemoryStream SerializeObject(Object obj)
    {
        MemoryStream ms = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(ms, obj);
        ms.Position = 0;
        return ms;
    }
    public Object DeserializeObject(MemoryStream ms)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        ms.Position = 0;
        return (Object)formatter.Deserialize(ms); 
    }


    public MemoryStream SerializeHtml(string html)
    {
        MemoryStream ms = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(ms, html);
        ms.Position = 0;
        return ms;
    }
    public string DeSerializeHtml(MemoryStream ms)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        ms.Position = 0;
        return (string)formatter.Deserialize(ms);
    }


}
