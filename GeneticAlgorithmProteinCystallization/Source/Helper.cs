using GeneticAlgorithm.SourceCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace GeneticAlgorithm
{
    public class Helper
    {
        private OleDbConnection conn;
        private String strFileType = ".xlsx";
        public static DataSet inputScreenFile;
        public static DataTable rangeOfScreensTable;
        public static List<String> badCom1 = new List<string>();
        public static List<String> badCom2 = new List<string>();
        public static List<String> noAnions = new List<string>();
        public static List<String> anions = new List<string>();
        public static List<String> cations = new List<string>();

        public Helper()
        {
            readListOfBadCombinations();
            readListOfNoAnionReagents();
            readListOfAnions();
        }

        private void readListOfNoAnionReagents()
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader("noAnions.txt");
            while ((line = file.ReadLine()) != null)
            {
                noAnions.Add(line.ToUpper());
            }
            file.Close();
        }

        private void readListOfAnions()
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader("anionList.txt");
            while ((line = file.ReadLine()) != null)
            {
                anions.Add(line.ToUpper());
            }
            file.Close();
        }

        private void readListOfCations()
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader("cationList.txt");
            while ((line = file.ReadLine()) != null)
            {
                cations.Add(line.ToUpper());
            }
            file.Close();
        }

        private void readListOfBadCombinations()
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader("listOfC1.txt");
            while ((line = file.ReadLine()) != null)
            {
                badCom1.Add(line.ToUpper());

            }

            file.Close();

            file = new System.IO.StreamReader("listOfC2.txt");
            while ((line = file.ReadLine()) != null)
            {
                badCom2.Add(line.ToUpper());

            }

            file.Close();

        }
        private void getRangeOfScreens(int lowestScore, int highestScore)
        {

            if (inputScreenFile.Tables.Count != 0)
            {

                IEnumerable<DataRow> rangeOfScreens = (from tuple in inputScreenFile.Tables[0].AsEnumerable()
                                                       where ((tuple.Field<double>("S_a") > lowestScore & tuple.Field<double>("S_a") < highestScore) ||
                                      (tuple.Field<double>("S_b") > lowestScore & tuple.Field<double>("S_b") < highestScore) ||
                                      (tuple.Field<double>("S_c") > lowestScore & tuple.Field<double>("S_c") < highestScore)) & tuple.Field<string>("C3_Anion") == ""
                                                       select tuple).ToList();

                if (rangeOfScreens.Any())
                    rangeOfScreensTable = rangeOfScreens.CopyToDataTable<DataRow>();

            }

        }

        public void ReadExcelFile(string filePath, int lowestScore, int highestScore)
        {

            String connString = "";
            //Connection String to Excel Workbook
            if (strFileType.Trim() == ".xls")
            {
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (strFileType.Trim() == ".xlsx")
            {
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            // String query = "SELECT Distinct Ph, C1_Anion,C1_Cation,C2_Anion,C2_Cation FROM [Sayfa1$] where (S_a>3 OR S_b>3 OR S_c>3) AND C3_Anion IS NULL ";

            String queryAllTable = "SELECT Distinct * FROM [Sheet1$] ";


            conn = new OleDbConnection(connString);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            OleDbCommand cmd = new OleDbCommand(queryAllTable, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            inputScreenFile = new DataSet();


            da.Fill(inputScreenFile);

            //trimming and make all data uppercase
            foreach (DataTable dt in inputScreenFile.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.DataType == typeof(System.String))
                        {
                            dr[col] = dr[col].ToString().Trim().ToUpper();
                        }
                    }
                }
            }

            da.Dispose();
            getRangeOfScreens(lowestScore, highestScore);
            cmd = null;
            conn.Close();
        }

        public DataSet ReadExcelFile(string filePath)
        {

            String connString = "";
            //Connection String to Excel Workbook
            if (strFileType.Trim() == ".xls")
            {
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (strFileType.Trim() == ".xlsx")
            {
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            // String query = "SELECT Distinct Ph, C1_Anion,C1_Cation,C2_Anion,C2_Cation FROM [Sayfa1$] where (S_a>3 OR S_b>3 OR S_c>3) AND C3_Anion IS NULL ";

            String queryAllTable = "SELECT Distinct * FROM [Sheet1$] ";


            conn = new OleDbConnection(connString);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            OleDbCommand cmd = new OleDbCommand(queryAllTable, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            inputScreenFile = new DataSet();


            da.Fill(inputScreenFile);

            //trimming and make all data uppercase
            foreach (DataTable dt in inputScreenFile.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.DataType == typeof(System.String))
                        {
                            dr[col] = dr[col].ToString().Trim().ToUpper();
                        }
                    }
                }
            }

            da.Dispose();
            //getRangeOfScreens(lowestScore, highestScore);
            cmd = null;
            conn.Close();
            return inputScreenFile;
        }

        public DataSet ReadExcelFileCompare(string filePath)
        {

            String connString = "";
            //Connection String to Excel Workbook
            if (strFileType.Trim() == ".xls")
            {
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (strFileType.Trim() == ".xlsx")
            {
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            // String query = "SELECT Distinct Ph, C1_Anion,C1_Cation,C2_Anion,C2_Cation FROM [Sayfa1$] where (S_a>3 OR S_b>3 OR S_c>3) AND C3_Anion IS NULL ";

            String queryAllTable = "SELECT Distinct * FROM [LIST_OF_CANDIDATES$] ";


            conn = new OleDbConnection(connString);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            OleDbCommand cmd = new OleDbCommand(queryAllTable, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            inputScreenFile = new DataSet();


            da.Fill(inputScreenFile);

            //trimming and make all data uppercase
            foreach (DataTable dt in inputScreenFile.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.DataType == typeof(System.String))
                        {
                            dr[col] = dr[col].ToString().Trim().ToUpper();
                        }
                    }
                }
            }

            da.Dispose();
            //getRangeOfScreens(lowestScore, highestScore);
            cmd = null;
            conn.Close();
            return inputScreenFile;
        }


        public bool WriteExcelFile(DataTable Tbl, string ExcelFilePath = null)
        {
            if (File.Exists(ExcelFilePath))
            {
                File.Delete(ExcelFilePath);
            }
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + ";Extended Properties=\"Excel 12.0 XML;HDR=Yes;IMEX=0\"";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                #region WriteCandidateCocktails

                //cmd.CommandText = @"CREATE TABLE [LIST_OF_CANDIDATES] (Well_Id VARCHAR,
                //B_Anion	VARCHAR,B_Cation	VARCHAR,Ph	VARCHAR,B_Conc	VARCHAR,
                //C1_Anion	VARCHAR, C1_Cation	VARCHAR, C1_Conc	VARCHAR, C1_M	VARCHAR, C1_Ph	VARCHAR,
                //C2_Anion	VARCHAR, C2_Cation	VARCHAR, C2_Conc	VARCHAR, C2_M	VARCHAR, C2_Ph	VARCHAR,
                //C3_Anion	VARCHAR, C3_Cation	VARCHAR, C3_Conc	VARCHAR, C3_M	VARCHAR, C3_Ph	VARCHAR,
                //C4_Anion	VARCHAR, C4_Cation	VARCHAR, C4_Conc	VARCHAR, C4_M	VARCHAR, C4_Ph	VARCHAR,
                //C5_Anion	VARCHAR, C5_Cation	VARCHAR, C5_Conc	VARCHAR, C5_M	VARCHAR, C5_Ph	VARCHAR,
                //S_a	DOUBLE, S_b	DOUBLE, S_c	DOUBLE, Rank	DOUBLE);";
                cmd.CommandText = @"CREATE TABLE [LIST_OF_CANDIDATES] (Well_Id VARCHAR,
                B_Anion	VARCHAR,B_Cation	VARCHAR,Ph	VARCHAR,B_Conc	VARCHAR,
                C1_Anion	VARCHAR, C1_Cation	VARCHAR, C1_Conc	VARCHAR, C1_M	VARCHAR, C1_Ph	VARCHAR,
                C2_Anion	VARCHAR, C2_Cation	VARCHAR, C2_Conc	VARCHAR, C2_M	VARCHAR, C2_Ph	VARCHAR,
                C3_Anion	VARCHAR, C3_Cation	VARCHAR, C3_Conc	VARCHAR, C3_M	VARCHAR, C3_Ph	VARCHAR,
                C4_Anion	VARCHAR, C4_Cation	VARCHAR, C4_Conc	VARCHAR, C4_M	VARCHAR, C4_Ph	VARCHAR,
                C5_Anion	VARCHAR, C5_Cation	VARCHAR, C5_Conc	VARCHAR, C5_M	VARCHAR, C5_Ph	VARCHAR,
                S_a	DOUBLE, S_b	DOUBLE, S_c	DOUBLE, Rank	DOUBLE);";

                cmd.ExecuteNonQuery();

                for (int i = 0; i < Tbl.Rows.Count; i++)
                {
                    //string command = @"INSERT INTO [LIST_OF_CANDIDATES] (Ph,B_Anion,B_Conc,C1_Anion,C1_Conc,C2_Anion,C2_Conc,Rank)
                    //                VALUES(";
                    string command = @"INSERT INTO [LIST_OF_CANDIDATES] (Ph,B_Anion,B_Conc,C1_Anion,C1_Conc,C2_Anion,C2_Conc,Rank)
                                    VALUES(";

                    for (int j = 0; j < Tbl.Columns.Count; j++)
                    {

                        if (Tbl.Columns[j].DataType == typeof(string))
                        {
                            command = command + "'" + Tbl.Rows[i].ItemArray[j].ToString() + "',";
                        }
                        else
                        {
                            command = command + Tbl.Rows[i].ItemArray[j] + ")";
                        }
                    }
                    cmd.CommandText = command;
                    cmd.ExecuteNonQuery();
                }
                #endregion
                /*
                #region WriteListOfScores

                cmd.CommandText = @"CREATE TABLE [LIST_OF_SCORES] (Ph VARCHAR,
                Ph_Rank	VARCHAR,Precipitant	VARCHAR,Precipitant_Rank	VARCHAR,Salt	VARCHAR,
                Salt_Rank	VARCHAR);";

                cmd.ExecuteNonQuery();


                DataTable dtScores = GetListOfScores();
                foreach (DataRow dr in dtScores.Rows)
                {

                    string commandListingScores = @"INSERT INTO [LIST_OF_SCORES] (Ph,Ph_Rank,Precipitant, Precipitant_Rank, Salt,Salt_Rank	) VALUES(";
                    foreach (var item in dr.ItemArray)
                    {
                        commandListingScores = commandListingScores + "'" + item.ToString() + "',";
                    }
                    commandListingScores = commandListingScores.Remove(commandListingScores.Length - 1) + ")";
                    cmd.CommandText = commandListingScores;
                    cmd.ExecuteNonQuery();
                }

                #endregion
                */
                conn.Close();
            }
            return true;
        }

        public bool WriteExcelFileCompare(DataTable Tbl, string ExcelFilePath = null)
        {
            if (File.Exists(ExcelFilePath))
            {
                File.Delete(ExcelFilePath);
            }
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + ";Extended Properties=\"Excel 12.0 XML;HDR=Yes;IMEX=0\"";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                #region WriteCandidateCocktails

                //cmd.CommandText = @"CREATE TABLE [LIST_OF_CANDIDATES] (Well_Id VARCHAR,
                //B_Anion	VARCHAR,B_Cation	VARCHAR,Ph	VARCHAR,B_Conc	VARCHAR,
                //C1_Anion	VARCHAR, C1_Cation	VARCHAR, C1_Conc	VARCHAR, C1_M	VARCHAR, C1_Ph	VARCHAR,
                //C2_Anion	VARCHAR, C2_Cation	VARCHAR, C2_Conc	VARCHAR, C2_M	VARCHAR, C2_Ph	VARCHAR,
                //C3_Anion	VARCHAR, C3_Cation	VARCHAR, C3_Conc	VARCHAR, C3_M	VARCHAR, C3_Ph	VARCHAR,
                //C4_Anion	VARCHAR, C4_Cation	VARCHAR, C4_Conc	VARCHAR, C4_M	VARCHAR, C4_Ph	VARCHAR,
                //C5_Anion	VARCHAR, C5_Cation	VARCHAR, C5_Conc	VARCHAR, C5_M	VARCHAR, C5_Ph	VARCHAR,
                //S_a	DOUBLE, S_b	DOUBLE, S_c	DOUBLE, Rank	DOUBLE);";
                cmd.CommandText = @"CREATE TABLE [LIST_OF_CANDIDATES] (Well_Id VARCHAR,
                B_Anion	VARCHAR,B_Cation	VARCHAR,Ph	VARCHAR,B_Conc	VARCHAR,
                C1_Anion	VARCHAR, C1_Cation	VARCHAR, C1_Conc	VARCHAR, C1_M	VARCHAR, C1_Ph	VARCHAR,
                C2_Anion	VARCHAR, C2_Cation	VARCHAR, C2_Conc	VARCHAR, C2_M	VARCHAR, C2_Ph	VARCHAR,
                C3_Anion	VARCHAR, C3_Cation	VARCHAR, C3_Conc	VARCHAR, C3_M	VARCHAR, C3_Ph	VARCHAR,
                C4_Anion	VARCHAR, C4_Cation	VARCHAR, C4_Conc	VARCHAR, C4_M	VARCHAR, C4_Ph	VARCHAR,
                C5_Anion	VARCHAR, C5_Cation	VARCHAR, C5_Conc	VARCHAR, C5_M	VARCHAR, C5_Ph	VARCHAR,
                S_a	VARCHAR, S_b	VARCHAR, S_c	VARCHAR, Rank	DOUBLE);";

                cmd.ExecuteNonQuery();

                for (int i = 0; i < Tbl.Rows.Count; i++)
                {
                    //string command = @"INSERT INTO [LIST_OF_CANDIDATES] (Ph,B_Anion,B_Conc,C1_Anion,C1_Conc,C2_Anion,C2_Conc,Rank)
                    //                VALUES(";
                    string command = @"INSERT INTO [LIST_OF_CANDIDATES] (Well_Id,
                B_Anion	,B_Cation	,Ph	,B_Conc	,
                C1_Anion	, C1_Cation	, C1_Conc	, C1_M	, C1_Ph	,
                C2_Anion	, C2_Cation	, C2_Conc	, C2_M	, C2_Ph	,
                C3_Anion	, C3_Cation	, C3_Conc	, C3_M	, C3_Ph	,
                C4_Anion	, C4_Cation	, C4_Conc	, C4_M	, C4_Ph	,
                C5_Anion	, C5_Cation	, C5_Conc	, C5_M	, C5_Ph	,
                S_a	, S_b	, S_c	, Rank	)
                                    VALUES(";

                    for (int j = 0; j < Tbl.Columns.Count; j++)
                    {

                        if (Tbl.Columns[j].DataType == typeof(string))
                        {
                            command = command + "'" + Tbl.Rows[i].ItemArray[j].ToString() + "',";
                        }
                        else
                        {
                            command = command + Tbl.Rows[i].ItemArray[j] + ")";
                        }
                    }
                    cmd.CommandText = command;
                    cmd.ExecuteNonQuery();
                }
                #endregion
                /*
                #region WriteListOfScores

                cmd.CommandText = @"CREATE TABLE [LIST_OF_SCORES] (Ph VARCHAR,
                Ph_Rank	VARCHAR,Precipitant	VARCHAR,Precipitant_Rank	VARCHAR,Salt	VARCHAR,
                Salt_Rank	VARCHAR);";

                cmd.ExecuteNonQuery();


                DataTable dtScores = GetListOfScores();
                foreach (DataRow dr in dtScores.Rows)
                {

                    string commandListingScores = @"INSERT INTO [LIST_OF_SCORES] (Ph,Ph_Rank,Precipitant, Precipitant_Rank, Salt,Salt_Rank	) VALUES(";
                    foreach (var item in dr.ItemArray)
                    {
                        commandListingScores = commandListingScores + "'" + item.ToString() + "',";
                    }
                    commandListingScores = commandListingScores.Remove(commandListingScores.Length - 1) + ")";
                    cmd.CommandText = commandListingScores;
                    cmd.ExecuteNonQuery();
                }

                #endregion
                */
                conn.Close();
            }
            return true;
        }


        /*
        private DataTable GetListOfScores()
        {
            DataTable dtScores = new DataTable();
            dtScores.Columns.Add("Ph", typeof(string));
            dtScores.Columns.Add("PhRank", typeof(string));
            dtScores.Columns.Add("Prep", typeof(string));
            dtScores.Columns.Add("PrepRank", typeof(string));
            dtScores.Columns.Add("Salt", typeof(string));
            dtScores.Columns.Add("SaltRank", typeof(string));

            int maxLenght = 0, index = 0;
            var scoresOfPhs = AED.scoresOfPhs.OrderByDescending(e => e.Value).ToList();
            var scoresOfPreps = AED.scoresOfPreps.OrderByDescending(e => e.Value).ToList();
            var scoresOfSalts = AED.scoresOfSalts.OrderByDescending(e => e.Value).ToList();
            maxLenght = Math.Max(Math.Max(scoresOfPhs.Count, scoresOfPreps.Count), scoresOfSalts.Count);

            while (index < maxLenght)
            {
                DataRow dr = dtScores.NewRow();

                if (index < scoresOfPhs.Count)
                {
                    dr["Ph"] = scoresOfPhs[index].Key;
                    dr["PhRank"] = scoresOfPhs[index].Value.ToString();
                }
                else
                {
                    dr["Ph"] = "";
                    dr["PhRank"] = "";
                }

                if (index < scoresOfPreps.Count)
                {
                    dr["Prep"] = scoresOfPreps[index].Key;
                    dr["PrepRank"] = scoresOfPreps[index].Value.ToString();
                }
                else
                {
                    dr["Prep"] = "";
                    dr["PrepRank"] = "";
                }

                if (index < scoresOfSalts.Count)
                {
                    dr["Salt"] = scoresOfSalts[index].Key;
                    dr["SaltRank"] = scoresOfSalts[index].Value.ToString();
                }
                else
                {
                    dr["Salt"] = "";
                    dr["SaltRank"] = "";
                }
                index++;
                dtScores.Rows.Add(dr);
            }

            return dtScores;
        }
        */


        public bool ExportToExcel(DataTable Tbl, string ExcelFilePath = null)
        {
            try
            {
                if (Tbl == null || Tbl.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;

                // column headings
                for (int i = 0; i < Tbl.Columns.Count; i++)
                {
                    workSheet.Cells[1, (i + 1)] = Tbl.Columns[i].ColumnName;
                }

                // rows
                for (int i = 0; i < Tbl.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (int j = 0; j < Tbl.Columns.Count; j++)
                    {
                        workSheet.Cells[(i + 2), (j + 1)] = Tbl.Rows[i][j];
                    }
                }

                // check fielpath
                if (ExcelFilePath != null && ExcelFilePath != "")
                {
                    try
                    {
                        workSheet.SaveAs(ExcelFilePath);
                        excelApp.Quit();

                    }
                    catch (Exception ex)
                    {
                        return false;
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                            + ex.Message);
                    }
                }
                else    // no filepath is given
                {
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("ExportToExcel: \n" + ex.Message);

            }

            conn.Close();
            conn.Dispose();
            return true;
        }
    }
}
