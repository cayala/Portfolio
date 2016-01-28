using PreProcessPeggedToModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace PreProcessPeggedToModel.Repo
{
    public class SqlHelper
    {
        private string connectionTemplate = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";

        private const string getDBQuery = 
            #region Get Database Query

                                 @"USE [ARI_PartStream]
                                  SELECT 
                                  [catalog], 
                                  [Server], 
                                  [port], 
                                  [userId], 
                                  [password] 
                                  FROM [PartDatabases] 
                                  WHERE Server = 'CYP-CAY-W7'
                                  AND [Active] = 1";
          
            #endregion

        private const string checkTableQuery = 
        #region

 @"IF(EXISTS(
             SELECT * 
             FROM INFORMATION_SCHEMA.TABLES 
             WHERE TABLE_NAME = 'ModelToPart'))
    BEGIN
    SELECT CAST(1 as bit)
    END
    ELSE
    BEGIN
    SELECT CAST(0 as BIT)
    END";

#endregion

        private const string createTableQuery =
        #region

             @"CREATE TABLE PeggedToAssembly
             (
                pegged_id int not null,
                assembly_id int not null
             )";
       #endregion

        private const string getIds =
        #region
        @"SELECT 
        p.pegged_id,
        a.assembly_id 
        FROM pegged p 
        join catalog_assembly ca 
        on p.assembly_id = ca.assembly_id
        join catalog_assembly ca1
        on ca1.assembly_id = ca.parent_assembly_id
        join assembly a
        on a.assembly_id = ca1.assembly_id";
        #endregion

        private string insertIds =
        #region
            @"INSERT INTO [partTable]
              VALUES (@partId, @assemblyId)";
        #endregion

        public List<Database> GetDataBases() 
        {
            string connectionString = ConfigurationSettings.AppSettings["ConnectionString"];
            List<Database> dbs = new List<Database>();

            using(SqlConnection conn = new SqlConnection(connectionString)) 
            {
                SqlCommand cmd = new SqlCommand(getDBQuery, conn);

                try
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) 
                    {
                        dbs.Add(new Database {
                            Catalog = reader.GetString(0),
                            Server = reader.GetString(1),
                            Port = reader.GetInt32(2),
                            UserId = reader.GetString(3),
                            Password = reader.GetString(4)
                        });
                    }

                    reader.Close();
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex);
                }
            }

            return dbs;
        }

        public bool CheckIfTableExists(Database db) 
        {
            string cs = CreateConnectionString(db);
            bool response = false; 

            using(SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(checkTableQuery, con);
                try
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                       response = reader.GetBoolean(0);
                    }

                    con.Close();
                }

                catch (Exception ex) 
                {
                    Console.WriteLine(ex);
                }
            }

            return response;
        }

        public void CreateTable(Database db) 
        {
            //Create datatable for bulk copy
            DataTable dt = new DataTable();

            dt.Columns.Add("pegged_id");
            dt.Columns.Add("assembly_id");

            string cs = CreateConnectionString(db);
            //create table and get ids
            
            using (SqlConnection con = new SqlConnection(cs)) 
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(createTableQuery, con);

                    con.Open();

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = getIds;

                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();
                    //add rows to data table for bulk insert
                    while (reader.Read())
                    {
                        dt.Rows.Add(reader.GetInt32(0), reader.GetInt32(1));
                    }

                    con.Close();
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex);
                }
            }


            //create table and insert data
            using (SqlBulkCopy sbc = new SqlBulkCopy(cs)) 
            {
                try
                {
                    sbc.DestinationTableName = "PeggedToAssembly";

                    sbc.BatchSize = dt.Rows.Count;

                    sbc.ColumnMappings.Add("pegged_id", "pegged_id");
                    sbc.ColumnMappings.Add("assembly_id", "assembly_id");

                    sbc.SqlRowsCopied += new SqlRowsCopiedEventHandler(SqlBatchInsertStatus);

                    sbc.WriteToServer(dt);

                    sbc.Close();
                }

                catch (Exception ex) 
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public void SqlBatchInsertStatus(object sender, SqlRowsCopiedEventArgs e) 
        {
            Console.WriteLine("Batch insert completed." + Environment.NewLine + String.Format("Copied {0}", e.RowsCopied));
        }

        public string CreateConnectionString(Database db) 
        {
            return String.Format(connectionTemplate, db.Server, db.Catalog, db.UserId, db.Password);
        }
    }
}
