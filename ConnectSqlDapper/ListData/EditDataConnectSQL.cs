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
    public class EditDataConnectSQL
    {
       
        public static bool EditData(string ID, string Name, string Gender, int Age, string Department, DateTime Birthday, string NumberPhone)
        {
            if (StaticSettings.User == null || StaticSettings.User.PK_ID == null)
            {
                return false;
            }
            EditData Edit = new EditData();
            Edit.ID = ID;
            Edit.Name = Name;
            Edit.Gender = Gender;
            Edit.Age = Age;
            Edit.Department = Department;
            Edit.Birthday = Birthday;
            Edit.NumberPhone = NumberPhone;

            var paramT = new DynamicParameters();
            paramT.Add("@ID", Edit.ID, System.Data.DbType.String);
            paramT.Add("@Name", Edit.Name, System.Data.DbType.String);
            paramT.Add("@Gender", Edit.Gender, System.Data.DbType.String);
            paramT.Add("@Age", Edit.Age, System.Data.DbType.Int32);
            paramT.Add("@Department", Edit.Department, System.Data.DbType.String);
            paramT.Add("@Birthday", Edit.Birthday, System.Data.DbType.DateTime);
            paramT.Add("@NumberPhone", Edit.NumberPhone, System.Data.DbType.String);
         
            string sqlEdit = "EditData";
            int Result = -100;
            try
            {
                var sReader = DapperManagerSQL.Instance.Conn.ExecuteReader(sqlEdit, paramT, commandType: CommandType.StoredProcedure);
                while (sReader.Read())
                {
                    // chỉ trường được duy nhất SQL trả về Result = 1
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
