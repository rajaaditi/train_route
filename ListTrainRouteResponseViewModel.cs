using System;

namespace LLM.Store.ApplicationCore.ViewModels
{
    public class ListTrainRouteResponseViewModel
    {
        public int train_route_id { get; set; }
        public int route_id { get; set; }
        public string train_no { get; set; }
        public string train_name { get; set; }
        public DateTime route_date { get; set; }
    }
}
