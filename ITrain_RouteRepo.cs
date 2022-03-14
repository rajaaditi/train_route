
using LLM.Store.ApplicationCore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LLM.Store.ApplicationCore.Interfaces.Bal
{
    public interface ITrain_RouteRepo
    { 

        Task<CreateTrain_RouteResponseViewModel> CreateTrainRoute(CreateTrain_RouteRequestViewModel entity, TokenInfo tokenInfo);
        Task<UpdateTrain_RouteResponseViewModel> UpdateRouteTrain(UpdateTrain_RouteRequestViewModel entity);
        Task<DetailsTrainRouteResponseViewModel> DeatailsRouteTrain(int train_route_id);
        Task<(int, List<ListTrainRouteResponseViewModel>)> ListFilterRouteTrain(int pageIndex, int pageSize, ListTrainRouteRequestViewModel entity);
      
    }

}
