using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.ContentManagement;
using YesSql;

namespace OrchardCore.GreenHouse.Models;

public class FlowerFilter
{
    public List<Func<IQuery<ContentItem>, IQuery<ContentItem>>> Conditions { get; } = [];
}
