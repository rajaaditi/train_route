

using System.ComponentModel.DataAnnotations;

namespace LLM.Store.ApplicationCore.ViewModels
{
    public class ListTrainRouteRequestViewModel
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid  train_route_id")]
        public int train_route_id { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid  route_id")]
        public int route_id { get; set; }
        [MaxLength(50, ErrorMessage = "The train_no  doesn't have more than 50 characters")]
        public string train_no { get; set; }
        [MaxLength(100, ErrorMessage = "The train_name doesn't have more than 100 characters")]
        public string train_name { get; set; }
       
    }
}
