using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.API
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        //LISTA DI TUTTE LE PIZZE
        [HttpGet]
        public IActionResult GetPizzas()
        {
            using PizzaContext db = new PizzaContext();
            List<Pizza> pizzas = db.Pizzas.ToList();

            return Ok(pizzas);
        }

        //FILTRO CON NOME
        [HttpGet]
        public IActionResult GetPizzasByName(string name)
        {
            using PizzaContext db = new PizzaContext();
            List<Pizza> pizzas = db.Pizzas.Where(p => p.Name.Contains(name)).ToList();
            return Ok(pizzas);

        }

        //FILTRO CON ID
        [HttpGet]
        public IActionResult GetPizzasById(int Id)
        {
            using PizzaContext db = new PizzaContext();
            List<Pizza> pizzas = db.Pizzas.Where(p => p.Id==Id).ToList();
            return Ok(pizzas);

        }

        //CREAZIONE PIZZA


    }
}
