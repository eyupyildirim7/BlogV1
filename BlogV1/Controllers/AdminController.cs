﻿using BlogV1.Context;
using BlogV1.Identity;
using BlogV1.Models;
using BlogV1.Models.VıewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogV1.Controllers

{  
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly BlogDbContext _context;
        private readonly UserManager<BlogIdentityUser> _userManager;
        private readonly SignInManager<BlogIdentityUser> _signInManager;
        public AdminController(BlogDbContext context, UserManager<BlogIdentityUser> userManager, SignInManager<BlogIdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
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

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model.Password == model.RePassword)
            {
                var user = new BlogIdentityUser
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Blogs");
        
        }

        

    }

}
