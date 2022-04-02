using ConnectSqlDapper.Helper;
using Dapper;
using DoMain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ConnectSqlDapper
{
    public class UserConnectSQL : BaseConnectSQL
    {
        // hàm này là lấy ra 1 thằng thôi, nên m phải .FirstOrDefault();
        public static UserName GetUserName(string userName)
        {

            string query = string.Format("SELECT * from UserName1 where UserName = '{0}'", userName);
            var result = GetListPeople<UserName>(query).FirstOrDefault();
            return result;
        }
        public static bool CreateUserName(string userName , string PasswordHash, string Email, string NumberPhone)
        {
            UserName user = new UserName();
            user.userName = userName;
            user.PasswordHash = PasswordHash;
            user.Email = Email;
            user.NumberPhone = NumberPhone;

            var param = new DynamicParameters();
            param.Add("@UserName", user.userName, System.Data.DbType.String);
            param.Add("@PasswordHash", user.PasswordHash, System.Data.DbType.String);
            param.Add("@Email", user.Email, System.Data.DbType.String);
            param.Add("@NumberPhone", user.NumberPhone, System.Data.DbType.String);
            string sql = "InsertUserName";
            int Result = -100;
            try
            {
                var sReader = DapperManagerSQL.Instance.Conn.ExecuteReader(sql, param, commandType: CommandType.StoredProcedure);
                while (sReader.Read())
                {
                    Result = sReader["ResultSql"] == null ? 0 : int.Parse(sReader["ResultSql"].ToString());
                    //SELECT @ResultSql AS ResultSql dưới store
                    // Result là cái kết quả m t được trả khi nó  truy vấn xuống store
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return Result > 0; // theo logic t viết dưới đó , nghĩa là khi t inser thành công thì nó trả về =1
                               // các trường hợp còn lại là -400 lỗi hệ thống, và = 0 là đã tồn tại
                               // tạm thời t kiểm tra xem nó có lớn hơn 0, nếu lớn hơn trả về true
        }
    }
}
