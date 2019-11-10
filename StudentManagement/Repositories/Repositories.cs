
namespace StudentManagement.Repositories
{
    public class Repositories
    {
        public AccountRepository AccountRepository { get; private set; }

        public Repositories(AccountRepository accountRepository)
        {
            this.AccountRepository = accountRepository;
        }
    }
}
