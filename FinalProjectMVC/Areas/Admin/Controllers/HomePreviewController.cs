using FinalProjectMVC.Exceptions;
using FinalProjectMVC.Models;

using FinalProjectMVC.Services.Implementations;
using FinalProjectMVC.ViewModels.Admin.FAQ;
using FinalProjectMVC.ViewModels.Admin.HomePreview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace FinalProjectMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class HomePreviewController : Controller
    {
        private readonly IHomePreviewService _homePreviewService;

        public HomePreviewController(IHomePreviewService homePreviewService)
        {
            _homePreviewService = homePreviewService;
        }

        public async Task<IActionResult> Index()
        {
           var previews = await _homePreviewService.GetPreviewsAsync();
           var previewVMs = previews.Select(x => new PreviewVM
           {
               Id = x.Id,
               Title=x.Title,
               Description=x.Description,
               ImagePath=x.ImagePath,
               IsSelected=x.IsSelected,
               
           }).ToList();

            return View(previewVMs);
        }


        public async Task<IActionResult> Details(int id)
        {
            var preview = await _homePreviewService.GetPreviewByIdAsync(id);
            if (preview == null)
            {
                return NotFound();
            }

            var previewVM = new PreviewVM
            {
                Id = preview.Id,
                Title = preview.Title,
                Description = preview.Description,
                ImagePath = preview.ImagePath,
                IsSelected = preview.IsSelected
            };

            return View(previewVM);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PreviewVM previewVM)
        {
            if (ModelState.IsValid)
            {
                var preview = new HomePreview
                {
                    Title = previewVM.Title,
                    Description = previewVM.Description,
                    ImagePath = previewVM.ImagePath,
                    IsSelected = previewVM.IsSelected
                };

                await _homePreviewService.AddPreviewAsync(preview);
                return RedirectToAction(nameof(Index));
            }

            return View(previewVM);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var preview = await _homePreviewService.GetPreviewByIdAsync(id);
            if (preview == null)
            {
                return NotFound();
            }

            var previewVM = new PreviewVM
            {
                Id = preview.Id,
                Title = preview.Title,
                Description= preview.Description,
                ImagePath = preview.ImagePath,
                IsSelected = preview.IsSelected
            };

            return View(previewVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PreviewVM previewVM)
        {
            if (id != previewVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var preview = new HomePreview
                    {
                        Id = previewVM.Id,
                        Title= previewVM.Title,
                        Description= previewVM.Description,
                        ImagePath= previewVM.ImagePath,
                        IsSelected = previewVM.IsSelected
                    };

                    await _homePreviewService.UpdatePreviewAsync(id, preview);
                }
                catch (NotFoundException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(previewVM);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var preview = await _homePreviewService.GetPreviewByIdAsync(id);
            if (preview == null)
            {
                return NotFound();
            }

            var previewVM = new PreviewVM
            {
                Id = preview.Id,
                Title = preview.Title,
                Description = preview.Description,
                ImagePath = preview.ImagePath,
                IsSelected = preview.IsSelected
            };

            return View(previewVM);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _homePreviewService.DeletePreviewAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsSelected(int id)
        {
            try
            {
                await _homePreviewService.MarkAsSelectedAsync(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnmarkAsSelected(int id)
        {
            try
            {
                await _homePreviewService.UnmarkAsSelectedAsync(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }





    }
    }

