/// DATRUONG - 02/02/2021
/// CREATE
//using RTCVision2101.Consts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoftware
{
    public class cSQLite
    {
        /// <summary>
        /// Tên tệp dữ liệu
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Message lỗi trả ra
        /// </summary>
        public string ErrMessage { get; set; }
        /// <summary>
        /// Báo đang connect hay không
        /// </summary>
        public bool IsConnected { get; set; }
        /// <summary>
        /// Connection SQLite
        /// </summary>
        public SQLiteConnection Conn { get; set; }
        public SQLiteTransaction Trans { get; set; }
        public bool IsUseTransaction { get; set; }
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="_FileName">Tên tệp dữ liệu</param>
        public cSQLite(string _FileName)
        {
            FileName = _FileName;
            ErrMessage = string.Empty;
            Trans = null;
            IsUseTransaction = false;
        }
        /// <summary>
        /// Mở kết nối
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            try
            {
                ErrMessage = string.Empty;
                if (IsConnected) return IsConnected;

                Conn = new SQLiteConnection("Data Source=" + FileName + ";Version=3;");
                Conn.Open();
                IsConnected = true;
            }
            catch (Exception ex)
            {
                ErrMessage = ex.Message;
                //GlobalFuntion.SaveErr(ex);
                IsConnected = false;
            }
            return IsConnected;
        }
        /// <summary>
        /// Đóng kết nối
        /// </summary>
        public void Close()
        {
            try
            {
                ErrMessage = string.Empty;
                if (Conn != null && IsConnected)
                {
                    Conn.Close();
                    Conn.Dispose();
                    Conn = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                IsConnected = false;
            }
            catch (Exception ex)
            {
                ErrMessage = ex.Message;
                //GlobalFuntion.SaveErr(ex);
            }
        }
        /// <summary>
        /// Thực hiện execute 1 câu lệnh, trả về integer
        /// </summary>
        /// <param name="sqlQuery">Chuỗi câu lệnh SQL</param>
        /// <returns>Trả về giá trị cần lấy kiểu INT</returns>
        public int ExecuteScalar_Int(string sqlQuery)
        {
            int Result = 0;
            try
            {
                if (!Connect())
                {
                    return Result;
                }
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = Conn;
                cmd.CommandText = sqlQuery;//"delete from Student where ID = 0";
                if (IsUseTransaction) cmd.Transaction = Trans;
                object value = cmd.ExecuteScalar();
                if (value != null && value.ToString() != "")
                {
                    if (int.TryParse(value.ToString(), out Result))
                    {
                        return Result;
                    }
                }

                return Result;
            }
            catch (Exception)
            {

                return 0;
            }
        }
        /// <summary>
        /// Thực hiện execute 1 câu lệnh, trả về string
        /// </summary>
        /// <param name="sqlQuery">Chuỗi câu lệnh SQL</param>
        /// <returns>Trả về giá trị cần lấy kiểu string</returns>
        public string ExecuteScalar_String(string sqlQuery)
        {
            string Result = string.Empty;
            try
            {
                if (!Connect())
                {
                    return Result;
                }
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = Conn;
                cmd.CommandText = sqlQuery;//"delete from Student where ID = 0";
                if (IsUseTransaction) cmd.Transaction = Trans;
                object value = cmd.ExecuteScalar();
                if (value != null && value.ToString() != "")
                {
                    Result = value.ToString();
                }

                return Result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Thực thi một câu lệnh SQL
        /// </summary>
        /// <param name="sqlQuery">Chuỗi câu lệnh SQL</param>
        public object ExecuteScalar(string sqlQuery)
        {
            if (!Connect())
            {
                return null;
            }
            try
            {
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = Conn;
                cmd.CommandText = sqlQuery;//"delete from Student where ID = 0";
                if (IsUseTransaction) cmd.Transaction = Trans;
                object value = cmd.ExecuteScalar();
                return value;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Thực hiện execute 1 câu lệnh
        /// </summary>
        /// <param name="sqlQuery">Chuỗi câu lệnh SQL</param>
        /// <returns>True: Thành công; False: Thất bại</returns>
        public bool ExecuteQuery(string sqlQuery)
        {
            try
            {
                if (!Connect())
                    return false;
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = Conn;
                cmd.CommandText = sqlQuery;
                if (IsUseTransaction) cmd.Transaction = Trans;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsTableExists(string tableName)
        {
            try
            {
                if (!Connect())
                    return false;
                string value = ExecuteScalar_String("SELECT name FROM sqlite_master WHERE type='table' AND name='" +
                                              tableName + "';");
                if (!string.IsNullOrEmpty(value))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy 1 bảng dữ liệu
        /// </summary>
        /// <param name="sqlQuery">Câu lệnh lấy bảng dữ liệu</param>
        /// <returns>Bảng dữ liệu cần lấy - NULL nếu lỗi</returns>
        public DataTable GetDataTable(string sqlQuery)
        {
            DataSet ds = new DataSet();
            DataTable dt = null;
            try
            {
                if (!Connect())
                    return dt;
                SQLiteDataAdapter da = new SQLiteDataAdapter(sqlQuery, Conn);
                da.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }
        /// <summary>
        /// Xóa dữ liệu 1 bảng
        /// </summary>
        /// <param name="tableName">Tên bảng cần xóa</param>
        /// <returns></returns>
        public bool DeleteDataTable(string tableName)
        {
            try
            {
                if (!Connect())
                    return false;
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = Conn;
                cmd.CommandText = "DELETE FROM " + tableName;
                if (IsUseTransaction) cmd.Transaction = Trans;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Xóa dữ liệu nhiều bảng
        /// </summary>
        /// <param name="tableNames">Danh sách bảng cần xóa dữ liệu</param>
        /// <returns></returns>
        public bool DeleteDataMultiTable(string[] tableNames)
        {
            foreach (string tableName in tableNames)
            {
                if (!DeleteDataTable(tableName))
                    return false;
            }

            return true;
        }
        //Các hàm dùng để chỉnh sửa dữ liệu từ 1 ROW
        /// <summary>
        /// Thêm 1 hàng dữ liệu
        /// </summary>
        /// <param name="Row">Hàng cần thêm</param>
        /// <returns>
        /// <para>True: Thành công</para>
        /// <para>False: Thất bại</para>
        /// </returns>
        public bool AddRow(DataRow Row)
        {
            try
            {
                bool Result = false;

                if (Row == null || !Connect())
                    return Result;
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = Conn;

                string sSQL = "INSERT INTO " + Row.Table.TableName + " VALUES(";
                foreach (DataColumn item in Row.Table.Columns)
                {
                    if (item.Caption == cColName.Unbound)
                        continue;
                    sSQL = sSQL + "@" + item.ColumnName + ",";
                    cmd.Parameters.AddWithValue(item.ColumnName, Row[item]);
                }
                cmd.CommandText = sSQL.Substring(0, sSQL.Length - 1) + ")";
                if (IsUseTransaction) cmd.Transaction = Trans;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        /// <summary>
        /// Sửa 1 dòng trong bảng
        /// </summary>
        /// <param name="Row">Dòng dữ liệu</param>
        /// <returns>True: Thành công; False: Thất bại</returns>
        public bool EditRow(DataRow Row)
        {
            try
            {
                bool Result = false;

                if (Row == null || !Connect())
                    return Result;
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = Conn;

                string sSQL = "UPDATE " + Row.Table.TableName + " SET ";
                string sSQLWHERE = "";
                foreach (DataColumn item in Row.Table.Columns)
                {
                    if (item.Caption == cColName.Unbound)
                        continue;
                    else if (item.Caption == cColName.KEY)
                    {
                        if (sSQLWHERE == "")
                            sSQLWHERE = " WHERE " + item.ColumnName + "=@" + item.ColumnName;
                        else
                            sSQLWHERE = " AND " + item.ColumnName + "=@" + item.ColumnName;
                        cmd.Parameters.AddWithValue(item.ColumnName, Row[item]);
                        continue;
                    }
                    sSQL = sSQL + item.ColumnName + "=@" + item.ColumnName + ",";
                    cmd.Parameters.AddWithValue(item.ColumnName, Row[item]);
                }
                cmd.CommandText = sSQL.Substring(0, sSQL.Length - 1) + sSQLWHERE;
                if (IsUseTransaction) cmd.Transaction = Trans;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// Xóa 1 dòng trong bảng
        /// </summary>
        /// <param name="Row">Dòng dữ liệu</param>
        /// <returns>True: Thành công; False: Thất bại</returns>
        public bool DeleteRow(DataRow Row)
        {
            try
            {
                bool Result = false;

                if (Row == null || !Connect())
                    return Result;
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = Conn;

                string sSQL = "DELETE FROM " + Row.Table.TableName + " ";
                string sSQLWHERE = "";
                foreach (DataColumn item in Row.Table.Columns)
                {
                    if (item.Caption == cColName.KEY)
                    {
                        if (sSQLWHERE == "")
                            sSQLWHERE = "WHERE " + item.ColumnName + "=@" + item.ColumnName;
                        else
                            sSQLWHERE = " AND " + item.ColumnName + "=@" + item.ColumnName;
                        cmd.Parameters.AddWithValue(item.ColumnName, Row[item]);
                        continue;
                    }
                }
                cmd.CommandText = sSQL + sSQLWHERE;
                if (IsUseTransaction) cmd.Transaction = Trans;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        /// <summary>
        /// Xóa 1 dòng trong bảng
        /// </summary>
        /// <param name="Row">Dòng dữ liệu</param>
        /// <returns>True: Thành công; False: Thất bại</returns>
        public bool AddCol(string tableName, string columnName, string type)
        {
            try
            {
                bool Result = false;

                if (!Connect())
                    return Result;
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = Conn;

                string sSQL = string.Format("ALTER TABLE {0} ADD {1} {2}", tableName, columnName, type);
                cmd.CommandText = sSQL;
                if (IsUseTransaction) cmd.Transaction = Trans;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// Khởi tạo Transaction
        /// </summary>
        public bool StartTransaction()
        {
            try
            {
                if (!IsConnected || IsUseTransaction) return IsUseTransaction;
                Trans = Conn.BeginTransaction();
                IsUseTransaction = true;
            }
            catch
            {
                IsUseTransaction = false;
            }

            return IsUseTransaction;
        }
        /// <summary>
        /// Commit dữ liệu
        /// </summary>
        public void Commit()
        {
            try
            {
                if (!IsConnected || !IsUseTransaction)
                {
                    IsUseTransaction = false;
                    return;
                }
                Trans.Commit();
                IsUseTransaction = false;
            }
            catch
            {
                IsUseTransaction = false;
            }
        }
        /// <summary>
        /// RollBack dữ liệu
        /// </summary>
        public void RollBack()
        {
            try
            {
                if (!IsConnected || !IsUseTransaction) return;
                Trans.Rollback();
                IsUseTransaction = false;
            }
            catch
            {
                IsUseTransaction = false;
            }
        }
    }
}
