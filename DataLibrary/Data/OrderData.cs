using Dapper;
using DataLibrary.Db;
using DataLibrary.Interface;
using DataLibrary.Models;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataLibrary.Data
{
    public class OrderData : IOrderData
    {
        private readonly IDataAccess _dbAccess;
        private readonly ConnectionStringData _connectionString;

        public OrderData(IDataAccess dbAccess, ConnectionStringData connectionString)
        {
            _dbAccess = dbAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateOrder(OrderModel order)
        {
            var p = new DynamicParameters();

            p.Add("OrderName", order.OrderName);
            p.Add("OrderDate", order.OrderDate);
            p.Add("FoodId", order.FoodId);
            p.Add("Quantity", order.Quantity);
            p.Add("Total", order.Total);
            p.Add("Id", DbType.Int32, direction:ParameterDirection.InputOutput);

            await _dbAccess.SaveData("dbo.SPOrder_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id");
        }

        public Task<int> UpdateOrder(int orderId, string orderName)
        {
            return _dbAccess.SaveData("dbo.SPOrder_Update", new { Id = orderId, OrderName = orderName },
                _connectionString.SqlConnectionName);
        }

        public Task<int> DeleteOrder(int orderId)
        {
            return _dbAccess.SaveData("dbo.SPOrder_Delete", new { Id = orderId },
                _connectionString.SqlConnectionName);
        }

        public async Task<OrderModel> GetOrderById(int orderId)
        {
            var result = await _dbAccess.LoadData<OrderModel, dynamic>("dbo.SPOrder_GetById", new { Id = orderId },
                _connectionString.SqlConnectionName);

            return result.FirstOrDefault();
        }
    }
}
