
using LLM.Store.ApplicationCore.Interfaces.Dal;
using LLM.Store.ApplicationCore.Utils;
using LLM.Store.ApplicationCore.ViewModels;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;

namespace LLM.Store.DAL
{
    public class Train_RouteData: ITrain_RouteData
    {
        public async Task<CreateTrain_RouteResponseViewModel> CreateRouteTrain(CreateTrain_RouteRequestViewModel entity, TokenInfo tokenInfo)
        {
            DbParam[] param = new DbParam[6];

            param[0] = new DbParam("p_route_id", entity.route_id, MySqlDbType.Int32);
            param[1] = new DbParam("p_train_no", entity.train_no, MySqlDbType.VarChar);
            param[2] = new DbParam("p_account_id", entity.account_id, MySqlDbType.Int32);
            param[3] = new DbParam("p_route_date", entity.route_date, MySqlDbType.DateTime);
            param[4] = new DbParam("p_created_by",  tokenInfo.created_by, MySqlDbType.Int32);
            param[5] = new DbParam("p_modified_by",  tokenInfo.modified_by, MySqlDbType.Int32);

            // DataSet code  
            DataSet ds = await DBUtil.GetDataSetAsync("usp_routetrain_create", param, 1800);

            if (DBUtil.IsDataExists(ds))
            {


                if (ds.Tables[0].Rows.Count > 0)

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CreateTrain_RouteResponseViewModel train = new CreateTrain_RouteResponseViewModel();
                        train.train_route_id = DBUtil.ToInteger(row["train_route_id"]);
                        train.train_no= DBUtil.ToString(row["train_no"]);

                        return train;
                    }

            }
            else
            {
                return null;
            }
            return null;
        }
        public async Task<UpdateTrain_RouteResponseViewModel> UpdateRouteTrain(UpdateTrain_RouteRequestViewModel entity )
        {
            DbParam[] param = new DbParam[2];

            param[0] = new DbParam("p_train_route_id", entity.train_route_id, MySqlDbType.Int64);
            param[1] = new DbParam("p_account_id", entity.account_id, MySqlDbType.Int32);


            DataTable dt = await DBUtil.GetDataTableAsync("usp_routetrain_update", param, 30);
            if (DBUtil.IsDataExists(dt))
            {
                return dt.ConvertDataTable<UpdateTrain_RouteResponseViewModel>()[0];
            }
            else
            {
                return null;
            }
        }
        public async Task<DetailsTrainRouteResponseViewModel> DeatailsRouteTrain(int  train_route_id )
        {
            DbParam[] param = new DbParam[1];

            param[0] = new DbParam("p_train_route_id", train_route_id, MySqlDbType.Int64);
            


            DataTable dt = await DBUtil.GetDataTableAsync("usp_train_route_get", param, 30);
            if (DBUtil.IsDataExists(dt))
            {
                return dt.ConvertDataTable<DetailsTrainRouteResponseViewModel>()[0];
            }
            else
            {
                return null;
            }
        }
        
        public async Task<(int, List<ListTrainRouteResponseViewModel>)> ListFilterRouteTrain(int pageIndex, int pageSize, ListTrainRouteRequestViewModel entity)
        {
            List<ListTrainRouteResponseViewModel> trains = new List<ListTrainRouteResponseViewModel>();

            DbParam[] param = new DbParam[6];
            param[0] = new DbParam("p_PageIndex", pageIndex, MySqlDbType.Int32);
            param[1] = new DbParam("p_PageSize", pageSize, MySqlDbType.Int32);
            param[2] = new DbParam("p_train_route_id", entity.train_route_id, MySqlDbType.Int64);
            param[3] = new DbParam("p_route_id", entity.route_id, MySqlDbType.Int32);
            param[4] = new DbParam("p_train_no", entity.train_no, MySqlDbType.VarChar);
            param[5] = new DbParam("p_train_name", entity.train_name, MySqlDbType.VarChar);



            DataSet ds = await DBUtil.GetDataSetAsync("usp_trainroute_data", param, 1800);
            if (DBUtil.IsDataExists(ds) && ds.Tables.Count > 1)
            {
                trains = (from DataRow row in ds.Tables[0].Rows
                          select new ListTrainRouteResponseViewModel()
                          {
                              train_route_id = (DBUtil.ToInteger(row["train_route_id"])),
                              route_id = (DBUtil.ToInteger(row["route_id"])),
                              train_no = (DBUtil.ToString(row["train_no"])),
                              train_name = (DBUtil.ToString(row["train_name"])),
                              route_date = (DBUtil.ToDateTime(row["route_date"])),
                          }).ToList();

                return (DBUtil.ToInteger(ds.Tables[1].Rows[0]["TotalRecord"]), trains);
            }

            else
            {
                return (0, null);
            }
        }

    }
}
