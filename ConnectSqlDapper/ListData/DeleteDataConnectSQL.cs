using Dapper;
using DoMainModel.DoMain;
using DoMainModel.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectSqlDapper
{
    public class DeleteDataConnectSQL
    {
        public static bool DeleteData(string ID)
        {
            if (StaticSettings.User == null || StaticSettings.User.PK_ID == null)
            {
                return false;
            }
            DeleteData delete = new DeleteData();
            delete.ID = ID;

            var param = new DynamicParameters();
            param.Add("@ID", delete.ID, System.Data.DbType.String);
            string sql = "DeleteData";
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
