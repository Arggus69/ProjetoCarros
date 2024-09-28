using Microsoft.AspNetCore.Mvc;
using ProjetoCarros.Models;
using Servico;
using Servico.model;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ProjetoCarros.Controllers
{
    public class HomeController : Controller
    { 
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
            var db = new Db();

            var listaTO = db.GetCarros();

            var listaCarro = new List<Carros>();
            foreach (var carroTO in listaTO)
            {
                listaCarro.Add(
                    new Carros()
                    {
                        id = carroTO.Id,
                        nome = carroTO.Nome,
                        fabricante = carroTO.Fabricante,
                        marca = carroTO.Marca,
                        modelo = carroTO.Modelo,
                        ano = carroTO.Ano.ToString()
                    }
                );
            }
            var viewModel = new CarrosViewModel() { listCarro = listaCarro };

            return View(viewModel);
        }

        public IActionResult CadastrarCarro(int? id)
        {
            Carros? carro = null;

            if (id != null)
            {
                var db = new Db();

                var carroTO = db.GetCarroById(id.GetValueOrDefault());

                carro = new Carros()
                {
                    id = carroTO.Id,
                    nome = carroTO.Nome,
                    fabricante = carroTO.Fabricante,
                    marca = carroTO.Marca,
                    modelo = carroTO.Modelo,
                    ano = carroTO.Ano.ToString()
                };
            }

            return View(carro);
        }

        public IActionResult PersistirCarro(int? id, string nome, string fabricante, string marca, string modelo, string ano)
        {
            var db = new Db();
            if (id == null)
            {
                var novoCarro = new CarroTO()
                {
                    Nome = nome,
                    Fabricante = fabricante,
                    Marca = marca,
                    Modelo = modelo,
                    Ano = int.Parse(ano),

                };

                db.AddCarro(novoCarro);
            }
            else
            {
                var alterarCarro = new CarroTO()
                {
                    Id = id.GetValueOrDefault(),
                    Nome = nome,
                    Fabricante = fabricante,
                    Marca = marca,
                    Modelo = modelo,
                    Ano = int.Parse(ano),
                };

                db.UpdateCarro(alterarCarro);
            }

            return RedirectToAction("Carros");
        }

        public IActionResult Deletar(int Id)
        {
            var db = new Db();

            db.DeleteCarro(Id);

            return RedirectToAction("Carros");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}