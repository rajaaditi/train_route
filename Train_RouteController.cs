

using LLM.Store.ApplicationCore.Interfaces.Bal;
using LLM.Store.ApplicationCore.Response;
using LLM.Store.ApplicationCore.Utils;
using LLM.Store.ApplicationCore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goggly_Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Train_RouteController : ControllerBase
    {
        private readonly ITrain_RouteRepo _train_RouteRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Train_RouteController(ITrain_RouteRepo train_RouteRepo, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _train_RouteRepo = train_RouteRepo;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPut("CreateTrainRoute")]
        public async Task<IActionResult> CreateTrainRoute([FromBody] CreateTrain_RouteRequestViewModel entity)
        {
            ISingleModelResponse<CreateTrain_RouteResponseViewModel> response = new SingleModelResponse<CreateTrain_RouteResponseViewModel>();
            try
            {
                TokenInfo tokenInfo = HttpContext.User.GetTokenInfo();
                CreateTrain_RouteResponseViewModel ResponseViewModel = await _train_RouteRepo.CreateTrainRoute(entity, tokenInfo);

                if (ResponseViewModel.train_route_id > 0)
                {
                    response.Model = ResponseViewModel;
                    response.Message = "SuccesfullyCreated";

                }
                else
                {
                    response.IsError = true;
                    response.Model = ResponseViewModel;
                    response.Message = "Already exist";
                }

                return Ok(response);


            }

            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }

        }
        [HttpPut("UpdateRouteTrain")]
        public async Task<IActionResult> UpdateRouteTrain([FromQuery] UpdateTrain_RouteRequestViewModel entity)
        {
            ISingleModelResponse<UpdateTrain_RouteResponseViewModel> response = new SingleModelResponse<UpdateTrain_RouteResponseViewModel>();
            try
            {
                
               UpdateTrain_RouteResponseViewModel result = await _train_RouteRepo.UpdateRouteTrain( entity);
                if (result.train_route_id != 0 && result.msg== "succesfully updated")
                {
                    response.Model = result;
                    response.Message = "SuccessfullyUpdate";
                }
                else
                {
                    response.IsError = true;
                    response.Message = "Not found train_route_id.";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPut("DetailsTrainRoute")]
        public async Task<IActionResult> DetailsTrainRoute([FromQuery] int train_route_id)
        {
            ISingleModelResponse<DetailsTrainRouteResponseViewModel> response = new SingleModelResponse<DetailsTrainRouteResponseViewModel>();
            try
            {

                DetailsTrainRouteResponseViewModel result = await _train_RouteRepo.DeatailsRouteTrain(train_route_id);
                if (result.train_route_id > 0 &&result.msg== "Details Get")
                {
                    response.IsError = false;
                    response.Model = result;
                    response.Message = "Suceesfully Get Details";
                }
                else
                {
                    response.Model = result;
                    response.IsError = true;
                    response.Message = "Details Not Found.";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPost("ListRouteTrain")]
        public async Task<IActionResult> ListRouteTrain([FromBody] ListTrainRouteRequestViewModel entity, int pageIndex, int pageSize)
        {

            IListModelResponse<ListTrainRouteResponseViewModel> response = new ListModelResponse<ListTrainRouteResponseViewModel>();
            try
            {
                (int TotalCount, List<ListTrainRouteResponseViewModel> result) = await _train_RouteRepo.ListFilterRouteTrain(pageIndex, pageSize,entity);
                if (TotalCount > 0 && result != null)
                {
                    response.Model = result;
                    response.TotalRecord = TotalCount;
                    response.Message = "Success";
                }
                else
                {
                    response.IsError = true;
                    response.TotalRecord = 0;
                    response.Message = "No record found.";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }
        

    }
}
