using System.Threading.Tasks;
using DataLibrary.Models;

namespace DataLibrary.Interface
{
    public interface IOrderData
    {
        Task<int> CreateOrder(OrderModel order);
        Task<int> UpdateOrder(int orderId, string orderName);
        Task<int> DeleteOrder(int orderId);
        Task<OrderModel> GetOrderById(int orderId);
    }
}