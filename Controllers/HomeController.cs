using Microsoft.AspNetCore.Mvc;
using ProjetoCarros.Models;
using System.Diagnostics;

namespace ProjetoCarros.Controllers
{
    public class HomeController : Controller
    {
        private static List<Carros> _List = new List<Carros>()
        {
            new Carros() { id = 1 , nome = "carro legal", fabricante = "fabricante boa", marca = "marca bonita", modelo = "modelo foda", ano = "2001" },
            new Carros() { id = 2 , nome = "carro legal 2", fabricante = "fabricante boa 2", marca = "marca bonita 2", modelo = "modelo foda 2", ano = "2002" },
        };

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult Carros()
        {
            var viewModel = new CarrosViewModel() { listCarro = _List };

            return View(viewModel);
        }

        public IActionResult CadastrarCarro(int? id)
        {
            Carros? carro = null;

            if (id != null)
            {
                carro = _List.Where(w => w.id == id).FirstOrDefault();
            }

            return View(carro);
        }

        public IActionResult PersistirCarro(int? id, string nome, string fabricante, string marca, string modelo, string ano)
        {
            if (id == null)
            {
                var novoCarro = new Carros()
                {
                    id = _List.Count + _List.Last().id,
                    nome = nome,
                    fabricante = fabricante,
                    marca = marca,
                    modelo = modelo,
                    ano = ano
                };

                _List.Add(novoCarro);
            }
            else
            {
                var carro = _List.Where(w => w.id == id).FirstOrDefault();
                carro.nome = nome;
                carro.fabricante = fabricante;
                carro.marca = marca;
                carro.modelo = modelo;
                carro.ano = ano;
            }

            return RedirectToAction("Carros");
        }

        public IActionResult Deletar(int Id)
        {
            var carro = _List.Where(w => w.id == Id).FirstOrDefault();

            _List.Remove(carro);

            return RedirectToAction("Carros");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}