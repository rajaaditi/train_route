using System;

namespace LLM.Store.ApplicationCore.ViewModels
{
    public class CreateTrain_RouteRequestViewModel
    {
        public int route_id { get; set; }
        public string  train_no{ get; set; }
        public int account_id { get; set; }

        public DateTime route_date { get; set; }
      
       
    }
}
