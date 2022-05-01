using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charity
{
    public interface ICharityDataProvider
    {
        List<Category> GetCharityCategories();

        IEnumerable<Organization> GetOrganizations(string category, string city = "");
    }
}
