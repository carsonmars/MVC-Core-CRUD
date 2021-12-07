using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.RegistroFunc.DAL.Interface;
using App.RegistroFunc.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.RegistroFunc.Controllers
{
    public class FuncionarioController : Controller
    {
		private readonly IFuncionarioDAL _func;
		
		public FuncionarioController(IFuncionarioDAL funcionario)
		{
			_func = funcionario;
		}


		public IActionResult Index()
        {
			List<Funcionario> listaFuncionarios = new List<Funcionario>();
			listaFuncionarios = _func.GetAllFuncionarios().ToList();
            return View(listaFuncionarios);
        }

		[HttpGet]
		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Funcionario funcionario = _func.GetFuncionario(id);

			if (funcionario == null)
			{
				return NotFound();
			}

			return View(funcionario);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind] Funcionario funcionario)
		{
			if (ModelState.IsValid)
			{
				_func.AddFuncionario(funcionario);
				return RedirectToAction("Index");
			}
			return View(funcionario);
		}

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Funcionario funcionario = _func.GetFuncionario(id);

			if (funcionario == null)
			{
				return NotFound();
			}

			return View(funcionario);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind]Funcionario funcionario)
		{
			Funcionario funcValid = _func.GetFuncionario(id);
			if (funcValid == null)
			{
				return NotFound();
			}
			funcionario.FuncionarioId = id;
			if (ModelState.IsValid)
			{
				_func.UpdateFuncionario(funcionario);
				return RedirectToAction("Index");
			}
			return View(funcionario);
		}

		[HttpGet]
		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Funcionario funcionario = _func.GetFuncionario(id);
			if (funcionario == null)
			{
				return NotFound();
			}
			return View(funcionario);
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int? id)
		{
			_func.DeleteFuncionario(id);
			return RedirectToAction("Index");
		}

		
	}
}