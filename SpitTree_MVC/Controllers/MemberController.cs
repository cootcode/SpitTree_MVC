using SpitTree_MVC.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;

namespace SpitTree_MVC.Controllers
{
    public class MemberController : Controller
    {

        //instance of the database.
        private SpitTreeDbContext db = new SpitTreeDbContext();


        // GET: Posts
        //the index action is called when registered user click the link "My Posts"
        //this method is returning a list of posts that where created by the logged in user (using user id)
        [Authorize(Roles = "Member")]//only registered user and as Member role can access this method
        public ActionResult Index()
        {
            //select all the posts from the Posts table
            //including the foreign keys category and User
            var posts = db.Posts
                .Include(p => p.Category)
                .Include(p => p.User);

            //get the Id of the logged in user, using Identity
            //user Id is a string
            var userId = User.Identity.GetUserId();

            //from the list of posts from the Posts tables
            //select only the ones that have the UserId equally to the Id of the logged in user
            //returns a list of posts
            posts = posts.Where(p => p.UserId == userId);

            //send the list of posts to the Index view in Members subfolder
            return View(posts.ToList());
        }

        // GET: Member/Details/5
        public ActionResult Details(int? id) //? creates a nullable variable
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            //find a post in the Posts table by id
            Post post = db.Posts.Find(id);

            //if post doesn't exists then return a not found error message
            if (post == null)
            {
                return HttpNotFound();
            }

            //otherwise send the post to the Details view
            //and display the values stored in the properties
            return View(post);
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            //send the list of categories to the view using a ViewBag
            //so user can select the category for the post from a dropdown box.
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");

            //return the Create view to the browser
            return View();
        }

        // POST: Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Title,Description,Location,Price,CategoryId")] Post post)
        {
            //if parameter post is not null
            if(ModelState.IsValid)
            {
                //assign todays date for DatePosted
                post.DatePosted = DateTime.Now;

                //set the expire date to 14 days later
                post.DateExpired = post.DatePosted.AddDays(14);

                //assign the registered user id as a foreignkey
                //this is the user who creates the post
                post.UserId = User.Identity.GetUserId();

                //add the post to the Posts tables
                db.Posts.Add(post);

                //save changes in database
                db.SaveChanges();

                //return to Index action in MemberController
                return RedirectToAction("Index");
            }

            //if the parameter post is null then send the list categories back to the create view
            //and try to create a post again
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);

            //send the post back to the create view
            return View(post);
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //find psot by Id in the Posts table
            Post post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            //get a list of all the categories from Categories table
            //and send the list to the view using a ViewBag
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);

            //also send the post to the Edit View
            //where user can change the details of the post
            return View(post);
        }

        // POST: Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Title,Description,Location,Price,CategoryId")] Post post)
        {
            //if the post passed as a parameter to the Edit action is not null then
            //the edited post will be updated in the database
            if (ModelState.IsValid)
            {
                //record the new date when the post was edited
                post.DatePosted = DateTime.Now;
                //sets the expire date to 14 days
                post.DateExpired = post.DatePosted.AddDays(14);
                //gets the id of the user that is logged in the system
                //and assigns it as a foreign key in the Post
                post.UserId = User.Identity.GetUserId();
                //updates the database
                db.Entry(post).State = EntityState.Modified;
                //saves changes to the database
                db.SaveChanges();
                //redirects the user to the Index action in MemberController that displays the list of posts
                return RedirectToAction("Index");
            }
            //otherwise, if the post parameter is null, then
            //we send the list of categories back to the Edit form and
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);

            //return the post to the Edit form
            return View(post);
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int? id)
        {
            //if id is null then return a bad request error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //first find a post in the Posts tables by id
            Post post = db.Posts.Find(id);

            //next find the post category by searching through the Categories table
            //for a category by id which is the foreign key in that post
            var category = db.Categories.Find(post.CategoryId);

            //assign the category to the Category navigational property Category
            //so we can display the category name
            post.Category = category;

            //if the post is a null object
            //then return a not found error message
            if (post == null)
            {
                return HttpNotFound();
            }

            //otherwise return the Delete view and send the post to the view
            //so post details can be viewed
            return View(post);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]//this is important]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, FormCollection collection)
        {
            //find post by id in Posts tables
            Post post = db.Posts.Find(id);

            //remove post form Posts table
            db.Posts.Remove(post);

            //save changes in the database
            db.SaveChanges();

            //redirect to Index action in MemberController
            return RedirectToAction("Index");
        }
    }
}
