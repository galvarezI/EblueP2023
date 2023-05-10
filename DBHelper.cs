using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public class DBHelper
{
    #region
    public DataSet DeptDS
    {
        get
        {
            return FillComboBox("Department", "DID", "DName", "None");
        }
        set
        {
        }
    }
    #endregion
    
    public static string ConnStr = ConfigurationManager.ConnectionStrings("eblueConnectionString").ConnectionString;

    public static DataSet FillGV(string queryString)
    {
        string connectionString = ConfigurationManager.ConnectionStrings("eBluePaper").ConnectionString;

        DataSet ds = new DataSet();

        try
        {

            // Connect to the database and run the query.
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);

            // Fill the DataSet.
            adapter.Fill(ds);
        }

        catch (Exception ex)
        {
        }

        return ds;
    }

    private DataSet FillFromText(string TableName, string ColumnName, string FirstRow)
    {
        DataSet tempDS = new DataSet();
        DataTable tempDT = new DataTable();
        DataRow tempDR;
        DataColumn tempDC;

        DataSet dsDB;
        DataTable dtDB;
        DataRow drDB;

        // tempDS = _DistritoDS
        // tempDT = tempDS.Tables(0)
        tempDC = new DataColumn();
        tempDC.DataType = System.Type.GetType("System.String");
        tempDC.ColumnName = ColumnName;
        tempDT.Columns.Add(tempDC);
        tempDS.Tables.Add(tempDT);

        tempDR = tempDT.NewRow;
        // tempDR("Results") = 0
        tempDR(ColumnName) = "<" + FirstRow + ">";
        tempDT.Rows.Add(tempDR);

        string str;
        str = "select distinct " + ColumnName + " from " + TableName;
        dsDB = CreateDataSet(str);
        dtDB = dsDB.Tables(0);
        foreach (var drDB in dtDB.Rows)
        {
            tempDR = tempDT.NewRow;
            tempDR(ColumnName) = drDB(ColumnName);
            tempDT.Rows.Add(tempDR);
        }
        return tempDS;
    }

    private DataSet FillComboBox(string TableName, string ColValue, string ColText, string FirstRow)
    {
        DataSet tempDS = new DataSet();
        DataTable tempDT = new DataTable();
        DataRow tempDR;
        DataColumn tempDC;

        DataSet dsDB;
        DataTable dtDB;
        DataRow drDB;

        // tempDS = _DistritoDS
        // tempDT = tempDS.Tables(0)
        tempDC = new DataColumn();
        tempDC.DataType = System.Type.GetType("System.Int32");
        tempDC.ColumnName = ColValue;
        tempDT.Columns.Add(tempDC);
        tempDC = new DataColumn();
        tempDC.DataType = System.Type.GetType("System.String");
        tempDC.ColumnName = ColText;
        tempDT.Columns.Add(tempDC);
        tempDS.Tables.Add(tempDT);

        tempDR = tempDT.NewRow;
        tempDR(ColValue) = 0;
        tempDR(ColText) = "<" + FirstRow + ">";
        tempDT.Rows.Add(tempDR);

        string str;
        str = "select * from " + TableName;
        dsDB = CreateDataSet(str);
        dtDB = dsDB.Tables(0);
        foreach (var drDB in dtDB.Rows)
        {
            tempDR = tempDT.NewRow;
            tempDR(ColValue) = drDB(ColValue);
            tempDR(ColText) = drDB(ColText);
            tempDT.Rows.Add(tempDR);
        }
        return tempDS;
    }

    // Create a DATASET
    public static DataSet CreateDataSet(string strSQL, SqlParameter sqlParam = null/* TODO Change to default(_) if this is not a reference type */)
    {
        // Try
        SqlConnection scnnNW = new SqlConnection(ConfigurationManager.ConnectionStrings("eBluePaper").ConnectionString);
        SqlCommand scmd = new SqlCommand(strSQL, scnnNW);

        if (!IsNothing(sqlParam))
            scmd.Parameters.Add(sqlParam);

        SqlDataAdapter sda = new SqlDataAdapter(scmd);
        DataSet ds = new DataSet();

        sda.Fill(ds);

        return ds;
        // CreateDataSet = ds

        scnnNW.Close();
        sda.Dispose();
        scmd.Dispose();
    }

}
