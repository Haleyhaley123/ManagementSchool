using DoMain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoMainModel.Helper
{
    public class StaticSettings
    {
        // đây là biến static sẽ được lưu ở trên ram khi chương trình được khởi tạo
        // nó sẽ lưu mãi giá trị của biến này cho đến khi mình  tắt chương trình
        // vì vậy thông tin gì cần lưu trữ thì mình lưu vào biến static này, khi cần dùng gọi ra để lấy giá trị thôi
        // thường thì sẽ lưu những loại gì vào cái này,ví dụ user tài khoản
        // cái nào cần lưu thì lưu, những cái mà chỉ cần query 1 lần mà lại sử dụng nhiều thì lưu vào
        // mà nó lại ko mang tính chất thay đổi nhiều

        public static UserName User { get; set; }
    }
    
}
