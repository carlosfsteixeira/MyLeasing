using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Controllers
{
    public class LesseesController : Controller
    {
        private readonly ILesseeRepository _lesseeRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public LesseesController(ILesseeRepository lesseeRepository, IUserHelper userHelper, IImageHelper imageHelper, IConverterHelper converterHelper)
        {
            _lesseeRepository = lesseeRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Lessees
        public async Task<IActionResult> Index()
        {
            return View(_lesseeRepository.GetAll().OrderBy(e => e.FirstName));
        }

        // GET: Lessees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseeRepository.GetByIdAsync(id.Value);

            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // GET: Lessees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LesseeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "lessee");
                }

                var lessee = _converterHelper.ToLessee(model, path, true);

                await _lesseeRepository.CreateAsync(lessee);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Lessees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseeRepository.GetByIdAsync(id.Value);
            if (lessee == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToLesseeViewModel(lessee);

            return View(model);
        }

        // POST: Lessees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LesseeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.Image;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "lessee");
                    }

                    var lessee = _converterHelper.ToLessee(model, path, false);

                    await _lesseeRepository.UpdateAsync(lessee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _lesseeRepository.ExistAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Lessees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseeRepository.GetByIdAsync(id.Value);

            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // POST: Lessees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lessee = await _lesseeRepository.GetByIdAsync(id);

            await _lesseeRepository.DeleteAsync(lessee);

            return RedirectToAction(nameof(Index));
        }
    }
}
