using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using System.Web.Services;
using System.Web.Script.Services;

namespace PickListTest.UserControls
{
    public partial class ExampleWF : System.Web.UI.Page
    {
        private static System.Data.DataTable _table;
        private static HttpCookie _cookie = new HttpCookie("PartsTable");
        private Application _excel = new Application();

        protected void Page_Load(object sender, EventArgs e)
        {
            ExampleUC userInfoBoxControl = (ExampleUC)LoadControl("~/UserControls/ExampleUC.ascx");
            userInfoBoxControl.UserName = "Carlos Ayala";
            userInfoBoxControl.UserAge = 100;
            userInfoBoxControl.UserCountry = "USA";
            phUserInfoBox.Controls.Add(userInfoBoxControl);

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Ref", typeof(int));
            dt.Columns.Add("Part", typeof(int));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Qty", typeof(int));
            dt.Columns.Add("AdditionalDetails", typeof(string));

            DataRow dtrow = dt.NewRow();    // Create New Row
            dtrow["Ref"] = 1;            //Bind Data to Columns
            dtrow["Part"] = 2;
            dtrow["Description"] = "Testing";
            dtrow["Qty"] = 3;
            dtrow["AdditionalDetails"] = "TestingTesting";
            dt.Rows.Add(dtrow);

            _table = dt;
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
            
            if (Session["name"] != null)
            {
                dt = Session["name"] as System.Data.DataTable;
                _table = dt;
            }
            
            if (!IsPostBack)
            {
                dataTable(dt);
            }
        }

        public void generateContent(object sender, EventArgs e)
        {
            dynamic.Text = TextBox1.Text;
        }

        protected void dataTable(System.Data.DataTable dt)
        {
            _table = dt;
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
            HttpContext.Current.Session["name"] = dt;
        }
       
        protected void AddPart(object sender, EventArgs e)
        {
            List<string> tableRow = new List<string>();
            tableRow.Add(referenceBox.Text);
            tableRow.Add(partBox.Text);
            tableRow.Add(descriptionBox.Text);
            tableRow.Add(QuantityBox.Text);
            tableRow.Add(Add1Box.Text);

            GenerateRow(_table, tableRow);
        }

        protected void GenerateRow(System.Data.DataTable dt, List<string> s)
        {
            DataRow dtrow = dt.NewRow();    // Create New Row
            dtrow["Ref"] = s[0];            //Bind Data to Columns
            dtrow["Part"] = s[1];
            dtrow["Description"] = s[2];
            dtrow["Qty"] = s[3];
            dtrow["AdditionalDetails"] = s[4];
            dt.Rows.Add(dtrow);
            _table = dt;
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
            HttpContext.Current.Session["name"] = dt;
        }
        //**NOTE BE SURE TO CHANGE IN THE ~/App_Start/RouteConfig.cs change
        //settings.AutoRedirectMode = Redirectmode.Permanent;
        //TO
        //settings.AutoRedirectMode = RedirectMode.Off
        //Also if friendly URLS are enabled then change regular url to 
        // '<%=ResolveUrl("RoutePath/FileName.aspx/methodName")%>'
        [WebMethod]
        public static void GetTableData(string data)
        {
            _cookie["PartsTable"] = data;
           // return "hi";
        }

        public void StoreTableToCookie(object sender, EventArgs e)
        {
            _cookie.Expires = DateTime.Now.AddDays(1);
            gvDetails.DataSource = _table;
            gvDetails.DataBind();
            HttpContext.Current.Session["name"] = _table;
            Response.Cookies.Add(_cookie); // Send cookie back in response to "update" client
            Console.WriteLine(_cookie["PartsTable"]);
            //try saving table to indivdual properties definded as rows and then cells in the cookie w/ foreach then reconstruct them
        }

        public void DeleteCookie(object sender, EventArgs e)
        {
            _cookie.Expires = DateTime.Now.AddDays(-1);
            gvDetails.DataSource = _table;
            gvDetails.DataBind();
            HttpContext.Current.Session["name"] = _table;
            Response.Cookies.Add(_cookie); // Remember to always send cookie back in response
            Console.WriteLine(_cookie["PartsTable"]);
        }

        public void ExportToExcel(object sender, EventArgs e)
        {
            var tableRow = _table.Rows;
            //Open Excel and get worksheet
            var app = new Application();
            var workbook = app.Workbooks.Add();
            var worksheet = workbook.Worksheets[1] as Microsoft.Office.Interop.Excel.Worksheet;
            //Add headers
            worksheet.Cells[1, 1] = "Reference #";
            worksheet.Cells[1, 2] = "Part #";
            worksheet.Cells[1, 3] = "Description | Serial #";
            worksheet.Cells[1, 4] = "Quantity";
            worksheet.Cells[1, 5] = "Additional Information";

            for (int x = 0; x < _table.Rows.Count; x++)
            {
                worksheet.Cells[x + 2, 1] = tableRow[x].ItemArray[0];
                worksheet.Cells[x + 2, 2] = tableRow[x].ItemArray[1];
                worksheet.Cells[x + 2, 3] = tableRow[x].ItemArray[2];
                worksheet.Cells[x + 2, 4] = tableRow[x].ItemArray[3];
                worksheet.Cells[x + 2, 5] = tableRow[x].ItemArray[4];
            }
                //save
            gvDetails.DataSource = _table;
            gvDetails.DataBind();
            HttpContext.Current.Session["name"] = _table;

            //Response.WriteFile(workbook.SaveAs("C:\\test2.xlsx"));
        }
    }
}