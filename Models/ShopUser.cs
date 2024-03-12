using Microsoft.AspNetCore.Identity;

namespace ITStepShop.Models
{
    public class ShopUser : IdentityUser
    {
        public string FullName { get; set; }
        public string AddressDelivery { get; set; }
    }
}
