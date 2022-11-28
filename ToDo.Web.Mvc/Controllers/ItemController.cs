using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Entities;
using ToDo.Domain.Interface;
using ToDo.Web.Mvc.Models;

namespace ToDo.Web.Mvc.Controllers;

public class ItemController : Controller
{
    protected IItemRepository repository;

    public ItemController(IItemRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var items = await repository.GetAllAsync();

        return View(items);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Description")] CreateItemModel createItemModel)
    {
        if (ModelState.IsValid)
        {
            var item = new Item(createItemModel.Description);
            await repository.AddAsync(item);
            return RedirectToAction(nameof(Index));
        }

        return View(createItemModel);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();

        var item = await repository.GetById(id.Value);

        if (item is null) return NotFound();

        return View(item);
    }

    [ActionName("Edit")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditItem([Bind("Id,Done")] EditItemModel editItemModel)
    {
        if (!ModelState.IsValid)
            return View(editItemModel);

        var itemToUpdate = await repository.GetById(editItemModel.Id);

        if (itemToUpdate is null)
            return NotFound();

        if (editItemModel.Done)
            itemToUpdate.MarkAsDone();
        else
            itemToUpdate.MarkAsUndone();

        await repository.UpdateAsync(editItemModel.Id, itemToUpdate);

        return RedirectToAction(nameof(Index));
    }

    [ActionName("Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await repository.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}