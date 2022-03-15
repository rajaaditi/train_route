using System.ComponentModel.DataAnnotations;

namespace LLM.Store.ApplicationCore.ViewModels
{
    public class UpdateTrain_RouteRequestViewModel
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid  train_route_id")]
        public int train_route_id { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid  account_id")]
        public int account_id { get; set; }
    }
}
