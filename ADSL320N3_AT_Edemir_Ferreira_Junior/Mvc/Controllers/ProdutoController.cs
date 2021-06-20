using MercadoClientes.ViewModels;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;

namespace Mvc.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepository _produtoRepository;
        public ProdutoController()
        {
            _produtoRepository = new ProdutoRepository();
        }
        // GET: ProdutoController
         
        public ActionResult Index(ProdutoIndexViewModel produtoIndexRequest)
        {
            var produtos = _produtoRepository
                .GetAll(produtoIndexRequest.OrderAscendant, produtoIndexRequest.Search)
                .ToList();

            var produtoIndexViewModel = new ProdutoIndexViewModel
            {
                Search = produtoIndexRequest.Search,
                Produtos = produtos,
                OrderAscendant = produtoIndexRequest.OrderAscendant
            };

            return View(produtoIndexViewModel);

           
        }

        // GET: ProdutoController/Details/5
        public ActionResult Details(int id)
        {
            var produto = _produtoRepository.GetById(id);

            if (produto == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(produto);
        }

        // GET: ProdutoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProdutoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProdutoModel produtoModel)
        {
            try
            {
                _produtoRepository.Create(produtoModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProdutoController/Edit/5
        public ActionResult Edit(int id)
        {
            var produto = _produtoRepository.GetById(id);

            if (produto == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(produto);
        }

        // POST: ProdutoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, ProdutoModel produtoModel)
        {
            try
            {

                produtoModel.IdProduto = Id;

                var produtoEditado = _produtoRepository.Edit(produtoModel);

                if (produtoEditado != null)
                {
                    return RedirectToAction(nameof(Details), new { id = produtoEditado.IdProduto });
                }              
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(produtoModel);
            }
        }

        // GET: ProdutoController/Delete/5
        public ActionResult Delete(int id)
        {
            var produto = _produtoRepository.GetById(id);

            return View(produto);
        }

        // POST: ProdutoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int id)
        {
            try
            {
                _produtoRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(nameof(Delete));
            }
        }
    }
}
