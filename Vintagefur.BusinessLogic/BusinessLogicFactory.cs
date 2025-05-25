using Vintagefur.BusinessLogic.BLogic;
using Vintagefur.BusinessLogic.Interfaces;

namespace Vintagefur.BusinessLogic
{
    public class BusinessLogicFactory
    {
        private static BusinessLogicFactory _instance;
        private static readonly object _lock = new object();
        
        private BusinessLogicFactory()
        {
            // Приватный конструктор для реализации Singleton
        }
        
        public static BusinessLogicFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new BusinessLogicFactory();
                        }
                    }
                }
                return _instance;
            }
        }
        
        public IProduct GetProductBL()
        {
            return new ProductBL();
        }
        
        public ICartService GetCartBL()
        {
            return new CartBL();
        }
        
        public IAdminService GetAdminBL()
        {
            return new AdminBL();
        }
        
        public IAuthService GetAuthBL()
        {
            return new AuthBL();
        }
        
        public ICustomerBL GetCustomerBL()
        {
            return new CustomerBL();
        }
        
        public IOrderBL GetOrderBL()
        {
            return new OrderBL();
        }
        
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