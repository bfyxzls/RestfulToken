using System;
namespace MvcApplication2.Service
{
	[Lind.DI.Component]
	public class ProductService:IProductService
    {
        public ProductService()
        {
        }

		public string getProductName()
		{
			return "ioc for product service.";
		}
	}
}
