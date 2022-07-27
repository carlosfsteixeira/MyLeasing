using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Controllers
{
    [Authorize]
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;

        public OwnersController(IOwnerRepository ownerRepository, IUserHelper userHelper, IBlobHelper blobHelper, IConverterHelper converterHelper)
        {
            _ownerRepository = ownerRepository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
        }

        // GET: Owners
        public IActionResult Index()
        {
            return View(_ownerRepository.GetAll().OrderBy(e => e.FirstName));
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "owners");
                }

                var owner = _converterHelper.ToOwner(model, imageId, true);

                owner.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                await _ownerRepository.CreateAsync(owner);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        //private Owner ToOwner(OwnerViewModel model, string path)
        //{
        //    return new Owner
        //    {
        //        Id = model.Id,
        //        Image = path,
        //        Document = model.Document,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        FixedPhone = model.FixedPhone,
        //        CellPhone = model.CellPhone,
        //        Adress = model.Adress,
        //        User = model.User,
        //    };
        //}

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToOwnerViewModel(owner);

            return View(model);
        }

        //private OwnerViewModel ToOwnerViewModel(Owner owner)
        //{
        //    return new OwnerViewModel
        //    {
        //        Id = owner.Id,
        //        Image = owner.Image,
        //        Document = owner.Document,
        //        FirstName = owner.FirstName,
        //        LastName = owner.LastName,
        //        FixedPhone = owner.FixedPhone,
        //        CellPhone = owner.CellPhone,
        //        Adress = owner.Adress,
        //        User = owner.User,
        //    };
        //}

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OwnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "owners");
                    }

                    var owner = _converterHelper.ToOwner(model, imageId, false);

                    owner.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                    await _ownerRepository.UpdateAsync(owner);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _ownerRepository.ExistAsync(model.Id))
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

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _ownerRepository.GetByIdAsync(id);

            await _ownerRepository.DeleteAsync(owner);

            return RedirectToAction(nameof(Index));
        }
    }
}
