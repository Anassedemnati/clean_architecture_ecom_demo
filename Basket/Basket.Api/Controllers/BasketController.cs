﻿using Basket.Application.Contracts.Handlers;
using Basket.Application.Contracts.Handlers.Dtos;
using Basket.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketHandler _basketHandler;
    private readonly IValidator<ShoppingCartDto> _requestValidator;

    public BasketController(IBasketHandler basketHandler, IValidator<ShoppingCartDto> requestValidator)
    {
        _basketHandler = basketHandler ?? throw new ArgumentNullException(nameof(basketHandler));
        _requestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator));
    }
   


    [HttpGet("{userName}", Name = "GetBasket")]
    public async Task<ActionResult<ShoppingCartDto>> GetBasket(string? userName)
    {
        var basket = await _basketHandler.GetBasketAsync(userName);
        return Ok(basket ?? new ShoppingCartDto(userName));
    }
    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCartDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ShoppingCartDto>> UpdateBasket([FromBody] ShoppingCartDto basket)
    {
        var validationResult = await _requestValidator.ValidateAsync(basket);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        return Ok(await _basketHandler.UpdateBasketAsync(basket));
    }

    [HttpDelete("{userName}", Name = "DeleteBasket")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket(string? userName)
    {
        await _basketHandler.DeleteBasketAsync(userName);
        return Ok();
    }
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(ShoppingCartDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ShoppingCartDto>> AddBasket([FromBody] ShoppingCartDto basket)
    {
        var validationResult = await _requestValidator.ValidateAsync(basket);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        return Ok(await _basketHandler.AddBasketAsync(basket));
    }

    
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
        
  
        
        if (basketCheckout == null)
        {
            return BadRequest();
        }

        var res = await _basketHandler.Checkout(basketCheckout);

        if(res == 0)
        {
            return BadRequest();
        }
      
        return Accepted();
    }

}
