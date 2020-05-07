using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MyCollection.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboPropertyTypes();
    }
}