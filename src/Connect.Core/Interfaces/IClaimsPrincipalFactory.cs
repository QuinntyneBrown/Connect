using System.Collections.Generic;

namespace Connect.Core.Interfaces
{
    public interface ISecurityTokenFactory
    {
        string Create(string username, ICollection<string> roles = default(ICollection<string>));
    }
}
