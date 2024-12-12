using Domain.Exceptions;
using Domain.Models;
using FinalProjectMVC.Services.Implementations;
using FinalProjectMVC.ViewModels.Admin.HomePreview;
using FinalProjectMVC.ViewModels.Admin.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace FinalProjectMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IActionResult> Index()
        {
            var news = await _newsService.GetNewsAsync();
            var newsVM = news.Select(x => new NewsVM
            {
                Id=x.Id,
                Title=x.Title,
                Description=x.Description,
                NewsCategory=x.NewsCategory,
                NewsDate=x.NewsDate,
                AuthorName=x.AuthorName,
                ImagePath=x.ImagePath,




            }).ToList();

            return View(newsVM);
        }

        public async Task<IActionResult> Details(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            var newsVM = new NewsVM
            {
                Id = news.Id,
                Title = news.Title,
                Description = news.Description,
                NewsCategory = news.NewsCategory,
                NewsDate = news.NewsDate,
                AuthorName=news.AuthorName,
                IsSelected = news.IsSelected,
                ImagePath = news.ImagePath,
            };

            return View(newsVM);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsVM newsVM)
        {
            if (ModelState.IsValid)
            {
                var news = new News
                {
                    Title = newsVM.Title,
                    Description = newsVM.Description,
                    NewsCategory = newsVM.NewsCategory,
                    NewsDate = newsVM.NewsDate,
                    AuthorName=newsVM.AuthorName,
                    ImagePath = newsVM.ImagePath,
                    IsSelected = newsVM.IsSelected
                };

                await _newsService.AddNewsAsync(news);
                return RedirectToAction(nameof(Index));
            }

            return View(newsVM);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            var newsVM = new NewsVM
            {
                Id = news.Id,
                Title = news.Title,
                Description = news.Description,
                NewsDate= news.NewsDate,
                NewsCategory= news.NewsCategory,
                AuthorName=news.AuthorName,
                ImagePath = news.ImagePath,
                IsSelected = news.IsSelected
            };

            return View(newsVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewsVM newsVM)
        {
            if (id != newsVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var news = new News
                    {
                        Id = newsVM.Id,
                        Title = newsVM.Title,
                        Description = newsVM.Description,
                        ImagePath = newsVM.ImagePath,
                        NewsCategory = newsVM.NewsCategory,
                        AuthorName = newsVM.AuthorName,
                        NewsDate = newsVM.NewsDate,
                        IsSelected = newsVM.IsSelected
                    };

                    await _newsService.UpdateNewsAsync(id, news);
                }
                catch (NotFoundException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(newsVM);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            var newsVM = new NewsVM
            {
                Id = news.Id,
                Title = news.Title,
                Description = news.Description,
                NewsDate = news.NewsDate,
                NewsCategory = news.NewsCategory,
                AuthorName = news.AuthorName,
                ImagePath = news.ImagePath,
                IsSelected = news.IsSelected
            };

            return View(newsVM);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _newsService.DeleteNewsAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsSelected(int id)
        {
            try
            {
                await _newsService.MarkAsSelectedAsync(id);
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
                await _newsService.UnmarkAsSelectedAsync(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }






    }
}
