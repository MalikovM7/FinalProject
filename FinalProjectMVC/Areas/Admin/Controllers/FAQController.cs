using FinalProjectMVC.Exceptions;
using FinalProjectMVC.Models;
using FinalProjectMVC.ViewModels.Admin.FAQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace FinalProjectMVC.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class FAQController : Controller
    {
        private readonly IfaqService _faqService;

        public FAQController(IfaqService faqService)
        {
            _faqService = faqService;
        }

       
        public async Task<IActionResult> Index()
        {
            var faqs = await _faqService.GetFAQSAsync();
            var faqVMs = faqs.Select(f => new FAQVM
            {
                Id = f.Id,
                Question = f.Question,
                Answer = f.Answer
            }).ToList();

            return View(faqVMs);
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var faq = await _faqService.GetFAQByIdAsync(id);
            if (faq == null)
            {
                return NotFound();
            }

            var faqVM = new FAQVM
            {
                Id = faq.Id,
                Question = faq.Question,
                Answer = faq.Answer
            };

            return View(faqVM);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FAQVM faqVM)
        {
            if (ModelState.IsValid)
            {
                var faq = new FAQ
                {
                    Question = faqVM.Question,
                    Answer = faqVM.Answer
                };

                await _faqService.AddFAQAsync(faq);
                return RedirectToAction(nameof(Index));
            }

            return View(faqVM);
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var faq = await _faqService.GetFAQByIdAsync(id);
            if (faq == null)
            {
                return NotFound();
            }

            var faqVM = new FAQVM
            {
                Id = faq.Id,
                Question = faq.Question,
                Answer = faq.Answer
            };

            return View(faqVM);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FAQVM faqVM)
        {
            if (id != faqVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var faq = new FAQ
                    {
                        Id = faqVM.Id,
                        Question = faqVM.Question,
                        Answer = faqVM.Answer
                    };

                    await _faqService.UpdateFAQAsync(id, faq);
                }
                catch (NotFoundException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(faqVM);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var faq = await _faqService.GetFAQByIdAsync(id);
            if (faq == null)
            {
                return NotFound();
            }

            var faqVM = new FAQVM
            {
                Id = faq.Id,
                Question = faq.Question,
                Answer = faq.Answer
            };

            return View(faqVM);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _faqService.DeleteFAQAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
