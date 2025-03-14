﻿using BlogV1.Context;
using BlogV1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogV1.Controllers
{
    public class AdminController : Controller
    {
        private readonly BlogDbContext _context;

        public AdminController(BlogDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Blogs() {
            var blogs=_context.Blogs.ToList();
        return View(blogs);
        }
        public IActionResult EditBlog(int id)
        {
            var blog =_context.Blogs.Where(x=>x.Id==id).FirstOrDefault();
            return View(blog);
        }
        //blog silme islemi
        public IActionResult DeleteBlog(int id)
        {
            var blog=_context.Blogs.Where(x=> x.Id==id).FirstOrDefault();
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            return RedirectToAction("Blogs");
        }
        //edit post islemi
        [HttpPost]
        public IActionResult EditBlog(Blog model)
        {
            var blog=_context.Blogs.Where(x=>x.Id==model.Id).FirstOrDefault();
            blog.Name = model.Name;
            blog.Description = model.Description;
            blog.Tags = model.Tags;
            blog.Imageurl = model.Imageurl;
            _context.SaveChanges();
            return RedirectToAction("Blogs");
        }
        //status ac kapa blogu

        public IActionResult ToggleStatus(int id) {
            var blog = _context.Blogs.Where(x => x.Id == id).FirstOrDefault();
            if (blog.Status == 1)
            {
                blog.Status = 0;
            }
            else
            {
                blog.Status = 1;
            }
            _context.SaveChanges();
            return RedirectToAction("Blogs");
        }

        public IActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBlog(Blog model)
        {
            model.publisDate = DateTime.Now;
            model.Status = 1;
            _context.Blogs.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Blogs");
        }
        public IActionResult Comments(int? blogId)
        {
            var comments = new List<Comment>();
           
            if ( blogId == null){
                comments = _context.Comments.ToList();
            }
            else
            {
                 comments =_context.Comments.Where(x=>x.BlogId==blogId).ToList();
            }
           

            return View(comments);
        }
        public IActionResult DeleteComment(int id) {
        var comment=_context.Comments.Where(x => x.id == id).FirstOrDefault();
        _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Comments");
        }
        public IActionResult Register()
        {
            return View();
        }

    }

}
