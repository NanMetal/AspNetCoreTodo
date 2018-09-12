using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoItemService todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            this.todoItemService = todoItemService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await todoItemService.GetIncompleteItemsAsync();

            var model = new TodoViewModel()
            {
                Items = items
            };
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem newItem)
        {
            if (!ModelState.IsValid) { return RedirectToAction("Index"); }
            var successful = await todoItemService.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest("Could	not	add	item.");
            }
            return RedirectToAction("Index");
        }
    }
}