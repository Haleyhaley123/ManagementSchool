using Dapper;
using DoMainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectSqlDapper
{
    public class PasswordResetConnectSQL
    {
        public static bool PassWordReset(string userName, string PasswordHash, string Email, string NumberPhone)
        {
            PasswordReset user1 = new PasswordReset();
            user1.UserName1 = userName;
            user1.Password1 = PasswordHash;
            user1.Email1 = Email;
            user1.Phone = NumberPhone;


            var param = new DynamicParameters();
            param.Add("@UserName", user1.UserName1, System.Data.DbType.String);
            param.Add("@PasswordHash",user1.Password1, System.Data.DbType.String);
            param.Add("@Email", user1.Email1, System.Data.DbType.String);
            param.Add("@NumberPhone", user1.Phone, System.Data.DbType.String);
            string sql = "InsertPassReset";
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
