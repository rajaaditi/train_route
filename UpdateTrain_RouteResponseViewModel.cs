using System;

namespace LLM.Store.ApplicationCore.ViewModels
{
    public class UpdateTrain_RouteResponseViewModel
    {
        public int train_route_id { get; set; }
        public string msg { get; set; }
        public int account_id { get; set; }
        public DateTime modified_on { get; set; }
    }
}
