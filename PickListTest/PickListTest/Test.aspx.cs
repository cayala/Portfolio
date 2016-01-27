using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickListTest
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["name"] != null)
            //{
            //    PartsTable = Session["name"] as 
            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            generateContent();
        }

        public void generateContent()
        {
            dynamic.Text = TextBox1.Text;
        }

        protected void AddPart(object sender, EventArgs e)
        {
            List<string> tableRow = new List<string>();
            tableRow.Add(referenceBox.Text);
            tableRow.Add(partBox.Text);
            tableRow.Add(QuantityBox.Text);
            tableRow.Add(descriptionBox.Text);
            tableRow.Add(Add1Box.Text);
            tableRow.Add(Add2Box.Text);

            GenerateRow(tableRow);
        }

        public void GenerateRow(List<string> list)
        {
            TableRow newRow = new TableRow();

            foreach (var item in list)
            {
                TableCell newCell = new TableCell();
                newCell.Text = item;
                newRow.Cells.Add(newCell);
            }
            PartsTable.Rows.Add(newRow);
        }

        //public void GenerateRow(List<string> list)
        //{
        //    var row = PartsTable.Rows;
        //    int x = 0;
        //    for (x = 0; x <= PartsTable.Rows.Count; x++ )
        //    {
        //        if (x == PartsTable.Rows.Count)
        //        {
        //            TableRow newRow = new TableRow();
        //            foreach (var st in list)
        //            {
        //                TableCell newCell = new TableCell();
        //                newCell.Text = st;
        //                newRow.Cells.Add(newCell);
        //            }
        //            PartsTable.Rows.Add(newRow);
        //            break;
        //        }
        //        PartsTable.Rows.Add((TableRow)row[x]);
        //    }
        //}
    }

    //public class DynamicTable
    //{
    //    public DynamicTable() { }

    //    public DataTable CreateDataSource()
    //    {
    //        DataTable dt = new DataTable();
    //        DataColumn identity = new DataColumn("ID", typeof(int));
    //        dt.Columns.Add(identity);
    //        dt.Columns.Add("Name", typeof(string));
    //        return dt;
    //    }

    //    public void AddRow(int id, string name, DataTable dt)
    //    {
    //        dt.Rows.Add(new object[] { id, name, pname });
    //    }
    //}


}