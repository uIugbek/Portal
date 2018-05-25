using System;
using System.Collections.Generic;

namespace Kendo.DynamicLinq
{
    public class DataSourceRequestWithAggregates : DataSourceRequest
    {
        public IEnumerable<Aggregator> Aggregate { get; set; }
    }
}
