using FinalProjectMVC.Exceptions;
using FinalProjectMVC.Models;
using FinalProjectMVC.ViewModels.Admin.AboutUs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace FinalProjectMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class AboutUsController : Controller
    {
        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutUsService.GetAboutUsAsync();
            var aboutVMs = abouts.Select(a => new AboutUsVM
            {
                Id = a.Id,
                Title = a.Title,
                Subtitle = a.Subtitle,
                Description = a.Description,
                Points = a.Points,
                ImageUrls = a.ImageUrls
            }).ToList();

            return View(aboutVMs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var aboutus = await _aboutUsService.GetAboutUsByIdAsync(id);
            if (aboutus == null)
            {
                return NotFound();
            }

            var aboutVM = new AboutUsVM
            {
                Id = aboutus.Id,
                Title = aboutus.Title,
                Subtitle = aboutus.Subtitle,
                Description = aboutus.Description,
                Points = aboutus.Points,
                ImageUrls = aboutus.ImageUrls
            };

            return View(aboutVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutUsVM aboutUsVM)
        {
            if (ModelState.IsValid)
            {
                var aboutUsmodel = new AboutUsViewModel
                {
                    Title = aboutUsVM.Title,
                    Subtitle = aboutUsVM.Subtitle,
                    Description = aboutUsVM.Description,
                    Points = aboutUsVM.Points,
                    ImageUrls = aboutUsVM.ImageUrls
                };

                await _aboutUsService.AddAboutUsAsync(aboutUsmodel);
                TempData["SuccessMessage"] = "About Us entry created successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "There was an issue creating the entry. Please check your inputs.";
            return View(aboutUsVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var aboutus = await _aboutUsService.GetAboutUsByIdAsync(id);
            if (aboutus == null)
            {
                return NotFound();
            }

            var aboutusVM = new AboutUsVM
            {
                Id = aboutus.Id,
                Title = aboutus.Title,
                Subtitle = aboutus.Subtitle,
                Description = aboutus.Description,
                Points = aboutus.Points,
                ImageUrls = aboutus.ImageUrls
            };

            return View(aboutusVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AboutUsVM aboutusVM)
        {
            if (id != aboutusVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aboutUsModel = new AboutUsViewModel
                    {
                        Id = aboutusVM.Id,
                        Title = aboutusVM.Title,
                        Subtitle = aboutusVM.Subtitle,
                        Description = aboutusVM.Description,
                        Points = aboutusVM.Points,
                        ImageUrls = aboutusVM.ImageUrls
                    };

                    await _aboutUsService.UpdateAboutUsAsync(id, aboutUsModel);
                    TempData["SuccessMessage"] = "About Us entry updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (NotFoundException)
                {
                    return NotFound();
                }
            }

            TempData["ErrorMessage"] = "There was an issue updating the entry. Please check your inputs.";
            return View(aboutusVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var aboutus = await _aboutUsService.GetAboutUsByIdAsync(id);
            if (aboutus == null)
            {
                return NotFound();
            }

            var aboutusVM = new AboutUsVM
            {
                Id = aboutus.Id,
                Title = aboutus.Title,
                Subtitle = aboutus.Subtitle,
                Description = aboutus.Description,
                Points = aboutus.Points,
                ImageUrls = aboutus.ImageUrls
            };

            return View(aboutusVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _aboutUsService.DeleteAboutUsAsync(id);
                TempData["SuccessMessage"] = "About Us entry deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                TempData["ErrorMessage"] = "The entry was not found.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}