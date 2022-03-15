using System;
using System.ComponentModel.DataAnnotations;

namespace LLM.Store.ApplicationCore.ViewModels
{
    public class CreateTrain_RouteRequestViewModel
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid  route_id")]
        public int route_id { get; set; }
        [MaxLength(50, ErrorMessage = "The train_no doesn't have more than 100 characters")]
        public string  train_no{ get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid  account_id")]
        public int account_id { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Please enter valid Date")]
        public DateTime route_date { get; set; }
      
       
    }
}
