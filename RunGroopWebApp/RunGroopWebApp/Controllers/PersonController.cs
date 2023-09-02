using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.Models.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class PersonController : Controller
    {
        private readonly AppDbContext _context;

        public PersonController(AppDbContext context)
        {
            _context = context;
        }

        public ActionResult Index() 
        {
            var people = _context.People.Where(p => (p.IsDeleted == null || !p.IsDeleted.Value)).ToList();


            return View(people);
        }

        [HttpGet]
        public IActionResult CreateOrEdit(int? id = null)
        {
            if (id.HasValue)
            {
                var person = _context.People.FirstOrDefault(p => p.Id == id.Value);
                var personViewModel = new CreateOrEditPersonViewModel
                {
                    Name = person.Name,
                    BirthDate = person.BirthDate,
                    RG = person.RG,
                    Id = person.Id,
                };

                return PartialView("_CreateOrEdit", personViewModel);
            }

            return PartialView("_CreateOrEdit");
        }

        [HttpGet]
        public IActionResult PeopleList()
        {
            var people = _context.People.Where(p => (p.IsDeleted == null || !p.IsDeleted.Value)).ToList();

            return View(people);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var person = _context.People.Where(p => (p.IsDeleted == null || !p.IsDeleted.Value) && p.Id == id).FirstOrDefault();

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPersonViewModel personViewModel)
        {
            try
            {
                // Instalar automapper depois para fazer correspondências entre dto e entidade e nao precisar mapear
                // Instalar lib validação

                // update
                if (personViewModel.Id != null && personViewModel.Id > 0)
                {
                    var person = _context.People.FirstOrDefault(p => p.Id == personViewModel.Id);

                    person.BirthDate = personViewModel.BirthDate;
                    person.RG = personViewModel.RG;
                    person.Name = personViewModel.Name;

                    _context.People.Update(person);
                    await _context.SaveChangesAsync();

                }
                else // create
                {
                    Person newPerson = new Person
                    {
                        BirthDate = personViewModel.BirthDate,
                        Name = personViewModel.Name,
                        RG = personViewModel.RG
                    };

                    _context.People.Add(newPerson);
                    await _context.SaveChangesAsync();
                }

                return Ok(Json(new DefaultJsonResponse
                {
                    Success = true,
                    Message = "Tudo certo :)"
                }));
            }
            catch(Exception e)
            {
                return BadRequest(Json(new DefaultJsonResponse
                {
                    Success = false,
                    Message = e.Message
                }));
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var person = _context.People.FirstOrDefault(p => p.Id == id);

                person.IsDeleted = true;

                _context.People.Update(person);
                await _context.SaveChangesAsync();


                return Ok(Json(new DefaultJsonResponse
                {
                    Success = true,
                    Message = "Deletado com sucesso"
                }));
            }
            catch
            {
                return BadRequest(Json(new DefaultJsonResponse
                {
                    Success = false,
                    Message = "Deu algo errado"
                }));
            }
        }
    }
}
