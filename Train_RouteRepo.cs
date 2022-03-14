
using LLM.Store.ApplicationCore.Interfaces.Bal;
using LLM.Store.ApplicationCore.Interfaces.Dal;
using LLM.Store.ApplicationCore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LLM.Store.BAL
{
    public class Train_RouteRepo: ITrain_RouteRepo
    {
        private readonly ITrain_RouteData _train_RouteData;
        public Train_RouteRepo(ITrain_RouteData train_RouteData)
        {
            _train_RouteData = train_RouteData;
        }
        public async Task<CreateTrain_RouteResponseViewModel> CreateTrainRoute(CreateTrain_RouteRequestViewModel entity, TokenInfo tokenInfo)
        {
            return await _train_RouteData.CreateRouteTrain(entity,  tokenInfo);

        }
        public async Task<UpdateTrain_RouteResponseViewModel> UpdateRouteTrain(UpdateTrain_RouteRequestViewModel entity)
        {

            return await _train_RouteData.UpdateRouteTrain(entity);
        }
        public async Task<DetailsTrainRouteResponseViewModel> DeatailsRouteTrain( int train_route_id)
        {

            return await _train_RouteData.DeatailsRouteTrain(train_route_id);
        }
       
       
        public async Task<(int, List<ListTrainRouteResponseViewModel>)> ListFilterRouteTrain(int pageIndex, int pageSize, ListTrainRouteRequestViewModel entity)
        {

            (int TotalCount, List<ListTrainRouteResponseViewModel> result) = await _train_RouteData.ListFilterRouteTrain(pageIndex, pageSize, entity);
            return (TotalCount, result);
        }
    }
}
