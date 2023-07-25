using AutoMapper;
using Basket.Application.Contracts.ExtApiService;
using Basket.Application.Contracts.Handlers.Dtos;
using Basket.Application.Contracts.Persistence;
using Basket.Application.Exceptions;
using Basket.Application.Models;
using Basket.Domain.Entities;

namespace Basket.Application.Contracts.Handlers;

public class BasketHandler : IBasketHandler
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;


    public BasketHandler(IBasketRepository basketRepository, IMapper mapper , IOrderService orderService)
    {
        _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    public async Task<ShoppingCartDto?> AddBasketAsync(ShoppingCartDto basket)
    {

        var mappedBasket = _mapper.Map<ShoppingCart>(basket);

        var addedBasket = await _basketRepository.AddBasket(mappedBasket);

        return _mapper.Map<ShoppingCartDto?>(addedBasket);
    }

    public async Task<int> Checkout(BasketCheckout basketCheckout)
    {
        var basket = await GetBasketAsync(basketCheckout.UserName);
        if(basket is null) throw new NotFoundException(nameof(ShoppingCart), basketCheckout.UserName!);

        var mappedBasketCheckout = _mapper.Map<OrderCheckoutModel>(basketCheckout);

        mappedBasketCheckout.TotalPrice = basket.TotalPrice;

        var res = await _orderService.Checkout(mappedBasketCheckout);

        await DeleteBasketAsync(basketCheckout.UserName);

        return res;

    }

    public async Task DeleteBasketAsync(string? userName)
    {
        await _basketRepository.DeleteBasket(userName);
    }

    public async Task<ShoppingCartDto?> GetBasketAsync(string? userName)
    {
        var basket = await _basketRepository.GetBasket(userName);
        if (basket is null) throw new NotFoundException(nameof(ShoppingCart), userName!);
        return _mapper.Map<ShoppingCartDto>(basket);
    }

    public async Task<ShoppingCartDto?> UpdateBasketAsync(ShoppingCartDto basket)
    {
        var mappedBasket = _mapper.Map<ShoppingCart>(basket);

        var updatedBasket = await _basketRepository.UpdateBasket(mappedBasket);

        return _mapper.Map<ShoppingCartDto>(updatedBasket);
    }
}
