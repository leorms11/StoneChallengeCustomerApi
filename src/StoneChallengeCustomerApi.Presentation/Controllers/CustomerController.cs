using Microsoft.AspNetCore.Mvc;
using StoneChallengeCustomerApi.Application.DTOs.Customers;
using StoneChallengeCustomerApi.Application.UseCases.Customers.Interfaces;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace StoneChallengeCustomerApi.Presentation.Controllers;

[ApiVersion("1.0")]
[Consumes("application/json")]
[Route("api/v{version:apiVersion}/customers", Name = "Customers")]
public class CustomerController : BaseController
{
    private readonly ICreateCustomerUseCase _createCustomerUseCase;
    private readonly IFindCustomerByCpfUseCase _findCustomerByCpfUseCase;
    private readonly IListCustomersUseCase _listCustomersUseCase;

    public CustomerController(ICreateCustomerUseCase createCustomerUseCase, 
        IFindCustomerByCpfUseCase findCustomerByCpfUseCase, 
        IListCustomersUseCase listCustomersUseCase)
    {
        _createCustomerUseCase = createCustomerUseCase;
        _findCustomerByCpfUseCase = findCustomerByCpfUseCase;
        _listCustomersUseCase = listCustomersUseCase;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ActionResult<IEnumerable<ListCustomersResponseDTO>>), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Listagem dos clientes cadastrados.")]
    public async Task<ActionResult<IEnumerable<ListCustomersResponseDTO>>> ListAsync()
    {
        var response = await _listCustomersUseCase.ExecuteAsync();

        return response is FailedOperation ? BadRequest(response) : Ok(response.Data);
    }

    [HttpGet("{cpf}")]
    [ProducesResponseType(typeof(ActionResult<FindCustomerResponseDTO>), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Buscar por um cliente.")]
    public async Task<ActionResult<FindCustomerResponseDTO>> FindAsync([FromRoute] string cpf)
    {
        var response = await _findCustomerByCpfUseCase.ExecuteAsync(cpf);

        return response is FailedOperation ? BadRequest(response) : Ok(response.Data);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
    [SwaggerOperation(Summary = "Criar um novo Cliente.")]
    public async Task<ActionResult<IOperation>> CreateAsync([FromBody] CreateCustomerRequestDTO dto)
    {
        var response = await _createCustomerUseCase.ExecuteAsync(dto);

        return response is FailedOperation ? BadRequest(response) : Ok(response);
    }
}