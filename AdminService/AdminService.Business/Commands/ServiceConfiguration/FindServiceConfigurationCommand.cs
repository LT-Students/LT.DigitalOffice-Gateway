using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.ServiceConfiguration.Interfaces;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Kernel.Validators.Interfaces;

namespace LT.DigitalOffice.AdminService.Business.Commands.ServiceConfiguration
{
  public class FindServiceConfigurationCommand : IFindServiceConfigurationCommand
  {
    private readonly IBaseFindFilterValidator _baseFindFilterValidator;
    private readonly IServiceConfigurationRepository _serviceConfigurationRepository;
    private readonly IServiceConfigurationInfoMapper _serviceConfigurationInfoMapper;
    private readonly IResponseCreator _responseCreator;

    public FindServiceConfigurationCommand(
      IBaseFindFilterValidator baseFindFilterValidator,
      IServiceConfigurationRepository serviceConfigurationRepository,
      IServiceConfigurationInfoMapper serviceConfigurationInfoMapper,
      IResponseCreator responseCreator)
    {
      _baseFindFilterValidator = baseFindFilterValidator;
      _serviceConfigurationRepository = serviceConfigurationRepository;
      _serviceConfigurationInfoMapper = serviceConfigurationInfoMapper;
      _responseCreator = responseCreator;
    }

    public async Task<FindResultResponse<ServiceConfigurationInfo>> ExecuteAsync(BaseFindFilter filter)
    {
      if (!_baseFindFilterValidator.ValidateCustom(filter, out List<string> errors))
      {
        return _responseCreator.CreateFailureFindResponse<ServiceConfigurationInfo>(HttpStatusCode.BadRequest, errors);
      };

      FindResultResponse<ServiceConfigurationInfo> response = new();
      response.Body = new();

      (List<DbServiceConfiguration> dbServicesConfigurations, int totalCount) =
        await _serviceConfigurationRepository.FindAsync(filter);

      response.TotalCount = totalCount;
      response.Body.AddRange(dbServicesConfigurations.Select((dbServicesConfigurations) =>
        _serviceConfigurationInfoMapper.Map(dbServicesConfigurations)));

      return response;
    }
  }
}

