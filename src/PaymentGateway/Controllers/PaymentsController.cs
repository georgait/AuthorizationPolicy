using Microsoft.AspNetCore.Mvc;
using PaymentGateway.DTOs;
using PaymentGateway.Infrastructure.Data;
using PaymentGateway.Infrastructure.Data.Entities;

namespace PaymentGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IRepository<Payment> _repository;

    public PaymentsController(IRepository<Payment> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentDto>> GetPaymentById(Guid id)
    {
        var payment = await _repository.GetByIdAsync(id);
        
        if (payment is null) return NotFound();

        var dto = payment.ToDto();

        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaymentDto>> CreatePayment([FromBody] CreatePaymentDto payment)
    {
        var entity = payment.ToEntity();

        // Simulate payment confirmation        
        entity.ConfirmPayment(); // Here is where the confirmation event is added 

        var createdEntity = await _repository.AddAsync(entity);

        var dto = createdEntity.ToDto();

        return CreatedAtAction(nameof(GetPaymentById), new { id = dto.Id }, dto);
    }
}
