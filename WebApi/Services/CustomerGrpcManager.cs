using Grpc.Core;
using WebApi.Grpc;
using WebApi.Interfaces;

namespace WebApi.Services;

public class CustomerGrpcManager(ICustomerService customerService) : CustomerGrpcService.CustomerGrpcServiceBase
{
    private readonly ICustomerService _customerService = customerService;

    public override async Task<GetCustomerByIdResponse> GetCustomerById(GetCustomerByIdRequest request, ServerCallContext context)
    {
        var response = await _customerService.GetCustomerByIdAsync(request.Id);
        
        if (!response.Succeeded || response.Result == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, response.Error ?? "Customer not found"));
        }

        if (string.IsNullOrEmpty(response.Result.PhoneNumber))
        {
            response.Result.PhoneNumber = "N/A";
        }

        return new GetCustomerByIdResponse
        {
            Id = response.Result.Id,
            CustomerName = response.Result.CustomerName,
            Email = response.Result.Email,
            PhoneNumber = response.Result.PhoneNumber,
        };

    }
}
