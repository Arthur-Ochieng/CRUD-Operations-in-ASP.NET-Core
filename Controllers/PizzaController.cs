using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
    PizzaService.GetAll();
    /*
        Responds only to the HTTP GET verb, as denoted by the [HttpGet] attribute.
        Returns an ActionResult instance of type List<Pizza>. The ActionResult type is the base class for all action results in ASP.NET Core.
        Queries the service for all pizza and automatically returns data with a Content-Type value of application/json.
    */

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> GetById(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza == null)
            return NotFound();
        return pizza;
    }
    /*
        Responds only to the HTTP GET verb, as denoted by the [HttpGet] attribute.
        Requires that the id parameter's value is included in the URL segment after pizza/. Remember, the controller-level [Route] attribute defined the /pizza pattern.
        Queries the database for a pizza that matches the provided id parameter.
    */


    // POST action
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(GetById), new { id = pizza.Id }, pizza);
    }
    /*
        IAction returns a response to the client letting them know if the request was successful or not
        ActionResult on the other end returns an object as a response, depending on the request of the client
        ActionResult is unknown until runtime
        Responds only to the HTTP POST verb, as denoted by the [HttpPost] attribute.
        Inserts the request body's Pizza object into the in-memory cache.
    */

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest(); 

        var existingPizza = PizzaService.Get(id);
        if (existingPizza == null)
            return NotFound();

        PizzaService.Update(pizza);
        return NoContent();
    }
    /*
        Responds only to the HTTP PUT verb, as denoted by the [HttpPut] attribute.
        Requires that the id parameter's value is included in the URL segment after pizza/.
        Returns IActionResult, because the ActionResult return type isn't known until runtime. 
        The BadRequest, NotFound, and NoContent methods return BadRequestResult, NotFoundResult, and NoContentResult types, respectively.
    */

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza == null)
            return NotFound();

        PizzaService.Delete(id);
        return NoContent();
    }
    /**/

    /*Because the controller is annotated with the [ApiController] attribute, 
     the pizza parameter will be found in the request body.*/
}