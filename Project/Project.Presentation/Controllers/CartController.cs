using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Cart.Commands.Add;
using Project.Application.Features.Cart.Commands.Delete;
using Project.Application.Features.Cart.Commands.Update;
using Project.Application.Features.Cart.Queries.GetAll;
using Project.Application.Features.Cart.Queries.GetById;
using Project.Domain.Routes.BaseRouter;

namespace Project.Presentation.Controllers;

public class CartController:BaseController
{
    [HttpPost(Router.CartRouter.Add)]
    public async Task<IActionResult> Create(AddToCartCommand request)
    {
        var result=await mediator.Send(request);
        return Result(result);
    }

    [HttpDelete(Router.CartRouter.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteCartCommand(id));
        return Result(result);

    }

    [HttpPut(Router.CartRouter.Update)]
    public async Task<IActionResult> Update(Guid id,UpdateCartCommand request)
    {
        request.Id = id;
        var result = await mediator.Send(request);
        return Result(result);
    }

    [HttpGet(Router.CartRouter.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCarts request)
    {
        var result = await mediator.Send(request);
        return Result(result);
    }

    [HttpGet(Router.CartRouter.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetByIdCartsQuery(id));
        return Result(result);
    }
}