using ConnectSqlDapper.Helper;
using Dapper;
using DoMain;
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
    public class PeopleConnectSQL : BaseConnectSQL
    {

        public static List<People> GetListPeople()
        {
            string query = "select ID, Name, Gender, Age, Department, Birthday, NumberPhone from ListData";
            var result = GetListPeople<People>(query);
            return result;
        }
        public static List<People> GetListStudent()
        {
            string query = "select ID, Name, Gender, Age, Department, Birthday, NumberPhone from ListData where Department = 'Học sinh' ";
            var result = GetListPeople<People>(query);
            return result;
        }
        public static List<People> GetListTeacher()
        {
            string query = "select ID, Name, Gender, Age, Department, Birthday, NumberPhone from ListData where Department = 'Giáo viên' ";
            var result = GetListPeople<People>(query);
            return result;
        }
        public static List<People> GetListEmployee()
        { 
            string query = "select ID, Name, Gender, Age, Department, Birthday, NumberPhone from ListData where Department = 'Nhân viên' ";
            var result = GetListPeople<People>(query);
            return result;
        }
       
        public static bool SetListPeople(string ID, string Name, string Gender, int Age, string Department, DateTime Birthday, string NumberPhone)
        {
            if (StaticSettings.User == null || StaticSettings.User.PK_ID == null)
            {
                return false;
            }
            People people = new People();
            people.ID = ID;
            people.Name = Name;
            people.Gender = Gender;
            people.Age = Age;
            people.Department = Department;
            people.Birthday = Birthday;
            people.NumberPhone = NumberPhone;

            var paramT = new DynamicParameters();
            paramT.Add("@ID", people.ID, System.Data.DbType.String);
            paramT.Add("@Name", people.Name, System.Data.DbType.String);
            paramT.Add("@Gender", people.Gender, System.Data.DbType.String);
            paramT.Add("@Age", people.Age, System.Data.DbType.Int32);
            paramT.Add("@Department", people.Department, System.Data.DbType.String);
            paramT.Add("@Birthday", people.Birthday, System.Data.DbType.DateTime);
            paramT.Add("@NumberPhone", people.NumberPhone, System.Data.DbType.String);
            paramT.Add("@PK_ID", StaticSettings.User.PK_ID, System.Data.DbType.Int32);



            string sqlPeople = "DataListInsert";
            int Result = -100;
            try
            {
                var sReader = DapperManagerSQL.Instance.Conn.ExecuteReader(sqlPeople, paramT, commandType: CommandType.StoredProcedure);
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
