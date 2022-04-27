using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.GraphicalUserInterface.Interfaces;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.AdminService.Business.Commands.GraphicalUserInterface
{
  public class GetGraphicalUserInterfaceCommand : IGetGraphicalUserInterfaceCommand
  {
    private readonly IGraphicalUserInterfaceSettingRepository _repository;
    private readonly IGuiInfoMapper _mapper;

    public GetGraphicalUserInterfaceCommand(
      IGraphicalUserInterfaceSettingRepository repository,
      IGuiInfoMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public async Task<OperationResultResponse<GuiInfo>> ExecuteAsync()
    {
      return new()
      {
        Body = _mapper.Map(await _repository.GetAsync())
      };
    }
  }
}
