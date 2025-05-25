using Vintagefur.BusinessLogic.BLogic;
using Vintagefur.BusinessLogic.Interfaces;

namespace Vintagefur.BusinessLogic
{
    public class BusinessLogic
    {
        public IProduct GetProductBl()
        {
            return new ProductBL();
        }
        
        public ICartService GetCartBl()
        {
            return new CartBL();
        }
        
        public IAdminService GetAdminBl()
        {
            return new AdminBL();
        }
        
        public IAuthService GetAuthBl()
        {
            return new AuthBL();
        }
        
        public ICustomerBL GetCustomerBl()
        {
            return new CustomerBL();
        }
        
        public IOrderBL GetOrderBl()
        {
            return new OrderBL();
        }
        
        // Другие методы для получения объектов бизнес-логики
        public IUserBL GetUserBL()
        {
            return new UserBL();
        }
        
        public IRoleBL GetRoleBL()
        {
            return new RoleBL();
        }
    }
} 