using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Repositories
{
    public class Repositories
    {
        public AccountRepository AccountRepository { get; private set; }

        public ShopRepository ShopRepository { get; private set; }

        public ProductRepository ProductRepository { get; private set; }

        public Repositories(
                    AccountRepository accountRepository,
                    ShopRepository shopRepository,
                    ProductRepository productRepository)

        {
            AccountRepository = accountRepository;
            ShopRepository = shopRepository;
            ProductRepository = productRepository;
        }
    }
}
