using Domain.Models;
using FinalProjectMVC.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Services.Interfaces;

namespace FinalProjectMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class SliderController : Controller
    {

        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;

        public SliderController(AppDbContext context, ISliderService sliderService)
        {

            _sliderService = sliderService;
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Fetch the sliders from the service
            var sliders = await _sliderService.GetSliderSAsync();

            // Map Domain.Models.Slider to SliderVM
            var sliderVMs = sliders.Select(slider => new SliderVM
            {
                Id = slider.Id,
                Title = slider.Title,
                Subtitle = slider.Subtitle,
            }).ToList();

            // Pass the SliderVM list to the view
            return View(sliderVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var slider = await _sliderService.GetSliderByIdAsync((int)id);
            if (slider is null) return NotFound();
            return View(slider);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderVM sliderVM)
        {
            if (!ModelState.IsValid)
            {
                return View(sliderVM);
            }

            var slider = new Slider
            {
                Title = sliderVM.Title,
                Subtitle = sliderVM.Subtitle

            };

            await _sliderService.AddSliderAsync(slider);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _sliderService.GetSliderByIdAsync((int)id);
            if (slider is null) return NotFound();

            var sliderVM = new SliderVM
            {
                Id = slider.Id,
                Title = slider.Title,
                Subtitle = slider.Subtitle

            };

            return View(sliderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SliderVM sliderVM)
        {
            if (id != sliderVM.Id) return BadRequest();
            if (!ModelState.IsValid) return View(sliderVM);

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();

            slider.Title = sliderVM.Title;
            slider.Subtitle = sliderVM.Subtitle;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();

            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeMainStatus(int id)
        {
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null) return NotFound();

            slider.IsMain = true;

            var currentMainSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id != id && m.IsMain);
            if (currentMainSlider != null)
            {
                currentMainSlider.IsMain = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
