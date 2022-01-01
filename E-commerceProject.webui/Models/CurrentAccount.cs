

using System;

namespace E_commerceProject.webui.Models
{
    public class CurrentAccount
    {
        public Account Profile { get; set; }

        public CurrentAccount(Account acc)
        {
            Profile = acc;
        }
    }
}