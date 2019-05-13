using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;


namespace ToDoList.Controllers
{
  public class CategoriesController : Controller
  {

    [HttpGet("/categories")]
    public ActionResult Index()
    {
      List<Category> allCategories = Category.GetAll();
      return View(allCategories);
    }

    [HttpGet("/categories/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/categories")]
    public ActionResult Create(string categoryName)
    {
      Category newCategory = new Category(categoryName);
      List<Category> allCategories = Category.GetAll();
      newCategory.Save();
      // return View("Index", allCategories);
      return RedirectToAction("Index");
    }
    //modified Controller and Show View to accept a single category object
    //instead of a Dictionary (we thought dictionary was redundant)

    [HttpGet("/categories/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Category selectedCategory = Category.Find(id);
      List<Item> categoryItems = selectedCategory.GetItems();
      model.Add("category", selectedCategory);
      model.Add("items", categoryItems);
      // return View(model);
      return View(selectedCategory);
    }
    // This one creates new Items within a given Category, not new Categories:

    [HttpPost("/categories/{categoryId}/items")]
    public ActionResult Create(int categoryId, string itemDescription, bool itemCompleted)
    {
      // Dictionary<string, object> model = new Dictionary<string, object>();
      Category foundCategory = Category.Find(categoryId);
      Item newItem = new Item(itemDescription, itemCompleted, categoryId);
      newItem.Save();
      foundCategory.GetItems();
      // List<Item> categoryItems = foundCategory.GetItems();
      // model.Add("items", categoryItems);
      // model.Add("category", foundCategory);
      return View("Show", foundCategory);
    }

    // [HttpPost("/categories/{categoryId}/delete-category")]
    // public ActionResult DeleteCategory(int categoryId)
    // {
    //   Category selectedCategory = Category.Find(categoryId);
    //   selectedCategory.DeleteCat(categoryId);
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   List<Item> categoryItems = selectedCategory.GetItems();
    //   model.Add("category", selectedCategory);
    //   return RedirectToAction("Index", "Categories");

    //
    // Item item = Item.Find(itemId);
    // item.Delete();
    // Dictionary<string, object> model = new Dictionary<string, object>();
    // Category foundCategory = Category.Find(categoryId);
    // List<Item> categoryItems = foundCategory.GetItems();
    // model.Add("item", categoryItems);
    // model.Add("category", foundCategory);
    // // return View(model);
    // return RedirectToAction("Show", "Categories");
    // //return RedirectToAction("actionName", "controllerName"); goes to a cshtml page in a different controller.
    // }

  }
}
