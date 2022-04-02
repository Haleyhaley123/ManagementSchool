using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectSqlDapper.Helper
{
    public class BaseConnectSQL
    {
        // kiểu T là đối tượng m sẽ truyền  vào khi gọi hàm
        // ví dụ thằng data gọi hàm này thì truyền  đối tượng là peopel vào
        public static List<T> GetListPeople<T>(string query)
        {
            List<T> result = new List<T>();
            try
            {
                // cái này là lấy ra 1 danh sách
                result = DapperManagerSQL.Instance.Conn.Query<T>(query).ToList();

            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
