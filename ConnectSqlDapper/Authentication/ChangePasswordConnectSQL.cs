using Dapper;
using DoMain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DoMainModel;

namespace ConnectSqlDapper
{
    public class ChangePasswordConnectSQL
    {
        public static bool ChangePassWord(ChangePassWord changePass) // m nên truyền cả một đối tượng sang
        {
            
            var param = new DynamicParameters();
            param.Add("@PK_ID", changePass.PK_ID, System.Data.DbType.Int32);
            param.Add("@NewPasswordHash", changePass.NewPasswordHash, System.Data.DbType.String);
            string sql = "InsertChangePass";
            int Result = -100;
            try
            {
                var sReader = DapperManagerSQL.Instance.Conn.ExecuteReader(sql, param, commandType: CommandType.StoredProcedure);
                while (sReader.Read())
                {
                    Result = sReader["ResultSql"] == null ? 0 : int.Parse(sReader["ResultSql"].ToString());
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return Result > 0;
        }
    }

}
