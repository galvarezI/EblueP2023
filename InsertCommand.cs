using System;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Diagnostics;

public class InsertCommand
{
    Hashtable argtable = new Hashtable();
    string table;

    //Constructing Insert Object
    //</sumary>
    //table name to insert to = param name = "table"
	public InsertCommand(String table)
	{
        this.table = table;
	}

    //adds time to insert object
    //param name> item name
    //param name> item value
    public void add(string name, object val)
    {
        argtable.Add(name, val);
    }

    //Removes item from Insert object
    //param name> item name
    public void remove(string name)
    {
        try
        {
            argtable.Remove(name);
        }
        catch
        {
            throw (new Exception("No existe esa entrada"));
        }
    }

    //Test representantion of the Insert object (SQL query)
    //returns system string
    public override string ToString()
    {
        StringBuilder S1 = new StringBuilder();
        StringBuilder S2 = new StringBuilder();

        IDictionaryEnumerator enuminterface = argtable.GetEnumerator();
        bool first = true;
        while(enuminterface.MoveNext())
        {
            if (first) first = false;
            else
            {
                S1.Append(", ");
                S2.Append(", ");
            }
            S1.Append(enuminterface.Key.ToString());
            S2.Append(enuminterface.Value.ToString());
        }

        return "INSERT INTO " + table + " (" + S1 + ") VALUES (" + S2 + ");";
    }

    //Gets or sets item into Insert object
    object this[string key]
    {
        get
        {
            Debug.Assert(argtable.Contains(key), "Key not found");
            return argtable[key];
        }
        set { argtable[key] = value;}
    }
}
