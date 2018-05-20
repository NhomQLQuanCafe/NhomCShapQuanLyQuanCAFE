using ProjectQLQuanCafe.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQLQuanCafe.BLL
{
    class OrderDetail
    {
        DataProvider da = new DataProvider();
        public void DeleteOrderDetailByFoodID(int id)
        {
            string sql = "Delete OrderDetail Where idFood = '" + id + "'";
            da.UnGetTable(sql);
        }
    }
}
