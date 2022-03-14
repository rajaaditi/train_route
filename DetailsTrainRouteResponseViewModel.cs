using System;

namespace LLM.Store.ApplicationCore.ViewModels
{
    public class DetailsTrainRouteResponseViewModel
    {
        public int train_route_id { get; set; }
        public int route_id { get; set; }
        public string train_no { get; set; }
        public int account_id { get; set; }

        public DateTime route_date { get; set; }
        public DateTime created_on { get; set; }
        public int created_by { get; set; }
        public DateTime modified_on { get; set; }

        public int modified_by { get; set; }
        public string msg { get; set; }
    }
}
