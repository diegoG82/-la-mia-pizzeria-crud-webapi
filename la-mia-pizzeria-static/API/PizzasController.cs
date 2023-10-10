using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.API
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PizzasController : ControllerBase


    {
        private PizzaContext _myDb;

        public PizzasController(PizzaContext myDb)
        {
            _myDb = myDb;
        }

        //LISTA DI TUTTE LE PIZZE
        [HttpGet]
        public IActionResult GetPizzas()
        {

            List<Pizza> pizzas = _myDb.Pizzas.Include(p => p.Category)
        .Include(p => p.Ingredients).ToList();

            if (pizzas.Count == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(pizzas);
            }

        }

        //FILTRO CON NOME
        [HttpGet]
        public IActionResult GetPizzasByName(string? name)
        {

            List<Pizza> pizzas = _myDb.Pizzas.Where(p => p.Name.Contains(name)).Include(p => p.Category)
        .Include(p => p.Ingredients).ToList();

            if (pizzas.Count == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(pizzas);
            }



        }

        //FILTRO CON ID
        [HttpGet]
        public IActionResult GetPizzasById(int Id)
        {

            List<Pizza> pizzas = _myDb.Pizzas.Where(p => p.Id == Id).Include(p => p.Category)
        .Include(p => p.Ingredients).ToList();

            if (pizzas.Count == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(pizzas);
            }


        }

        //CREAZIONE PIZZA
        [HttpPost]
        public IActionResult CreatePizza([FromBody] Pizza newPizza)
        {


            _myDb.Pizzas.Add(newPizza);
            _myDb.SaveChanges();

            return Ok(newPizza);
        }


        //UPDATE PIZZA
        [HttpPut("{id}")]
        public IActionResult UpdatePizza(int Id, [FromBody] Pizza updatedPizza)
        {
            Pizza? modifiedPizza = _myDb.Pizzas.Where(p => p.Id == Id).Include(p => p.Category)
        .Include(p => p.Ingredients).FirstOrDefault();

            if (modifiedPizza != null)
            {
                return NotFound();
            }


            modifiedPizza.Name = updatedPizza.Name;
            modifiedPizza.Image = updatedPizza.Image;
            modifiedPizza.Description = updatedPizza.Description;
            modifiedPizza.Ingredients = updatedPizza.Ingredients;
            modifiedPizza.CategoryId = updatedPizza.CategoryId;
            modifiedPizza.Price = updatedPizza.Price;


            _myDb.SaveChanges();

            return Ok();
        }

        //DELETE PIZZA
        [HttpDelete]

        public IActionResult DeletePizza(int id)
        {
            Pizza deletedPizza = _myDb.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (deletedPizza == null)
            {
                return NotFound();
            }

            _myDb.Pizzas.Remove(deletedPizza);
            _myDb.SaveChanges();
            return Ok();
        }


    }

}






