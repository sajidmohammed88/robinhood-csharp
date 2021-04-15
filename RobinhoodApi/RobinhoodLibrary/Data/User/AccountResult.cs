using RobinhoodLibrary.Data.Base;
using System.Collections.Generic;

namespace RobinhoodLibrary.Data.User
{
    public class AccountResult : BaseResult
    {
        public IList<Account> Results { get; set; }
    }
}
