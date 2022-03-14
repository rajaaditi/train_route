
using LLM.Store.ApplicationCore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LLM.Store.ApplicationCore.Interfaces.Dal
{
    public interface ITrain_RouteData
    {
        Task<CreateTrain_RouteResponseViewModel> CreateRouteTrain(CreateTrain_RouteRequestViewModel entity, TokenInfo tokenInfo);
        Task<UpdateTrain_RouteResponseViewModel> UpdateRouteTrain(UpdateTrain_RouteRequestViewModel entity);
        Task<DetailsTrainRouteResponseViewModel> DeatailsRouteTrain(int train_route_id);
        Task<(int, List<ListTrainRouteResponseViewModel>)> ListFilterRouteTrain(int pageIndex, int pageSize, ListTrainRouteRequestViewModel entity);
     
    }
}
